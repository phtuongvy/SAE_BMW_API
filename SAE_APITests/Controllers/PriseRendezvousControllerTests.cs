﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SAE_API.Controllers;
using SAE_API.Models.DataManager;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_API.Controllers.Tests
{
    [TestClass()]
    public class PriseRendezvousControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private PriseRendezvousController controller;
        private BMWDBContext context;
        private IDataRepository<PriseRendezvous> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur PriseRendezvous.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new PriseRendezvousManager(context);
            controller = new PriseRendezvousController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur PriseRendezvoussController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void PriseRendezvoussControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new PriseRendezvousManager(context);

            // Act : appel de la méthode à tester
            var option = new PriseRendezvousController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetPriseRendezvousTest

        /// <summary>
        /// Teste la méthode GetPriseRendezvouss pour vérifier qu'elle retourne la liste correcte des éléments PriseRendezvous.
        /// </summary>
        [TestMethod()]
        public void GetPriseRendezvousTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<PriseRendezvous> expected = context.PriseRendezvouss.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetPriseRendezvouss().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetPriseRendezvousByIdTest

        /// <summary>
        /// Teste la méthode GetPriseRendezvousById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetPriseRendezvousByIdTest()
        {
            // Arrange : préparation des données attendues
            PriseRendezvous expected = context.PriseRendezvouss.Find(1);
            // Act : appel de la méthode à tester
            var res = controller.GetPriseRendezvousById(expected.IdReservationOffre).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetPriseRendezvousById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetPriseRendezvousByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<PriseRendezvous>>();

            PriseRendezvous option = new PriseRendezvous
            {
                IdReservationOffre = 1,
                IdConcessionnaire = 4,
                NomReservation = "Duval",
                PrenomReservation = "Anastasia Mcintyre",
                CiviliteReservation = "M",
                EmailReservation = "magna@yahoo.fr",
                TelephoneReservation = "07 11 22 96 65",
                VilleReservation = "VILLE",
                TypeDePermis = 1
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdReservationOffre).Result).Returns(option);
            var userController = new PriseRendezvousController(mockRepository.Object);
            var actionResult = userController.GetPriseRendezvousById(option.IdReservationOffre).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as PriseRendezvous);
        }
        #endregion

        #region Test PutPriseRendezvousTestAsync
        /// <summary>
        /// Teste la méthode PutPriseRendezvous pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutPriseRendezvousTestAsync_ReturnsBadRequest()
        {
            //Arrange
            PriseRendezvous addresse = new PriseRendezvous
            {
                IdReservationOffre = 1,
                IdConcessionnaire = 4,
                NomReservation = "Duval",
                PrenomReservation = "Anastasia Mcintyre",
                CiviliteReservation = "M",
                EmailReservation = "magna@yahoo.fr",
                TelephoneReservation = "07 11 22 96 65",
                VilleReservation = "VILLE",
                TypeDePermis = 1
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutPriseRendezvous(1, addresse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutPriseRendezvous en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutPriseRendezvousTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<PriseRendezvous>>();
            var _controller = new PriseRendezvousController(mockRepository.Object);
            var addresse = new PriseRendezvous { IdReservationOffre = 100, };
            mockRepository.Setup(x => x.GetByIdAsync(1, 2)).ReturnsAsync(addresse);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<PriseRendezvous>(), It.IsAny<PriseRendezvous>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutPriseRendezvous(100, addresse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutPriseRendezvous_ReturnsNotFound_WhenPriseRendezvousDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<PriseRendezvous>>();
            var _controller = new PriseRendezvousController(mockRepository.Object);
            var addresse = new PriseRendezvous { IdReservationOffre = 100, };
            mockRepository.Setup(x => x.GetByIdAsync(1000, 1000)).ReturnsAsync((PriseRendezvous)null);

            // Act
            var result = await _controller.PutPriseRendezvous(100, addresse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
        #endregion

        #region Test PostPriseRendezvousTestAsync

        /// <summary>
        /// Teste la méthode PostPriseRendezvous pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostPriseRendezvousTestAsync()
        {
            // Arrange : préparation des données attendues
            PriseRendezvous option = new PriseRendezvous
            {
                IdReservationOffre = 23,
                IdConcessionnaire = 4,
                
            };

            // Act : appel de la méthode à tester
            var result = controller.PostPriseRendezvous(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            PriseRendezvous? optionRecupere = context.PriseRendezvouss
                .Where(u => u.IdReservationOffre == option.IdReservationOffre)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdReservationOffre = optionRecupere.IdReservationOffre;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.PriseRendezvouss.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostPriseRendezvous en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostPriseRendezvousTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<PriseRendezvous>>();
            var userController = new PriseRendezvousController(mockRepository.Object);



            // Arrange : préparation des données attendues
            PriseRendezvous option = new PriseRendezvous
            {
                IdReservationOffre = 1,
                IdConcessionnaire = 4,
                NomReservation = "Duval",
                PrenomReservation = "Anastasia Mcintyre",
                CiviliteReservation = "M",
                EmailReservation = "magna@yahoo.fr",
                TelephoneReservation = "07 11 22 96 65",
                VilleReservation = "VILLE",
                TypeDePermis = 1
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostPriseRendezvous(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<PriseRendezvous>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(PriseRendezvous), "Pas un Utilisateur");

            option.IdReservationOffre = ((PriseRendezvous)result.Value).IdReservationOffre;

            Assert.AreEqual(option, (PriseRendezvous)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeletePriseRendezvousTest
        /// <summary>
        /// Teste la méthode DeletePriseRendezvous pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeletePriseRendezvousTest()
        {
            // Arrange : préparation des données attendues
            PriseRendezvous option = new PriseRendezvous
            {
                IdReservationOffre = 23,
                IdConcessionnaire = 4,
                
            };

            context.PriseRendezvouss.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            PriseRendezvous option1 = context.PriseRendezvouss.FirstOrDefault(u => u.IdReservationOffre == option.IdReservationOffre);
            _ = controller.DeletePriseRendezvous(option.IdReservationOffre).Result;

            // Arrange : préparation des données attendues
            PriseRendezvous res = context.PriseRendezvouss.FirstOrDefault(u => u.IdReservationOffre == option.IdReservationOffre);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeletePriseRendezvous en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeletePriseRendezvousTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            PriseRendezvous option = new PriseRendezvous
            {
                IdReservationOffre=1,
                IdConcessionnaire=4,
                NomReservation = "Duval",
                PrenomReservation = "Anastasia Mcintyre",
                CiviliteReservation = "M",
                EmailReservation = "magna@yahoo.fr",
                TelephoneReservation = "07 11 22 96 65",
                VilleReservation = "VILLE",
                TypeDePermis = 1
            };
            var mockRepository = new Mock<IDataRepository<PriseRendezvous>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdReservationOffre).Result).Returns(option);
            var userController = new PriseRendezvousController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeletePriseRendezvous(option.IdReservationOffre).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}