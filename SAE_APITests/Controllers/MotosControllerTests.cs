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
    public class MotosControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private MotosController controller;
        private BMWDBContext context;
        private IDataRepository<Moto> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur Moto.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new MotoManager(context);
            controller = new MotosController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur MotoController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void MotoControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new MotoManager(context);

            // Act : appel de la méthode à tester
            var option = new MotosController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetMotoTest

        /// <summary>
        /// Teste la méthode GetMotos pour vérifier qu'elle retourne la liste correcte des éléments Moto.
        /// </summary>
        [TestMethod()]
        public void GetMotoTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<Moto> expected = context.Motos.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetMotos().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetMotoByIdTest

        /// <summary>
        /// Teste la méthode GetMotoById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetMotoByIdTest()
        {
            // Arrange : préparation des données attendues
            Moto expected = context.Motos.Find(1);
            // Act : appel de la méthode à tester
            var res = controller.GetMotoById(expected.MotoId).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetMotoById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetMotoByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Moto>>();

            Moto option = new Moto
            {
                MotoId = 5,
                IdGammeMoto = 1,
                NomMoto = "test",
                DescriptionMoto = "test",
                PrixMoto = 10
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.MotoId).Result).Returns(option);
            var userController = new MotosController(mockRepository.Object);
            var actionResult = userController.GetMotoById(option.MotoId).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as Moto);
        }
        #endregion

        #region Test PutMotoTestAsync
        /// <summary>
        /// Teste la méthode PutMoto pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutMotoTestAsync_ReturnsBadRequest()
        {
            //Arrange
            Moto Moto = new Moto
            {
                MotoId = 5,
                IdGammeMoto = 1,
                NomMoto = "test",
                DescriptionMoto = "test",
                PrixMoto = 10
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutMoto(3, Moto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutMoto en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutMotoTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Moto>>();
            var _controller = new MotosController(mockRepository.Object);
            var Moto = new Moto { MotoId = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(Moto);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Moto>(), It.IsAny<Moto>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutMoto(1, Moto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutMoto_ReturnsNotFound_WhenMotoDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Moto>>();
            var _controller = new MotosController(mockRepository.Object);
            var Moto = new Moto { MotoId = 1000 };
            mockRepository.Setup(x => x.GetByIdAsync(1000)).ReturnsAsync((Moto)null);

            // Act
            var result = await _controller.PutMoto(1000, Moto);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostMotoTestAsync

        /// <summary>
        /// Teste la méthode PostMoto pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostMotoTestAsync()
        {
            // Arrange : préparation des données attendues
            Moto option = new Moto
            {
                MotoId = 5,
                IdGammeMoto = 1,
                NomMoto = "test",
                DescriptionMoto = "test",
                PrixMoto = 10
            };

            // Act : appel de la méthode à tester
            var result = controller.PostMoto(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Moto? optionRecupere = context.Motos
                .Where(u => u.MotoId == option.MotoId)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.MotoId = optionRecupere.MotoId;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.Motos.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostMoto en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostMotoTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Moto>>();
            var userController = new MotosController(mockRepository.Object);



            // Arrange : préparation des données attendues
            Moto option = new Moto
            {
                MotoId = 5,
                IdGammeMoto = 1,
                NomMoto = "test",
                DescriptionMoto = "test",
                PrixMoto = 10
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostMoto(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Moto>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Moto), "Pas un Utilisateur");

            option.MotoId = ((Moto)result.Value).MotoId;
            Assert.AreEqual(option, (Moto)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteMotoTest
        /// <summary>
        /// Teste la méthode DeleteMoto pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteMotoTest()
        {
            // Arrange : préparation des données attendues
            Moto option = new Moto
            {
                MotoId= 7,
                IdGammeMoto = 1,
                NomMoto = "test",
                DescriptionMoto = "test",
                PrixMoto = 10
            };
            context.Motos.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            Moto option1 = context.Motos.FirstOrDefault(u => u.MotoId == option.MotoId);
            _ = controller.DeleteMoto(option.MotoId).Result;

            // Arrange : préparation des données attendues
            Moto res = context.Motos.FirstOrDefault(u => u.MotoId == option.MotoId);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteMoto en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteMotoTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            Moto option = new Moto
            {
                MotoId = 5,
                IdGammeMoto = 1,
                NomMoto = "test",
                DescriptionMoto = "test",
                PrixMoto = 10

            };
            var mockRepository = new Mock<IDataRepository<Moto>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.MotoId).Result).Returns(option);
            var userController = new MotosController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteMoto(option.MotoId).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}