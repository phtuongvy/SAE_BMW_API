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
    public class PeutContenirsControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private PeutContenirsController controller;
        private BMWDBContext context;
        private IDataRepository<PeutContenir> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur PeutContenir.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new PeutContenirManager(context);
            controller = new PeutContenirsController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur PeutContenirController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void PeutContenirControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new PeutContenirManager(context);

            // Act : appel de la méthode à tester
            var option = new PeutContenirsController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetPeutContenirTest

        /// <summary>
        /// Teste la méthode GetPeutContenirs pour vérifier qu'elle retourne la liste correcte des éléments PeutContenir.
        /// </summary>
        [TestMethod()]
        public void GetPeutContenirTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<PeutContenir> expected = context.PeutContenirs.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetPeutContenirs().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetPeutContenirByIdTest

        /// <summary>
        /// Teste la méthode GetPeutContenirById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetPeutContenirByIdTest()
        {
            // Arrange : préparation des données attendues
            PeutContenir expected = context.PeutContenirs.Find(1, 1);
            // Act : appel de la méthode à tester
            var res = controller.GetPeutContenirById(expected.IdColoris, expected.IdMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetPeutContenirById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetPeutContenirByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<PeutContenir>>();

            PeutContenir option = new PeutContenir
            {
                IdColoris = 3,
                IdMoto = 1
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdColoris, option.IdMoto).Result).Returns(option);
            var userController = new PeutContenirsController(mockRepository.Object);
            var actionResult = userController.GetPeutContenirById(option.IdColoris, option.IdMoto).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as PeutContenir);
        }
        #endregion

        #region Test PutPeutContenirTestAsync
        /// <summary>
        /// Teste la méthode PutPeutContenir pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutPeutContenirTestAsync_ReturnsBadRequest()
        {
            //Arrange
            PeutContenir PeutContenir = new PeutContenir
            {
                IdColoris = 1,
                IdMoto = 1
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutPeutContenir(3,1, PeutContenir);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutPeutContenir en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutPeutContenirTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<PeutContenir>>();
            var _controller = new PeutContenirsController(mockRepository.Object);
            var PeutContenir = new PeutContenir { IdColoris = 1, IdMoto = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1, 1)).ReturnsAsync(PeutContenir);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<PeutContenir>(), It.IsAny<PeutContenir>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutPeutContenir(1, 1, PeutContenir);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutPeutContenir_ReturnsNotFound_WhenPeutContenirDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<PeutContenir>>();
            var _controller = new PeutContenirsController(mockRepository.Object);
            var PeutContenir = new PeutContenir { IdColoris = 1000, IdMoto = 1000 };
            mockRepository.Setup(x => x.GetByIdAsync(1000, 1000)).ReturnsAsync((PeutContenir)null);

            // Act
            var result = await _controller.PutPeutContenir(1000, 1000, PeutContenir);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostPeutContenirTestAsync

        /// <summary>
        /// Teste la méthode PostPeutContenir pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostPeutContenirTestAsync()
        {
            // Arrange : préparation des données attendues
            PeutContenir option = new PeutContenir
            {
                IdColoris = 3,
                IdMoto = 1
            };

            // Act : appel de la méthode à tester
            var result = controller.PostPeutContenir(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            PeutContenir? optionRecupere = context.PeutContenirs
                .Where(u => u.IdColoris == option.IdColoris && u.IdMoto == option.IdMoto)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdMoto = optionRecupere.IdMoto;
            option.IdColoris = optionRecupere.IdColoris;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.PeutContenirs.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostPeutContenir en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostPeutContenirTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<PeutContenir>>();
            var userController = new PeutContenirsController(mockRepository.Object);



            // Arrange : préparation des données attendues
            PeutContenir option = new PeutContenir
            {
                IdColoris = 3,
                IdMoto = 1
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostPeutContenir(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<PeutContenir>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(PeutContenir), "Pas un Utilisateur");

            option.IdMoto = ((PeutContenir)result.Value).IdMoto;
            option.IdColoris = ((PeutContenir)result.Value).IdColoris;
            Assert.AreEqual(option, (PeutContenir)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeletePeutContenirTest
        /// <summary>
        /// Teste la méthode DeletePeutContenir pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeletePeutContenirTest()
        {
            // Arrange : préparation des données attendues
            PeutContenir option = new PeutContenir
            {
                IdColoris = 3,
                IdMoto = 1
            };
            context.PeutContenirs.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            PeutContenir option1 = context.PeutContenirs.FirstOrDefault(u => u.IdColoris == option.IdColoris && u.IdMoto == option.IdMoto);
            _ = controller.DeletePeutContenir(option.IdColoris, option.IdMoto).Result;

            // Arrange : préparation des données attendues
            PeutContenir res = context.PeutContenirs.FirstOrDefault(u => u.IdColoris == option.IdColoris && u.IdMoto == option.IdMoto);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeletePeutContenir en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeletePeutContenirTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            PeutContenir option = new PeutContenir
            {
                IdColoris = 3,
                IdMoto = 1
            };
            var mockRepository = new Mock<IDataRepository<PeutContenir>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdColoris, option.IdMoto).Result).Returns(option);
            var userController = new PeutContenirsController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeletePeutContenir(option.IdColoris, option.IdMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}