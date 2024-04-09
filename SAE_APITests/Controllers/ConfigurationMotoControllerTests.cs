using Microsoft.AspNetCore.Mvc;
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
    public class ConfigurationConfigurationMotoControllerTests
    {

        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private ConfigurationMotoController controller;
        private BMWDBContext context;
        private IDataRepository<ConfigurationMoto> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur ConfigurationMoto.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new ConfigurationMotoManager(context);
            controller = new ConfigurationMotoController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur ConfigurationMotoController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void ConfigurationMotoControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new ConfigurationMotoManager(context);

            // Act : appel de la méthode à tester
            var option = new ConfigurationMotoController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetConfigurationMotoTest

        /// <summary>
        /// Teste la méthode GetConfigurationMotos pour vérifier qu'elle retourne la liste correcte des éléments ConfigurationMoto.
        /// </summary>
        [TestMethod()]
        public void GetConfigurationMotoTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<ConfigurationMoto> expected = context.ConfigurationMotos.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetConfigMotos().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetConfigurationMotoByIdTest

        /// <summary>
        /// Teste la méthode GetConfigurationMotoById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetConfigurationMotoByIdTest()
        {
            // Arrange : préparation des données attendues
            ConfigurationMoto expected = context.ConfigurationMotos.Find(1);
            // Act : appel de la méthode à tester
            var res = controller.GetConfigMotoById(expected.IdConfigurationMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetConfigurationMotoById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetConfigurationMotoByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<ConfigurationMoto>>();

            ConfigurationMoto option = new ConfigurationMoto
            {
                IdConfigurationMoto = 1,
                IdColoris = 1,
                IdMoto = 1,
                IdReservationOffre = 1,

            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdConfigurationMoto).Result).Returns(option);
            var userController = new ConfigurationMotoController(mockRepository.Object);
            var actionResult = userController.GetConfigMotoById(option.IdConfigurationMoto).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as ConfigurationMoto);
        }
        #endregion

        #region Test PutConfigurationMotoTestAsync
        /// <summary>
        /// Teste la méthode PutConfigurationMoto pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutConfigurationMotoTestAsync_ReturnsBadRequest()
        {
            //Arrange
            ConfigurationMoto option = new ConfigurationMoto
            {
                IdConfigurationMoto = 1,
                IdColoris = 1,
                IdMoto = 1,
                IdReservationOffre = 1,

            };

            // Act : appel de la méthode à tester
            var result = await controller.PutConfigMoto(3, option);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutConfigurationMoto en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutConfigurationMotoTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<ConfigurationMoto>>();
            var _controller = new ConfigurationMotoController(mockRepository.Object);
            var ConfigurationMoto = new ConfigurationMoto { IdConfigurationMoto = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(ConfigurationMoto);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<ConfigurationMoto>(), It.IsAny<ConfigurationMoto>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutConfigMoto(1, ConfigurationMoto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task PutConfigurationMoto_ReturnsNotFound_WhenConfigurationMotoDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<ConfigurationMoto>>();
            var _controller = new ConfigurationMotoController(mockRepository.Object);
            var ConfigurationMoto = new ConfigurationMoto { IdConfigurationMoto = 1000 };
            mockRepository.Setup(x => x.GetByIdAsync(1000)).ReturnsAsync((ConfigurationMoto)null);

            // Act
            var result = await _controller.PutConfigMoto(1000, ConfigurationMoto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
        }
        #endregion

        #region Test PostConfigurationMotoTestAsync

        /// <summary>
        /// Teste la méthode PostConfigurationMoto pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostConfigurationMotoTestAsync()
        {
            // Arrange : préparation des données attendues
            ConfigurationMoto option = new ConfigurationMoto
            {
                IdConfigurationMoto = 120,
                IdColoris = 1,
                IdMoto = 1,
                IdReservationOffre = 1,

            };

            // Act : appel de la méthode à tester
            var result = controller.PostConfigMoto(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            ConfigurationMoto? optionRecupere = context.ConfigurationMotos
                .Where(u => u.IdConfigurationMoto == option.IdConfigurationMoto)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdConfigurationMoto = optionRecupere.IdConfigurationMoto;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.ConfigurationMotos.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostConfigurationMoto en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostConfigurationMotoTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<ConfigurationMoto>>();
            var userController = new ConfigurationMotoController(mockRepository.Object);



            // Arrange : préparation des données attendues
            ConfigurationMoto option = new ConfigurationMoto
            {
                IdConfigurationMoto = 120,
                IdColoris = 1,
                IdMoto = 1,
                IdReservationOffre = 1,

            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostConfigMoto(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<ConfigurationMoto>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as OkObjectResult;
            Assert.IsInstanceOfType(result.Value, typeof(ConfigurationMoto), "Pas un Utilisateur");

            option.IdConfigurationMoto = ((ConfigurationMoto)result.Value).IdConfigurationMoto;
            Assert.AreEqual(option, (ConfigurationMoto)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteConfigurationMotoTest
        /// <summary>
        /// Teste la méthode DeleteConfigurationMoto pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteConfigurationMotoTest()
        {
            // Arrange : préparation des données attendues
            ConfigurationMoto option = new ConfigurationMoto
            {
                IdColoris = 1,
                IdMoto = 1,
                IdReservationOffre = 1,

            };

            context.ConfigurationMotos.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            ConfigurationMoto option1 = context.ConfigurationMotos.FirstOrDefault(u => u.IdConfigurationMoto == option.IdConfigurationMoto);
            _ = controller.DeleteConfigMoto(option.IdConfigurationMoto).Result;

            // Arrange : préparation des données attendues
            ConfigurationMoto res = context.ConfigurationMotos.FirstOrDefault(u => u.IdConfigurationMoto == option.IdConfigurationMoto);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteConfigurationMoto en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteConfigurationMotoTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            ConfigurationMoto option = new ConfigurationMoto
            {
                IdConfigurationMoto = 1,
                IdColoris = 1,
                IdMoto = 1,
                IdReservationOffre = 1,

            };

            var mockRepository = new Mock<IDataRepository<ConfigurationMoto>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdConfigurationMoto).Result).Returns(option);
            var userController = new ConfigurationMotoController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteConfigMoto(option.IdConfigurationMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}