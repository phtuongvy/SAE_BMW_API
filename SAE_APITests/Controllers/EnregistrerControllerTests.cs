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
    public class EnregistrerControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private EnregistrerController controller;
        private BMWDBContext context;
        private IDataRepository<Enregistrer> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur Enregistrer.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new EnregistrerManager(context);
            controller = new EnregistrerController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur EnregistrerController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void EnregistrerControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new EnregistrerManager(context);

            // Act : appel de la méthode à tester
            var option = new EnregistrerController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetEnregistrerTest

        /// <summary>
        /// Teste la méthode GetEnregistrers pour vérifier qu'elle retourne la liste correcte des éléments Enregistrer.
        /// </summary>
        [TestMethod()]
        public void GetEnregistrerTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<Enregistrer> expected = context.Enregistrers.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetEnregistrers().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetEnregistrerByIdTest

        /// <summary>
        /// Teste la méthode GetEnregistrerById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetEnregistrerByIdTest()
        {
            // Arrange : préparation des données attendues
            Enregistrer expected = context.Enregistrers.Find(1, 1);
            // Act : appel de la méthode à tester
            var res = controller.GetEnregistrerById(expected.IdConfigurationMoto, expected.IdCompteClient).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetEnregistrerById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetEnregistrerByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Enregistrer>>();

            Enregistrer option = new Enregistrer
            {
                IdConfigurationMoto = 1,
                IdCompteClient = 1,
                NomConfiguration = "test"
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdConfigurationMoto, option.IdCompteClient).Result).Returns(option);
            var userController = new EnregistrerController(mockRepository.Object);
            var actionResult = userController.GetEnregistrerById(option.IdConfigurationMoto, option.IdCompteClient).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as Enregistrer);
        }
        #endregion

        #region Test PutEnregistrerTestAsync
        /// <summary>
        /// Teste la méthode PutEnregistrer pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutEnregistrerTestAsync_ReturnsBadRequest()
        {
            //Arrange
            Enregistrer Enregistrer = new Enregistrer
            {
                IdConfigurationMoto = 40,
                IdCompteClient = 7,
                NomConfiguration = "test"
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutEnregistrer(3, 4, Enregistrer);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutEnregistrer en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutEnregistrerTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Enregistrer>>();
            var _controller = new EnregistrerController(mockRepository.Object);
            var Enregistrer = new Enregistrer { IdConfigurationMoto = 1, IdCompteClient = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1, 1)).ReturnsAsync(Enregistrer);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Enregistrer>(), It.IsAny<Enregistrer>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutEnregistrer(1, 1, Enregistrer);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutEnregistrer_ReturnsNotFound_WhenEnregistrerDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Enregistrer>>();
            var _controller = new EnregistrerController(mockRepository.Object);
            var Enregistrer = new Enregistrer { IdConfigurationMoto = 1000, IdCompteClient = 1000 };
            mockRepository.Setup(x => x.GetByIdAsync(1000, 1000)).ReturnsAsync((Enregistrer)null);

            // Act
            var result = await _controller.PutEnregistrer(1000, 1000, Enregistrer);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostEnregistrerTestAsync

        /// <summary>
        /// Teste la méthode PostEnregistrer pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostEnregistrerTestAsync()
        {
            // Arrange : préparation des données attendues
            Enregistrer option = new Enregistrer
            {
                IdConfigurationMoto = 1,
                IdCompteClient = 7,
                NomConfiguration = "test"
            };

            // Act : appel de la méthode à tester
            var result = controller.PostEnregistrer(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Enregistrer? optionRecupere = context.Enregistrers
                .Where(u => u.IdConfigurationMoto == option.IdConfigurationMoto && u.IdCompteClient == option.IdCompteClient)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdConfigurationMoto = optionRecupere.IdConfigurationMoto;
            option.IdCompteClient = optionRecupere.IdCompteClient;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.Enregistrers.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostEnregistrer en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostEnregistrerTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Enregistrer>>();
            var userController = new EnregistrerController(mockRepository.Object);



            // Arrange : préparation des données attendues
            Enregistrer option = new Enregistrer
            {
                IdConfigurationMoto = 1,
                IdCompteClient = 7,
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostEnregistrer(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Enregistrer>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Enregistrer), "Pas un Utilisateur");

            option.IdConfigurationMoto = ((Enregistrer)result.Value).IdConfigurationMoto;
            option.IdCompteClient = ((Enregistrer)result.Value).IdCompteClient;
            Assert.AreEqual(option, (Enregistrer)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteEnregistrerTest
        /// <summary>
        /// Teste la méthode DeleteEnregistrer pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteEnregistrerTest()
        {
            // Arrange : préparation des données attendues
            Enregistrer option = new Enregistrer
            {
                IdConfigurationMoto = 1,
                IdCompteClient = 7,
                NomConfiguration = "test"
            };
            context.Enregistrers.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            Enregistrer option1 = context.Enregistrers.FirstOrDefault(u => u.IdCompteClient == option.IdCompteClient && u.IdConfigurationMoto == option.IdConfigurationMoto);
            _ = controller.DeleteEnregistrer(option.IdConfigurationMoto, option.IdCompteClient).Result;

            // Arrange : préparation des données attendues
            Enregistrer res = context.Enregistrers.FirstOrDefault(u => u.IdConfigurationMoto == option.IdConfigurationMoto && u.IdCompteClient == option.IdCompteClient);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteEnregistrer en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteEnregistrerTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            Enregistrer option = new Enregistrer
            {
                IdConfigurationMoto = 1,
                IdCompteClient = 7,
            };
            var mockRepository = new Mock<IDataRepository<Enregistrer>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdConfigurationMoto, option.IdCompteClient).Result).Returns(option);
            var userController = new EnregistrerController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteEnregistrer(option.IdConfigurationMoto, option.IdCompteClient).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}