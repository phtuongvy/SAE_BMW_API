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
    public class PeutEquipersControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private PeutEquipersController controller;
        private BMWDBContext context;
        private IDataRepository<PeutEquiper> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur PeutEquiper.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new PeutEquiperManager(context);
            controller = new PeutEquipersController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur PeutEquiperController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void PeutEquiperControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new PeutEquiperManager(context);

            // Act : appel de la méthode à tester
            var option = new PeutEquipersController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetPeutEquiperTest

        /// <summary>
        /// Teste la méthode GetPeutEquipers pour vérifier qu'elle retourne la liste correcte des éléments PeutEquiper.
        /// </summary>
        [TestMethod()]
        public void GetPeutEquiperTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<PeutEquiper> expected = context.PeutEquipers.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetPeutEquipers().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetPeutEquiperByIdTest

        /// <summary>
        /// Teste la méthode GetPeutEquiperById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetPeutEquiperByIdTest()
        {
            // Arrange : préparation des données attendues
            PeutEquiper expected = context.PeutEquipers.Find(1, 1);
            // Act : appel de la méthode à tester
            var res = controller.GetPeutEquiperById(expected.IdPack, expected.IdMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetPeutEquiperById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetPeutEquiperByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<PeutEquiper>>();

            PeutEquiper option = new PeutEquiper
            {
                IdPack = 1,
                IdMoto = 1
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdPack, option.IdMoto).Result).Returns(option);
            var userController = new PeutEquipersController(mockRepository.Object);
            var actionResult = userController.GetPeutEquiperById(option.IdPack, option.IdMoto).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as PeutEquiper);
        }
        #endregion

        #region Test PutPeutEquiperTestAsync
        /// <summary>
        /// Teste la méthode PutPeutEquiper pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutPeutEquiperTestAsync_ReturnsBadRequest()
        {
            //Arrange
            PeutEquiper PeutEquiper = new PeutEquiper
            {
                IdPack = 1,
                IdMoto = 1
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutPeutEquiper(3, 4, PeutEquiper);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutPeutEquiper en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutPeutEquiperTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<PeutEquiper>>();
            var _controller = new PeutEquipersController(mockRepository.Object);
            var PeutEquiper = new PeutEquiper { IdPack = 1, IdMoto = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1, 1)).ReturnsAsync(PeutEquiper);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<PeutEquiper>(), It.IsAny<PeutEquiper>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutPeutEquiper(1, 1, PeutEquiper);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutPeutEquiper_ReturnsNotFound_WhenPeutEquiperDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<PeutEquiper>>();
            var _controller = new PeutEquipersController(mockRepository.Object);
            var PeutEquiper = new PeutEquiper { IdPack = 1000, IdMoto = 1000 };
            mockRepository.Setup(x => x.GetByIdAsync(1000, 1000)).ReturnsAsync((PeutEquiper)null);

            // Act
            var result = await _controller.PutPeutEquiper(1000, 1000, PeutEquiper);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostPeutEquiperTestAsync

        /// <summary>
        /// Teste la méthode PostPeutEquiper pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostPeutEquiperTestAsync()
        {
            // Arrange : préparation des données attendues
            PeutEquiper option = new PeutEquiper
            {
                IdPack = 2,
                IdMoto = 2
            };

            // Act : appel de la méthode à tester
            var result = controller.PostPeutEquiper(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            PeutEquiper? optionRecupere = context.PeutEquipers
                .Where(u => u.IdPack == option.IdPack && u.IdMoto == option.IdMoto)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdMoto = optionRecupere.IdMoto;
            option.IdPack = optionRecupere.IdPack;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.PeutEquipers.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostPeutEquiper en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostPeutEquiperTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<PeutEquiper>>();
            var userController = new PeutEquipersController(mockRepository.Object);



            // Arrange : préparation des données attendues
            PeutEquiper option = new PeutEquiper
            {
                IdPack = 1,
                IdMoto = 1
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostPeutEquiper(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<PeutEquiper>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(PeutEquiper), "Pas un Utilisateur");

            option.IdMoto = ((PeutEquiper)result.Value).IdMoto;
            option.IdPack = ((PeutEquiper)result.Value).IdPack;
            Assert.AreEqual(option, (PeutEquiper)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeletePeutEquiperTest
        /// <summary>
        /// Teste la méthode DeletePeutEquiper pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeletePeutEquiperTest()
        {
            // Arrange : préparation des données attendues
            PeutEquiper option = new PeutEquiper
            {
                IdPack = 2,
                IdMoto = 2
            };
            context.PeutEquipers.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            PeutEquiper option1 = context.PeutEquipers.FirstOrDefault(u => u.IdPack == option.IdPack && u.IdMoto == option.IdMoto);
            _ = controller.DeletePeutEquiper(option.IdPack, option.IdMoto).Result;

            // Arrange : préparation des données attendues
            PeutEquiper res = context.PeutEquipers.FirstOrDefault(u => u.IdPack == option.IdPack && u.IdMoto == option.IdMoto);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeletePeutEquiper en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeletePeutEquiperTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            PeutEquiper option = new PeutEquiper
            {
                IdPack = 2,
                IdMoto = 2
            };
            var mockRepository = new Mock<IDataRepository<PeutEquiper>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdPack, option.IdMoto).Result).Returns(option);
            var userController = new PeutEquipersController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeletePeutEquiper(option.IdPack, option.IdMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}