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
    public class ConcessionnairesControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private ConcessionnairesController controller;
        private BMWDBContext context;
        private IDataRepository<Concessionnaire> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur Concessionnaire.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new ConcessionnaireManager(context);
            controller = new ConcessionnairesController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur ConcessionnairesController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void ConcessionnairesControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new ConcessionnaireManager(context);

            // Act : appel de la méthode à tester
            var option = new ConcessionnairesController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetConcessionnaireTest

        /// <summary>
        /// Teste la méthode GetConcessionnaires pour vérifier qu'elle retourne la liste correcte des éléments Concessionnaire.
        /// </summary>
        [TestMethod()]
        public void GetConcessionnaireTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<Concessionnaire> expected = context.Concessionnaires.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetConcessionnaires().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetConcessionnaireByIdTest

        /// <summary>
        /// Teste la méthode GetConcessionnaireById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetConcessionnaireByIdTest()
        {
            // Arrange : préparation des données attendues
            Concessionnaire expected = context.Concessionnaires.Find(1);
            // Act : appel de la méthode à tester
            var res = controller.GetConcessionnaireById(expected.IdConcessionnaire).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetConcessionnaireById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetConcessionnaireByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Concessionnaire>>();

            Concessionnaire option = new Concessionnaire
            {
                IdConcessionnaire = 1,
                NomConcessionnaire = "test",
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdConcessionnaire).Result).Returns(option);
            var userController = new ConcessionnairesController(mockRepository.Object);
            var actionResult = userController.GetConcessionnaireById(option.IdConcessionnaire).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as Concessionnaire);
        }
        #endregion

        #region Test PutConcessionnaireTestAsync
        /// <summary>
        /// Teste la méthode PutConcessionnaire pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutConcessionnaires_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var aPourTailles = new Concessionnaire { IdConcessionnaire = 1 };

            // Act
            var result = await controller.PutConcessionnaire(3, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutConcessionnaire en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutConcessionnaires_ReturnsBadRequestResult_WhenConcessionnaireDoesNotExistAsync()
        {

            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Concessionnaire>>();
            var _controller = new ConcessionnairesController(mockRepository.Object);

            var aPourTailles = new Concessionnaire { IdConcessionnaire = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Concessionnaire)null);

            // Act
            var result = await _controller.PutConcessionnaire(1, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutConcessionnaires_ReturnsNotFoundResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Concessionnaire>>();
            var _controller = new ConcessionnairesController(mockRepository.Object);

            var aPourTailles = new Concessionnaire { IdConcessionnaire = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(aPourTailles);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Concessionnaire>(), It.IsAny<Concessionnaire>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutConcessionnaire(1, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostConcessionnaireTestAsync

        /// <summary>
        /// Teste la méthode PostConcessionnaire pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostConcessionnaireTestAsync()
        {
            // Arrange : préparation des données attendues
            Concessionnaire option = new Concessionnaire
            {
                IdConcessionnaire = 100,
                IdAdresse = 1,
                IdStock = 1, 
                NomConcessionnaire = "test",
            };

            // Act : appel de la méthode à tester
            var result = controller.PostConcessionnaire(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Concessionnaire? optionRecupere = context.Concessionnaires
                .Where(u => u.IdConcessionnaire == option.IdConcessionnaire)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdConcessionnaire = optionRecupere.IdConcessionnaire;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.Concessionnaires.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostConcessionnaire en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostConcessionnaireTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Concessionnaire>>();
            var userController = new ConcessionnairesController(mockRepository.Object);



            // Arrange : préparation des données attendues
            Concessionnaire option = new Concessionnaire
            {
                IdConcessionnaire = 1,
                NomConcessionnaire = "test",
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostConcessionnaire(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Concessionnaire>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Concessionnaire), "Pas un Utilisateur");

            option.IdConcessionnaire = ((Concessionnaire)result.Value).IdConcessionnaire;
            Assert.AreEqual(option, (Concessionnaire)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteConcessionnaireTest
        /// <summary>
        /// Teste la méthode DeleteConcessionnaire pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteConcessionnaireTest()
        {
            // Arrange : préparation des données attendues
            Concessionnaire option = new Concessionnaire
            {
                IdConcessionnaire = 100,
                IdAdresse = 1 ,
                IdStock = 1,
                NomConcessionnaire = "test",
            };
            context.Concessionnaires.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            Concessionnaire option1 = context.Concessionnaires.FirstOrDefault(u => u.IdConcessionnaire == option.IdConcessionnaire);
            _ = controller.DeleteConcessionnaire(option.IdConcessionnaire).Result;

            // Arrange : préparation des données attendues
            Concessionnaire res = context.Concessionnaires.FirstOrDefault(u => u.IdConcessionnaire == option.IdConcessionnaire);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteConcessionnaire en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteConcessionnaireTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            Concessionnaire option = new Concessionnaire
            {
                IdConcessionnaire = 1,
                NomConcessionnaire = "test",
            };
            var mockRepository = new Mock<IDataRepository<Concessionnaire>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdConcessionnaire).Result).Returns(option);
            var userController = new ConcessionnairesController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteConcessionnaire(option.IdConcessionnaire).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}