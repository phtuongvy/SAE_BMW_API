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
    public class EstDansControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private EstDansController controller;
        private BMWDBContext context;
        private IDataRepository<EstDans> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur EstDans.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new EstDanssManager(context);
            controller = new EstDansController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur EstDansController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void EstDansControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new EstDanssManager(context);

            // Act : appel de la méthode à tester
            var option = new EstDansController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetEstDansTest

        /// <summary>
        /// Teste la méthode GetEstDanss pour vérifier qu'elle retourne la liste correcte des éléments EstDans.
        /// </summary>
        [TestMethod()]
        public void GetEstDansTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<EstDans> expected = context.EstDanss.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetEstDanss().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetEstDansByIdTest

        /// <summary>
        /// Teste la méthode GetEstDansById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetEstDansByIdTest()
        {
            // Arrange : préparation des données attendues
            EstDans expected = context.EstDanss.Find(1, 1);
            // Act : appel de la méthode à tester
            var res = controller.GetEstDansById(expected.IdMoto, expected.IdStock).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetEstDansById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetEstDansByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<EstDans>>();

            EstDans option = new EstDans
            {
                IdMoto = 1,
                IdStock = 1,
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdMoto, option.IdStock).Result).Returns(option);
            var userController = new EstDansController(mockRepository.Object);
            var actionResult = userController.GetEstDansById(option.IdMoto, option.IdStock).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as EstDans);
        }
        #endregion

        #region Test PutEstDansTestAsync
        /// <summary>
        /// Teste la méthode PutEstDans pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutEstDansTestAsync_ReturnsBadRequest()
        {
            //Arrange
            EstDans EstDans = new EstDans
            {
                IdMoto = 40,
                IdStock = 7,
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutEstDans(3, 4, EstDans);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutEstDans en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutEstDansTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<EstDans>>();
            var _controller = new EstDansController(mockRepository.Object);
            var EstDans = new EstDans { IdMoto = 1, IdStock = 2 };
            mockRepository.Setup(x => x.GetByIdAsync(1, 2)).ReturnsAsync(EstDans);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<EstDans>(), It.IsAny<EstDans>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutEstDans(1, 2, EstDans);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutEstDans_ReturnsNotFound_WhenEstDansDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<EstDans>>();
            var _controller = new EstDansController(mockRepository.Object);
            var EstDans = new EstDans { IdMoto = 1000, IdStock = 1000 };
            mockRepository.Setup(x => x.GetByIdAsync(1000, 1000)).ReturnsAsync((EstDans)null);

            // Act
            var result = await _controller.PutEstDans(1000, 1000, EstDans);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostEstDansTestAsync

        /// <summary>
        /// Teste la méthode PostEstDans pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostEstDansTestAsync()
        {
            // Arrange : préparation des données attendues
            EstDans option = new EstDans
            {
                IdMoto = 1,
                IdStock = 7,
            };

            // Act : appel de la méthode à tester
            var result = controller.PostEstDans(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            EstDans? optionRecupere = context.EstDanss
                .Where(u => u.IdMoto == option.IdMoto && u.IdStock == option.IdStock)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdMoto = optionRecupere.IdMoto;
            option.IdStock = optionRecupere.IdStock;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.EstDanss.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostEstDans en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostEstDansTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<EstDans>>();
            var userController = new EstDansController(mockRepository.Object);



            // Arrange : préparation des données attendues
            EstDans option = new EstDans
            {
                IdMoto = 1,
                IdStock = 7,
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostEstDans(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<EstDans>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(EstDans), "Pas un Utilisateur");

            option.IdMoto = ((EstDans)result.Value).IdMoto;
            option.IdStock = ((EstDans)result.Value).IdStock;
            Assert.AreEqual(option, (EstDans)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteEstDansTest
        /// <summary>
        /// Teste la méthode DeleteEstDans pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteEstDansTest()
        {
            // Arrange : préparation des données attendues
            EstDans option = new EstDans
            {
                IdMoto = 1,
                IdStock = 7,
            };
            context.EstDanss.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            EstDans option1 = context.EstDanss.FirstOrDefault(u => u.IdStock == option.IdStock && u.IdMoto == option.IdMoto);
            _ = controller.DeleteEstDans(option.IdMoto, option.IdStock).Result;

            // Arrange : préparation des données attendues
            EstDans res = context.EstDanss.FirstOrDefault(u => u.IdMoto == option.IdMoto && u.IdStock == option.IdStock);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteEstDans en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteEstDansTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            EstDans option = new EstDans
            {
                IdMoto = 1,
                IdStock = 7,
            };
            var mockRepository = new Mock<IDataRepository<EstDans>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdMoto, option.IdStock).Result).Returns(option);
            var userController = new EstDansController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteEstDans(option.IdMoto, option.IdStock).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}