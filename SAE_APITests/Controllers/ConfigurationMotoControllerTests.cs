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
    public class ConfigurationMotoControllerTests
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

        #region Test GetConfigMotosTest

        /// <summary>
        /// Teste la méthode GetConfigMotoss pour vérifier qu'elle retourne la liste correcte des éléments ConfigurationMoto.
        /// </summary>
        [TestMethod()]
        public void GetConfigMotosTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<ConfigurationMoto> expected = context.ConfigurationMotos.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetConfigMotos().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetConfigMotoByIdTest

        /// <summary>
        /// Teste la méthode GetConfigMotoById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetConfigMotoByIdTest()
        {
            // Arrange : préparation des données attendues
            ConfigurationMoto expected = context.ConfigurationMotos.Find(2);
            // Act : appel de la méthode à tester
            var res = controller.GetConfigMotoById(expected.IdConfigurationMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetConfigMotoById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetConfigMotoByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<ConfigurationMoto>>();

            ConfigurationMoto option = new ConfigurationMoto
            {
                IdConfigurationMoto = 1,
                IdColoris = 1, 
                IdMoto = 1 , 
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
        public async Task PutConfigurationMotoTestAsync()
        {
            //Arrange
            ConfigurationMoto optionAtester = new ConfigurationMoto
            {
                IdConfigurationMoto = 40,
                IdColoris = 1,
                IdMoto = 1,
                IdReservationOffre = 1,

            };

            ConfigurationMoto optionUptade = new ConfigurationMoto
            {
                IdConfigurationMoto = 40,
                IdColoris = 1,
                IdMoto = 2,
                IdReservationOffre = 1,

            };


            // Act : appel de la méthode à tester
            var res = await controller.PutConfigMoto(optionAtester.IdConfigurationMoto, optionUptade);

            // Arrange : préparation des données attendues
            var nouvelleoption = controller.GetConfigMotoById(optionUptade.IdConfigurationMoto).Result;
            Assert.AreEqual(optionUptade, res);

            context.ConfigurationMotos.Remove(optionUptade);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PutConfigurationMoto en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public void PutConfigurationMotoTestAvecMoq()
        {

            // Arrange : préparation des données attendues
            ConfigurationMoto optionToUpdate = new ConfigurationMoto
            {
                IdConfigurationMoto = 100,
                IdColoris = 1,
                IdMoto = 1,
                IdReservationOffre = 1,
            };
            ConfigurationMoto updatedOption = new ConfigurationMoto
            {
                IdConfigurationMoto = 100,
                IdColoris = 1,
                IdMoto = 1,
                IdReservationOffre = 1,
            };

            var mockRepository = new Mock<IDataRepository<ConfigurationMoto>>();
            mockRepository.Setup(repo => repo.GetByIdAsync(21000)).ReturnsAsync(optionToUpdate);
            mockRepository.Setup(repo => repo.UpdateAsync(optionToUpdate, updatedOption)).Returns(Task.CompletedTask);


            var controller = new ConfigurationMotoController(mockRepository.Object);

            // Act : appel de la méthode à tester
            var result = controller.PutConfigMoto(optionToUpdate.IdConfigurationMoto, updatedOption).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(result, typeof(ActionResult<ConfigurationMoto>), "La réponse n'est pas du type attendu ConfigurationMoto");
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
                IdConfigurationMoto = 1000,
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
                IdConfigurationMoto = 1000,
                IdColoris = 1,
                IdMoto = 1,
                IdReservationOffre = 1,
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostConfigMoto(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<ConfigurationMoto>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
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
                IdConfigurationMoto = 1000,
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