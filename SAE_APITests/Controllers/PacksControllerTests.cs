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
    public class PacksControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private PacksController controller;
        private BMWDBContext context;
        private IDataRepository<Pack> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur Pack.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new PackManager(context);
            controller = new PacksController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur PackController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void PackControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new PackManager(context);

            // Act : appel de la méthode à tester
            var option = new PacksController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetPackTest

        /// <summary>
        /// Teste la méthode GetPacks pour vérifier qu'elle retourne la liste correcte des éléments Pack.
        /// </summary>
        [TestMethod()]
        public void GetPackTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<Pack> expected = context.Packs.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetPacks().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetPackByIdTest

        /// <summary>
        /// Teste la méthode GetPackById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetPackByIdTest()
        {
            // Arrange : préparation des données attendues
            Pack expected = context.Packs.Find(1);
            // Act : appel de la méthode à tester
            var res = controller.GetPackById(expected.PackId).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetPackById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetPackByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Pack>>();

            Pack option = new Pack
            {
                PackId = 10,
                NomPack = "test",
                DescriptionPack = "test",
                PrixPack = 100
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.PackId).Result).Returns(option);
            var userController = new PacksController(mockRepository.Object);
            var actionResult = userController.GetPackById(option.PackId).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as Pack);
        }
        #endregion

        #region Test PutPackTestAsync
        /// <summary>
        /// Teste la méthode PutPack pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutPackTestAsync_ReturnsBadRequest()
        {
            //Arrange
            Pack Pack = new Pack
            {
                PackId = 10,
                NomPack = "test",
                DescriptionPack = "test",
                PrixPack = 100
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutPack(3, Pack);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutPack en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutPackTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Pack>>();
            var _controller = new PacksController(mockRepository.Object);
            var Pack = new Pack { PackId = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(Pack);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Pack>(), It.IsAny<Pack>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutPack(1, Pack);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutPack_ReturnsNotFound_WhenPackDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Pack>>();
            var _controller = new PacksController(mockRepository.Object);
            var Pack = new Pack { PackId = 1000 };
            mockRepository.Setup(x => x.GetByIdAsync(1000)).ReturnsAsync((Pack)null);

            // Act
            var result = await _controller.PutPack(1000, Pack);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostPackTestAsync

        /// <summary>
        /// Teste la méthode PostPack pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostPackTestAsync()
        {
            // Arrange : préparation des données attendues
            Pack option = new Pack
            {
                PackId = 10,
                NomPack = "test",
                DescriptionPack = "test",
                PrixPack = 100
            };

            // Act : appel de la méthode à tester
            var result = controller.PostPack(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Pack? optionRecupere = context.Packs
                .Where(u => u.PackId == option.PackId)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.PackId = optionRecupere.PackId;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.Packs.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostPack en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostPackTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Pack>>();
            var userController = new PacksController(mockRepository.Object);



            // Arrange : préparation des données attendues
            Pack option = new Pack
            {
                PackId = 10,
                NomPack = "test",
                DescriptionPack = "test",
                PrixPack = 100
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostPack(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Pack>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Pack), "Pas un Utilisateur");

            option.PackId = ((Pack)result.Value).PackId;
            Assert.AreEqual(option, (Pack)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeletePackTest
        /// <summary>
        /// Teste la méthode DeletePack pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeletePackTest()
        {
            // Arrange : préparation des données attendues
            Pack option = new Pack
            {
                PackId = 10,
                NomPack = "test",
                DescriptionPack = "test",
                PrixPack = 100
            };
            context.Packs.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            Pack option1 = context.Packs.FirstOrDefault(u => u.PackId == option.PackId);
            _ = controller.DeletePack(option.PackId).Result;

            // Arrange : préparation des données attendues
            Pack res = context.Packs.FirstOrDefault(u => u.PackId == option.PackId);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeletePack en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeletePackTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            Pack option = new Pack
            {
                PackId = 10,
                NomPack = "test",
                DescriptionPack = "test",
                PrixPack = 100

            };
            var mockRepository = new Mock<IDataRepository<Pack>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.PackId).Result).Returns(option);
            var userController = new PacksController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeletePack(option.PackId).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}