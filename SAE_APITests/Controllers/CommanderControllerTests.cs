using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SAE_API.Controllers;
using SAE_API.Models.DataManager;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;
using System;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_API.Controllers.Tests
{
    [TestClass()]
    public class CommanderControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private CommanderController controller;
        private BMWDBContext context;
        private IDataRepository<Commander> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur Commander.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new CommanderManager(context);
            controller = new CommanderController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur CommanderController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void CommanderControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new CommanderManager(context);

            // Act : appel de la méthode à tester
            var option = new CommanderController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetCommanderTest

        /// <summary>
        /// Teste la méthode GetCommanders pour vérifier qu'elle retourne la liste correcte des éléments Commander.
        /// </summary>
        [TestMethod()]
        public void GetCommanderTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<Commander> expected = context.Commanders.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetCommanders().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
           CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetCommanderByIdTest

        /// <summary>
        /// Teste la méthode GetCommanderById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetCommanderByIdTest()
        {
            // Arrange : préparation des données attendues
            Commander expected = context.Commanders.Find(1);
            // Act : appel de la méthode à tester
            var res = controller.GetCommanderById(expected.IdCommande, expected.IdEquipement , expected.IdConfigurationMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetCommanderById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetCommanderByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Commander>>();

            Commander option = new Commander
            {
                IdEquipement = 1,
                IdCommande = 1,
                IdConfigurationMoto = 1,
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync( option.IdCommande, option.IdEquipement , option.IdConfigurationMoto).Result).Returns(option);
            var userController = new CommanderController(mockRepository.Object);
            var actionResult = userController.GetCommanderById(option.IdCommande, option.IdEquipement, option.IdConfigurationMoto).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as Commander);
        }
        #endregion

        #region Test PutCommanderTestAsync
        /// <summary>
        /// Teste la méthode PutCommander pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutCommanders_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var aPourTailles = new Commander { IdCommande = 1 , IdConfigurationMoto = 1};

            // Act
            var result = await controller.PutCommander(3, 2, 1, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutCommander en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutCommanders_ReturnsBadRequestResult_WhenCommanderDoesNotExistAsync()
        {

            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Commander>>();
            var _controller = new CommanderController(mockRepository.Object);

            var aPourTailles = new Commander { IdCommande = 1, IdConfigurationMoto = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Commander)null);

            // Act
            var result = await _controller.PutCommander(1, 1 , 1, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutCommanders_ReturnsNotFoundResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Commander>>();
            var _controller = new CommanderController(mockRepository.Object);

            var aPourTailles = new Commander { IdCommande = 1, IdConfigurationMoto = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(aPourTailles);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Commander>(), It.IsAny<Commander>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutCommander(1, 1, 1, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        #endregion

        #region Test PostCommanderTestAsync

        /// <summary>
        /// Teste la méthode PostCommander pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostCommanderTestAsync()
        {
            // Arrange : préparation des données attendues
            Commander option = new Commander
            {
                IdEquipement = 1,
                IdCommande = 81,
                IdConfigurationMoto = 7,
            };

            // Act : appel de la méthode à tester
            var result = controller.PostCommander(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Commander? optionRecupere = context.Commanders
                .Where(u => u.IdEquipement == option.IdEquipement && u.IdCommande == option.IdCommande && option.IdConfigurationMoto == u.IdConfigurationMoto)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdEquipement = optionRecupere.IdEquipement;
            option.IdCommande = optionRecupere.IdCommande;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.Commanders.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostCommander en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostCommanderTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Commander>>();
            var userController = new CommanderController(mockRepository.Object);



            // Arrange : préparation des données attendues
            Commander option = new Commander
            {
                IdEquipement = 1,
                IdCommande = 7,
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostCommander(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Commander>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Commander), "Pas un Utilisateur");

            option.IdEquipement = ((Commander)result.Value).IdEquipement;
            option.IdCommande = ((Commander)result.Value).IdCommande;
            Assert.AreEqual(option, (Commander)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteCommanderTest
        /// <summary>
        /// Teste la méthode DeleteCommander pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteCommanderTest()
        {
            // Arrange : préparation des données attendues
            Commander option = new Commander
            {
                IdEquipement = 1,
                IdCommande = 81,
                IdConfigurationMoto = 7,
            };
            context.Commanders.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            Commander option1 = context.Commanders.FirstOrDefault(u => u.IdCommande == option.IdCommande && u.IdEquipement == option.IdEquipement);
            _ = controller.DeleteCommander(option.IdCommande, (Int32)option.IdEquipement, (Int32)option.IdConfigurationMoto).Result;

            // Arrange : préparation des données attendues
            Commander res = context.Commanders.FirstOrDefault(u => u.IdEquipement == option.IdEquipement && u.IdCommande == option.IdCommande);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteCommander en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteCommanderTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            Commander option = new Commander
            {
                IdEquipement = 1,
                IdCommande = 7,
                IdConfigurationMoto = 7,
            };
            var mockRepository = new Mock<IDataRepository<Commander>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdCommande, (Int32)option.IdEquipement, (Int32)option.IdConfigurationMoto).Result).Returns(option);
            var userController = new CommanderController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteCommander(option.IdCommande, (Int32)option.IdEquipement, (Int32)option.IdConfigurationMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}