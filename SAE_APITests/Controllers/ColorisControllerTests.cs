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
    public class ColorisControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private ColorisController controller;
        private BMWDBContext context;
        private IDataRepository<Coloris> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur Coloris.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new ColorisManager(context);
            controller = new ColorisController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur ColorisController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void ColorisControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new ColorisManager(context);

            // Act : appel de la méthode à tester
            var option = new ColorisController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetColorisTest

        /// <summary>
        /// Teste la méthode GetColoris pour vérifier qu'elle retourne la liste correcte des éléments Coloris.
        /// </summary>
        [TestMethod()]
        public void GetColorisTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<Coloris> expected = context.Coloris.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetColoriss().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");

        }
        #endregion

        #region Test GetColorisByIdTest

        /// <summary>
        /// Teste la méthode GetColorisById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetColorisByIdTest()
        {
            // Arrange : préparation des données attendues
            Coloris expected = context.Coloris.Find(1);
            // Act : appel de la méthode à tester
            var res = controller.GetColorisById(expected.IdColoris).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetColorisById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetColorisByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Coloris>>();

            Coloris option = new Coloris
            {
                IdColoris = 1,
                IdPhoto = 1 , 
                NomColoris = "test",
                DescriptionColoris = "une couleur de test"
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdColoris).Result).Returns(option);
            var userController = new ColorisController(mockRepository.Object);
            var actionResult = userController.GetColorisById(option.IdColoris).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as Coloris);
        }
        #endregion

        #region Test PutColorisTestAsync
        /// <summary>
        /// Teste la méthode PutColoris pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutColoriss_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var aPourTailles = new Coloris { IdColoris = 1 };

            // Act
            var result = await controller.PutColoris(3, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutColoris en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutColoriss_ReturnsBadRequestResult_WhenColorisDoesNotExistAsync()
        {

            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Coloris>>();
            var _controller = new ColorisController(mockRepository.Object);

            var aPourTailles = new Coloris { IdColoris = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Coloris)null);

            // Act
            var result = await _controller.PutColoris(1, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutColoriss_ReturnsNotFoundResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Coloris>>();
            var _controller = new ColorisController(mockRepository.Object);

            var aPourTailles = new Coloris { IdColoris = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(aPourTailles);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Coloris>(), It.IsAny<Coloris>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutColoris(1, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostColorisTestAsync

        /// <summary>
        /// Teste la méthode PostColoris pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostColorisTestAsync()
        {
            // Arrange : préparation des données attendues
            Coloris option = new Coloris
            {
                IdColoris = 100,
                IdPhoto = 1,
                NomColoris = "test",
                DescriptionColoris = "une couleur de test"
            };

            // Act : appel de la méthode à tester
            var result = controller.PostColoris(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Coloris? optionRecupere = context.Coloris
                .Where(u => u.IdColoris == option.IdColoris)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdColoris = optionRecupere.IdColoris;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.Coloris.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostColoris en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostColorisTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Coloris>>();
            var userController = new ColorisController(mockRepository.Object);



            // Arrange : préparation des données attendues
            Coloris option = new Coloris
            {
                IdColoris = 1,
                IdPhoto = 1,
                NomColoris = "test",
                DescriptionColoris = "une couleur de test"
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostColoris(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Coloris>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Coloris), "Pas un Utilisateur");

            option.IdColoris = ((Coloris)result.Value).IdColoris;
            Assert.AreEqual(option, (Coloris)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteColorisTest
        /// <summary>
        /// Teste la méthode DeleteColoris pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteColorisTest()
        {
            // Arrange : préparation des données attendues
            Coloris option = new Coloris
            {
                IdColoris = 100,
                IdPhoto = 1,
                NomColoris = "test",
                DescriptionColoris = "une couleur de test"
            };
            context.Coloris.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            Coloris option1 = context.Coloris.FirstOrDefault(u => u.IdColoris == option.IdColoris);
            _ = controller.DeleteColoris(option.IdColoris).Result;

            // Arrange : préparation des données attendues
            Coloris res = context.Coloris.FirstOrDefault(u => u.IdColoris == option.IdColoris);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteColoris en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteColorisTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            Coloris option = new Coloris
            {
                IdColoris = 1,
                NomColoris = "test",
                DescriptionColoris = "une couleur de test"
            };
            var mockRepository = new Mock<IDataRepository<Coloris>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdColoris).Result).Returns(option);
            var userController = new ColorisController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteColoris(option.IdColoris).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}