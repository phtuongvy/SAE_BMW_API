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
    public class EffectuersControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private EffectuersController controller;
        private BMWDBContext context;
        private IDataRepository<Effectuer> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur Effectuer.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new EffectuerManager(context);
            controller = new EffectuersController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur EffectuersController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void EffectuersControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new EffectuerManager(context);

            // Act : appel de la méthode à tester
            var option = new EffectuersController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetEffectuerTest

        /// <summary>
        /// Teste la méthode GetEffectuers pour vérifier qu'elle retourne la liste correcte des éléments Effectuer.
        /// </summary>
        [TestMethod()]
        public void GetEffectuerTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<Effectuer> expected = context.Effectuers.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetEffectuers().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetEffectuerByIdTest

        /// <summary>
        /// Teste la méthode GetEffectuerById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetEffectuerByIdTest()
        {
            // Arrange : préparation des données attendues
            Effectuer expected = context.Effectuers.Find(41, 1);
            // Act : appel de la méthode à tester
            var res = controller.GetEffectuerById(expected.IdCompteClient, expected.IdCommande).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetEffectuerById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetEffectuerByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Effectuer>>();

            Effectuer option = new Effectuer
            {
                IdCompteClient = 1,
                IdCommande = 1,
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdCompteClient, option.IdCommande).Result).Returns(option);
            var userController = new EffectuersController(mockRepository.Object);
            var actionResult = userController.GetEffectuerById(option.IdCompteClient, option.IdCommande).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as Effectuer);
        }
        #endregion

        #region Test PutEffectuerTestAsync
        /// <summary>
        /// Teste la méthode PutEffectuer pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutEffectuerTestAsync_ReturnsBadRequest()
        {
            //Arrange
            Effectuer Effectuer = new Effectuer
            {
                IdCompteClient = 40,
                IdCommande = 7,
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutEffectuer(3, 4, Effectuer);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutEffectuer en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutEffectuerTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Effectuer>>();
            var _controller = new EffectuersController(mockRepository.Object);
            var Effectuer = new Effectuer { IdCompteClient = 1, IdCommande = 2 };
            mockRepository.Setup(x => x.GetByIdAsync(1, 2)).ReturnsAsync(Effectuer);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Effectuer>(), It.IsAny<Effectuer>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutEffectuer(1, 2, Effectuer);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutEffectuer_ReturnsNotFound_WhenEffectuerDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Effectuer>>();
            var _controller = new EffectuersController(mockRepository.Object);
            var Effectuer = new Effectuer { IdCompteClient = 1000, IdCommande = 1000 };
            mockRepository.Setup(x => x.GetByIdAsync(1000, 1000)).ReturnsAsync((Effectuer)null);

            // Act
            var result = await _controller.PutEffectuer(1000, 1000, Effectuer);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostEffectuerTestAsync

        /// <summary>
        /// Teste la méthode PostEffectuer pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostEffectuerTestAsync()
        {
            // Arrange : préparation des données attendues
            Effectuer option = new Effectuer
            {
                IdCompteClient = 1,
                IdCommande = 7,
            };

            // Act : appel de la méthode à tester
            var result = controller.PostEffectuer(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Effectuer? optionRecupere = context.Effectuers
                .Where(u => u.IdCompteClient == option.IdCompteClient && u.IdCommande == option.IdCommande)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdCompteClient = optionRecupere.IdCompteClient;
            option.IdCommande = optionRecupere.IdCommande;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.Effectuers.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostEffectuer en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostEffectuerTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Effectuer>>();
            var userController = new EffectuersController(mockRepository.Object);



            // Arrange : préparation des données attendues
            Effectuer option = new Effectuer
            {
                IdCompteClient = 1,
                IdCommande = 7,
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostEffectuer(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Effectuer>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Effectuer), "Pas un Utilisateur");

            option.IdCompteClient = ((Effectuer)result.Value).IdCompteClient;
            option.IdCommande = ((Effectuer)result.Value).IdCommande;
            Assert.AreEqual(option, (Effectuer)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteEffectuerTest
        /// <summary>
        /// Teste la méthode DeleteEffectuer pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteEffectuerTest()
        {
            // Arrange : préparation des données attendues
            Effectuer option = new Effectuer
            {
                IdCompteClient = 1,
                IdCommande = 7,
            };
            context.Effectuers.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            Effectuer option1 = context.Effectuers.FirstOrDefault(u => u.IdCommande == option.IdCommande && u.IdCompteClient == option.IdCompteClient);
            _ = controller.DeleteEffectuer(option.IdCompteClient, option.IdCommande).Result;

            // Arrange : préparation des données attendues
            Effectuer res = context.Effectuers.FirstOrDefault(u => u.IdCompteClient == option.IdCompteClient && u.IdCommande == option.IdCommande);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteEffectuer en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteEffectuerTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            Effectuer option = new Effectuer
            {
                IdCompteClient = 1,
                IdCommande = 7,
            };
            var mockRepository = new Mock<IDataRepository<Effectuer>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdCompteClient, option.IdCommande).Result).Returns(option);
            var userController = new EffectuersController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteEffectuer(option.IdCompteClient, option.IdCommande).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}