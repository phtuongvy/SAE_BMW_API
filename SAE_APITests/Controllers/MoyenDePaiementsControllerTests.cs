using Microsoft.AspNetCore.Mvc;
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
    public class MoyenDePaiementsControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private MoyenDePaiementsController controller;
        private BMWDBContext context;
        private IDataRepository<MoyenDePaiement> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur MoyenDePaiement.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new MoyenDePaiementManager(context);
            controller = new MoyenDePaiementsController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur MoyenDePaiementController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void MoyenDePaiementControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new MoyenDePaiementManager(context);

            // Act : appel de la méthode à tester
            var option = new MoyenDePaiementsController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetMoyenDePaiementTest

        /// <summary>
        /// Teste la méthode GetMoyenDePaiements pour vérifier qu'elle retourne la liste correcte des éléments MoyenDePaiement.
        /// </summary>
        [TestMethod()]
        public void GetMoyenDePaiementTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<MoyenDePaiement> expected = context.MoyensDePaiement.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetMoyenDePaiements().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetMoyenDePaiementByIdTest

        /// <summary>
        /// Teste la méthode GetMoyenDePaiementById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetMoyenDePaiementByIdTest()
        {
            // Arrange : préparation des données attendues
            MoyenDePaiement expected = context.MoyensDePaiement.Find(1);
            // Act : appel de la méthode à tester
            var res = controller.GetMoyenDePaiementById(expected.IdMoyenDePaiement).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetMoyenDePaiementById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetMoyenDePaiementByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<MoyenDePaiement>>();

            MoyenDePaiement option = new MoyenDePaiement
            {
                IdMoyenDePaiement = 4,
                LibelleMoyenDePaiement = "test"
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdMoyenDePaiement).Result).Returns(option);
            var userController = new MoyenDePaiementsController(mockRepository.Object);
            var actionResult = userController.GetMoyenDePaiementById(option.IdMoyenDePaiement).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as MoyenDePaiement);
        }
        #endregion

        #region Test PutMoyenDePaiementTestAsync
        /// <summary>
        /// Teste la méthode PutMoyenDePaiement pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutMoyenDePaiementTestAsync_ReturnsBadRequest()
        {
            //Arrange
            MoyenDePaiement MoyenDePaiement = new MoyenDePaiement
            {
                IdMoyenDePaiement = 4,
                LibelleMoyenDePaiement = "test"
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutMoyenDePaiement(3, MoyenDePaiement);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutMoyenDePaiement en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutMoyenDePaiementTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<MoyenDePaiement>>();
            var _controller = new MoyenDePaiementsController(mockRepository.Object);
            var MoyenDePaiement = new MoyenDePaiement { IdMoyenDePaiement = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(MoyenDePaiement);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<MoyenDePaiement>(), It.IsAny<MoyenDePaiement>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutMoyenDePaiement(1, MoyenDePaiement);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutMoyenDePaiement_ReturnsNotFound_WhenMoyenDePaiementDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<MoyenDePaiement>>();
            var _controller = new MoyenDePaiementsController(mockRepository.Object);
            var MoyenDePaiement = new MoyenDePaiement { IdMoyenDePaiement = 1000 };
            mockRepository.Setup(x => x.GetByIdAsync(1000)).ReturnsAsync((MoyenDePaiement)null);

            // Act
            var result = await _controller.PutMoyenDePaiement(1000, MoyenDePaiement);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostMoyenDePaiementTestAsync

        /// <summary>
        /// Teste la méthode PostMoyenDePaiement pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostMoyenDePaiementTestAsync()
        {
            // Arrange : préparation des données attendues
            MoyenDePaiement option = new MoyenDePaiement
            {
                IdMoyenDePaiement = 4,
                LibelleMoyenDePaiement = "test"
            };

            // Act : appel de la méthode à tester
            var result = controller.PostMoyenDePaiement(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            MoyenDePaiement? optionRecupere = context.MoyensDePaiement
                .Where(u => u.IdMoyenDePaiement == option.IdMoyenDePaiement)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdMoyenDePaiement = optionRecupere.IdMoyenDePaiement;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.MoyensDePaiement.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostMoyenDePaiement en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostMoyenDePaiementTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<MoyenDePaiement>>();
            var userController = new MoyenDePaiementsController(mockRepository.Object);



            // Arrange : préparation des données attendues
            MoyenDePaiement option = new MoyenDePaiement
            {
                IdMoyenDePaiement = 4,
                LibelleMoyenDePaiement = "test"
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostMoyenDePaiement(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<MoyenDePaiement>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(MoyenDePaiement), "Pas un Utilisateur");

            option.IdMoyenDePaiement = ((MoyenDePaiement)result.Value).IdMoyenDePaiement;
            Assert.AreEqual(option, (MoyenDePaiement)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteMoyenDePaiementTest
        /// <summary>
        /// Teste la méthode DeleteMoyenDePaiement pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteMoyenDePaiementTest()
        {
            // Arrange : préparation des données attendues
            MoyenDePaiement option = new MoyenDePaiement
            {
                IdMoyenDePaiement = 4,
                LibelleMoyenDePaiement = "test"
            };
            context.MoyensDePaiement.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            MoyenDePaiement option1 = context.MoyensDePaiement.FirstOrDefault(u => u.IdMoyenDePaiement == option.IdMoyenDePaiement);
            _ = controller.DeleteMoyenDePaiement(option.IdMoyenDePaiement).Result;

            // Arrange : préparation des données attendues
            MoyenDePaiement res = context.MoyensDePaiement.FirstOrDefault(u => u.IdMoyenDePaiement == option.IdMoyenDePaiement);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteMoyenDePaiement en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteMoyenDePaiementTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            MoyenDePaiement option = new MoyenDePaiement
            {
                IdMoyenDePaiement = 4,
                LibelleMoyenDePaiement="test"

            };
            var mockRepository = new Mock<IDataRepository<MoyenDePaiement>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdMoyenDePaiement).Result).Returns(option);
            var userController = new MoyenDePaiementsController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteMoyenDePaiement(option.IdMoyenDePaiement).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}