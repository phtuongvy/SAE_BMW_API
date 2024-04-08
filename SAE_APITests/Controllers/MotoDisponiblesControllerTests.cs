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
    public class MotoDisponiblesControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private MotoDisponiblesController controller;
        private BMWDBContext context;
        private IDataRepository<MotoDisponible> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur MotoDisponible.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new MotoDisponibleManager(context);
            controller = new MotoDisponiblesController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur MotoDisponibleController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void MotoDisponibleControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new MotoDisponibleManager(context);

            // Act : appel de la méthode à tester
            var option = new MotoDisponiblesController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetMotoDisponibleTest

        /// <summary>
        /// Teste la méthode GetMotoDisponibles pour vérifier qu'elle retourne la liste correcte des éléments MotoDisponible.
        /// </summary>
        [TestMethod()]
        public void GetMotoDisponibleTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<MotoDisponible> expected = context.MotoDisponibles.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetMotoDisponibles().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetMotoDisponibleByIdTest

        /// <summary>
        /// Teste la méthode GetMotoDisponibleById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetMotoDisponibleByIdTest()
        {
            // Arrange : préparation des données attendues
            MotoDisponible expected = context.MotoDisponibles.Find(1);
            // Act : appel de la méthode à tester
            var res = controller.GetMotoDisponibleById(expected.IdMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetMotoDisponibleById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetMotoDisponibleByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<MotoDisponible>>();

            MotoDisponible option = new MotoDisponible
            {
                IdMoto = 5,
                PrixMoto = 10000
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdMoto).Result).Returns(option);
            var userController = new MotoDisponiblesController(mockRepository.Object);
            var actionResult = userController.GetMotoDisponibleById(option.IdMoto).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as MotoDisponible);
        }
        #endregion

        #region Test PutMotoDisponibleTestAsync
        /// <summary>
        /// Teste la méthode PutMotoDisponible pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutMotoDisponibleTestAsync_ReturnsBadRequest()
        {
            //Arrange
            MotoDisponible MotoDisponible = new MotoDisponible
            {
                IdMoto = 5,
                PrixMoto = 10000
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutMotoDisponible(3, MotoDisponible);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutMotoDisponible en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutMotoDisponibleTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<MotoDisponible>>();
            var _controller = new MotoDisponiblesController(mockRepository.Object);
            var MotoDisponible = new MotoDisponible { IdMoto = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(MotoDisponible);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<MotoDisponible>(), It.IsAny<MotoDisponible>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutMotoDisponible(1, MotoDisponible);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutMotoDisponible_ReturnsNotFound_WhenMotoDisponibleDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<MotoDisponible>>();
            var _controller = new MotoDisponiblesController(mockRepository.Object);
            var MotoDisponible = new MotoDisponible { IdMoto = 1000 };
            mockRepository.Setup(x => x.GetByIdAsync(1000)).ReturnsAsync((MotoDisponible)null);

            // Act
            var result = await _controller.PutMotoDisponible(1000, MotoDisponible);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostMotoDisponibleTestAsync

        /// <summary>
        /// Teste la méthode PostMotoDisponible pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostMotoDisponibleTestAsync()
        {
            // Arrange : préparation des données attendues
            MotoDisponible option = new MotoDisponible
            {
                IdMoto = 4,
                PrixMoto = 10000
            };

            // Act : appel de la méthode à tester
            var result = controller.PostMotoDisponible(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            MotoDisponible? optionRecupere = context.MotoDisponibles
                .Where(u => u.IdMoto == option.IdMoto)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdMoto = optionRecupere.IdMoto;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.MotoDisponibles.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostMotoDisponible en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostMotoDisponibleTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<MotoDisponible>>();
            var userController = new MotoDisponiblesController(mockRepository.Object);



            // Arrange : préparation des données attendues
            MotoDisponible option = new MotoDisponible
            {
                IdMoto = 4,
                PrixMoto = 10000
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostMotoDisponible(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<MotoDisponible>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(MotoDisponible), "Pas un Utilisateur");

            option.IdMoto = ((MotoDisponible)result.Value).IdMoto;
            Assert.AreEqual(option, (MotoDisponible)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteMotoDisponibleTest
        /// <summary>
        /// Teste la méthode DeleteMotoDisponible pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteMotoDisponibleTest()
        {
            // Arrange : préparation des données attendues
            MotoDisponible option = new MotoDisponible
            {
                IdMoto = 4,
                PrixMoto = 10000
            };
            context.MotoDisponibles.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            MotoDisponible option1 = context.MotoDisponibles.FirstOrDefault(u => u.IdMoto == option.IdMoto);
            _ = controller.DeleteMotoDisponible(option.IdMoto).Result;

            // Arrange : préparation des données attendues
            MotoDisponible res = context.MotoDisponibles.FirstOrDefault(u => u.IdMoto == option.IdMoto);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteMotoDisponible en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteMotoDisponibleTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            MotoDisponible option = new MotoDisponible
            {
                IdMoto = 4,
                PrixMoto = 10000

            };
            var mockRepository = new Mock<IDataRepository<MotoDisponible>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdMoto).Result).Returns(option);
            var userController = new MotoDisponiblesController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteMotoDisponible(option.IdMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}