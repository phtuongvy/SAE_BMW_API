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
    /// <summary>
    /// Classe de test pour APourValeurController. Contient des tests unitaires pour tester le comportement du contrôleur APourValeur.
    /// </summary>
    [TestClass()]
    public class APourValeurControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private APourValeurController controller;
        private BMWDBContext context;
        private IDataRepository<APourValeur> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur APourValeur.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new APourValeurManager(context);
            controller = new APourValeurController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur APourValeurController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void APourValeurControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new APourValeurManager(context);

            // Act : appel de la méthode à tester
            var option = new APourValeurController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetAPourValeurTest

        /// <summary>
        /// Teste la méthode GetAPourValeur pour vérifier qu'elle retourne la liste correcte des éléments APourValeur.
        /// </summary>
        [TestMethod()]
        public void GetAPourValeurTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<APourValeur> expected = context.APourValeurs.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetAPourValeurs().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetAPourValeurByIdTest

        /// <summary>
        /// Teste la méthode GetAPourValeurById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetAPourValeurByIdTest()
        {
            // Arrange : préparation des données attendues
            APourValeur expected = context.APourValeurs.Find(1, 2);
            // Act : appel de la méthode à tester
            var res = controller.GetAPourValeurById(expected.IdCaracteristiqueMoto, expected.IdMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetAPourValeurById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetAPourValeurByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<APourValeur>>();

            APourValeur option = new APourValeur
            {
                IdCaracteristiqueMoto = 1,
                IdMoto = 1,
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdCaracteristiqueMoto, option.IdMoto).Result).Returns(option);
            var userController = new APourValeurController(mockRepository.Object);
            var actionResult = userController.GetAPourValeurById(option.IdCaracteristiqueMoto, option.IdMoto).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as APourValeur);
        }
        #endregion

        #region Test PutAPourValeurTestAsync
        /// <summary>
        /// Teste la méthode PutAPourValeur pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutAPourValeurTestAsync()
        {
            //Arrange
            APourValeur optionAtester = new APourValeur
            {
                IdCaracteristiqueMoto = 40,
                IdMoto = 7,
            };

            APourValeur optionUptade = new APourValeur
            {
                IdCaracteristiqueMoto = 40,
                IdMoto = 7,
            };


            // Act : appel de la méthode à tester
            var res = await controller.PutAPourValeur(optionAtester.IdCaracteristiqueMoto, optionAtester.IdMoto, optionUptade);

            // Arrange : préparation des données attendues
            var nouvelleoption = controller.GetAPourValeurById(optionUptade.IdCaracteristiqueMoto, optionUptade.IdMoto).Result;
            Assert.AreEqual(optionUptade, res);

            context.APourValeurs.Remove(optionUptade);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PutAPourValeur en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public void PutAPourValeurTestAvecMoq()
        {

            // Arrange : préparation des données attendues
            APourValeur optionToUpdate = new APourValeur
            {
                IdCaracteristiqueMoto = 40,
                IdMoto = 5,
            };
            APourValeur updatedOption = new APourValeur
            {
                IdCaracteristiqueMoto = 100,
                IdMoto = 100,
            };

            var mockRepository = new Mock<IDataRepository<APourValeur>>();
            mockRepository.Setup(repo => repo.GetByIdAsync(21000)).ReturnsAsync(optionToUpdate);
            mockRepository.Setup(repo => repo.UpdateAsync(optionToUpdate, updatedOption)).Returns(Task.CompletedTask);


            var controller = new APourValeurController(mockRepository.Object);

            // Act : appel de la méthode à tester
            var result = controller.PutAPourValeur(40, 5, updatedOption).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(result, typeof(ActionResult<APourValeur>), "La réponse n'est pas du type attendu APourValeur");
        }
        #endregion

        #region Test PostAPourValeurTestAsync

        /// <summary>
        /// Teste la méthode PostAPourValeur pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostAPourValeurTestAsync()
        {
            // Arrange : préparation des données attendues
            APourValeur option = new APourValeur
            {
                IdCaracteristiqueMoto = 3,
                IdMoto = 4,
            };

            // Act : appel de la méthode à tester
            var result = controller.PostAPourValeur(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            APourValeur? optionRecupere = context.APourValeurs
                .Where(u => u.IdCaracteristiqueMoto == option.IdCaracteristiqueMoto && u.IdMoto == option.IdMoto)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdCaracteristiqueMoto = optionRecupere.IdCaracteristiqueMoto;
            option.IdMoto = optionRecupere.IdMoto;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.APourValeurs.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostAPourValeur en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostAPourValeurTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<APourValeur>>();
            var userController = new APourValeurController(mockRepository.Object);



            // Arrange : préparation des données attendues
            APourValeur option = new APourValeur
            {
                IdCaracteristiqueMoto = 1,
                IdMoto = 7,
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostAPourValeur(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<APourValeur>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(APourValeur), "Pas un Utilisateur");

            option.IdCaracteristiqueMoto = ((APourValeur)result.Value).IdCaracteristiqueMoto;
            option.IdMoto = ((APourValeur)result.Value).IdMoto;
            Assert.AreEqual(option, (APourValeur)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteAPourValeurTest
        /// <summary>
        /// Teste la méthode DeleteAPourValeur pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteAPourValeurTest()
        {
            // Arrange : préparation des données attendues
            APourValeur option = new APourValeur
            {
                IdCaracteristiqueMoto = 4,
                IdMoto = 4,
            };
            context.APourValeurs.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            APourValeur option1 = context.APourValeurs.FirstOrDefault(u => u.IdMoto == option.IdMoto && u.IdCaracteristiqueMoto == option.IdCaracteristiqueMoto);
            _ = controller.DeleteAPourValeur(option.IdCaracteristiqueMoto, option.IdMoto).Result;

            // Arrange : préparation des données attendues
            APourValeur res = context.APourValeurs.FirstOrDefault(u => u.IdCaracteristiqueMoto == option.IdCaracteristiqueMoto && u.IdMoto == option.IdMoto);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteAPourValeur en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteAPourValeurTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            APourValeur option = new APourValeur
            {
                IdCaracteristiqueMoto = 1,
                IdMoto = 7,
            };
            var mockRepository = new Mock<IDataRepository<APourValeur>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdCaracteristiqueMoto, option.IdMoto).Result).Returns(option);
            var userController = new APourValeurController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteAPourValeur(option.IdCaracteristiqueMoto, option.IdMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}