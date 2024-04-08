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
    public class DateLivraisonControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private DateLivraisonController controller;
        private BMWDBContext context;
        private IDataRepository<DateLivraison> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur DateLivraison.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new DateLivraisonManager(context);
            controller = new DateLivraisonController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur DateLivraisonController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void DateLivraisonControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new DateLivraisonManager(context);

            // Act : appel de la méthode à tester
            var option = new DateLivraisonController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetDateLivraisonTest

        /// <summary>
        /// Teste la méthode GetDateLivraisons pour vérifier qu'elle retourne la liste correcte des éléments DateLivraison.
        /// </summary>
        [TestMethod()]
        public void GetDateLivraisonTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<DateLivraison> expected = context.DateLivraisons.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetDateLivraisons().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetDateLivraisonByIdTest

        /// <summary>
        /// Teste la méthode GetDateLivraisonById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetDateLivraisonByIdTest()
        {
            // Arrange : préparation des données attendues
            DateLivraison expected = context.DateLivraisons.Find(1);
            // Act : appel de la méthode à tester
            var res = controller.GetDateLivraisonById(expected.IdDateLivraison).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetDateLivraisonById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetDateLivraisonByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<DateLivraison>>();

            DateLivraison option = new DateLivraison
            {
                IdDateLivraison = 1,
               
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdDateLivraison).Result).Returns(option);
            var userController = new DateLivraisonController(mockRepository.Object);
            var actionResult = userController.GetDateLivraisonById(option.IdDateLivraison).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as DateLivraison);
        }
        #endregion

        #region Test PutDateLivraisonTestAsync
        /// <summary>
        /// Teste la méthode PutDateLivraison pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutDateLivraisons_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var aPourTailles = new DateLivraison { IdDateLivraison = 1 };

            // Act
            var result = await controller.PutDateLivraison(3, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutDateLivraison en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutDateLivraisons_ReturnsBadRequestResult_WhenDateLivraisonDoesNotExistAsync()
        {

            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<DateLivraison>>();
            var _controller = new DateLivraisonController(mockRepository.Object);

            var aPourTailles = new DateLivraison { IdDateLivraison = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((DateLivraison)null);

            // Act
            var result = await _controller.PutDateLivraison(1, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutDateLivraisons_ReturnsNotFoundResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<DateLivraison>>();
            var _controller = new DateLivraisonController(mockRepository.Object);

            var aPourTailles = new DateLivraison { IdDateLivraison = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(aPourTailles);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<DateLivraison>(), It.IsAny<DateLivraison>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutDateLivraison(1, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostDateLivraisonTestAsync

        /// <summary>
        /// Teste la méthode PostDateLivraison pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostDateLivraisonTestAsync()
        {
            // Arrange : préparation des données attendues
            DateLivraison option = new DateLivraison
            {
                IdDateLivraison = 100,
                
            };

            // Act : appel de la méthode à tester
            var result = controller.PostDateLivraison(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            DateLivraison? optionRecupere = context.DateLivraisons
                .Where(u => u.IdDateLivraison == option.IdDateLivraison)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdDateLivraison = optionRecupere.IdDateLivraison;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.DateLivraisons.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostDateLivraison en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostDateLivraisonTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<DateLivraison>>();
            var userController = new DateLivraisonController(mockRepository.Object);



            // Arrange : préparation des données attendues
            DateLivraison option = new DateLivraison
            {
                IdDateLivraison = 1,
             
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostDateLivraison(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<DateLivraison>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(DateLivraison), "Pas un Utilisateur");

            option.IdDateLivraison = ((DateLivraison)result.Value).IdDateLivraison;
            Assert.AreEqual(option, (DateLivraison)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteDateLivraisonTest
        /// <summary>
        /// Teste la méthode DeleteDateLivraison pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteDateLivraisonTest()
        {
            // Arrange : préparation des données attendues
            DateLivraison option = new DateLivraison
            {
                IdDateLivraison = 100,
                
            };
            context.DateLivraisons.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            DateLivraison option1 = context.DateLivraisons.FirstOrDefault(u => u.IdDateLivraison == option.IdDateLivraison);
            _ = controller.DeleteDateLivraison(option.IdDateLivraison).Result;

            // Arrange : préparation des données attendues
            DateLivraison res = context.DateLivraisons.FirstOrDefault(u => u.IdDateLivraison == option.IdDateLivraison);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteDateLivraison en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteDateLivraisonTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            DateLivraison option = new DateLivraison
            {
                IdDateLivraison = 1,
                
            };
            var mockRepository = new Mock<IDataRepository<DateLivraison>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdDateLivraison).Result).Returns(option);
            var userController = new DateLivraisonController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteDateLivraison(option.IdDateLivraison).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}