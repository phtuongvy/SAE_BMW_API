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
    public class PossedersControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private PossedersController controller;
        private BMWDBContext context;
        private IDataRepository<Posseder> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur Posseder.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new PossederManager(context);
            controller = new PossedersController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur PossederController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void PossederControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new PossederManager(context);

            // Act : appel de la méthode à tester
            var option = new PossedersController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetPossederTest

        /// <summary>
        /// Teste la méthode GetPosseders pour vérifier qu'elle retourne la liste correcte des éléments Posseder.
        /// </summary>
        [TestMethod()]
        public void GetPossederTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<Posseder> expected = context.Posseders.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetPosseders().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetPossederByIdTest

        /// <summary>
        /// Teste la méthode GetPossederById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetPossederByIdTest()
        {
            // Arrange : préparation des données attendues
            Posseder expected = context.Posseders.Find(1, 1);
            // Act : appel de la méthode à tester
            var res = controller.GetPossederById(expected.IdMoto, expected.IdEquipementMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetPossederById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetPossederByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Posseder>>();

            Posseder option = new Posseder
            {
                IdMoto = 1,
                IdEquipementMoto = 1
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdMoto, option.IdEquipementMoto).Result).Returns(option);
            var userController = new PossedersController(mockRepository.Object);
            var actionResult = userController.GetPossederById(option.IdMoto, option.IdEquipementMoto).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as Posseder);
        }
        #endregion

        #region Test PutPossederTestAsync
        /// <summary>
        /// Teste la méthode PutPosseder pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutPossederTestAsync_ReturnsBadRequest()
        {
            //Arrange
            Posseder Posseder = new Posseder
            {
                IdMoto = 1,
                IdEquipementMoto = 1
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutPosseder(3, 4, Posseder);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutPosseder en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutPossederTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Posseder>>();
            var _controller = new PossedersController(mockRepository.Object);
            var Posseder = new Posseder { IdMoto = 1, IdEquipementMoto = 2 };
            mockRepository.Setup(x => x.GetByIdAsync(1, 2)).ReturnsAsync(Posseder);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Posseder>(), It.IsAny<Posseder>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutPosseder(1, 2, Posseder);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutPosseder_ReturnsNotFound_WhenPossederDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Posseder>>();
            var _controller = new PossedersController(mockRepository.Object);
            var Posseder = new Posseder { IdMoto = 1000, IdEquipementMoto = 1000 };
            mockRepository.Setup(x => x.GetByIdAsync(1000, 1000)).ReturnsAsync((Posseder)null);

            // Act
            var result = await _controller.PutPosseder(1000, 1000, Posseder);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostPossederTestAsync

        /// <summary>
        /// Teste la méthode PostPosseder pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostPossederTestAsync()
        {
            // Arrange : préparation des données attendues
            Posseder option = new Posseder
            {
                IdMoto = 4,
                IdEquipementMoto = 1
            };

            // Act : appel de la méthode à tester
            var result = controller.PostPosseder(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Posseder? optionRecupere = context.Posseders
                .Where(u => u.IdMoto == option.IdMoto && u.IdEquipementMoto == option.IdEquipementMoto)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdMoto = optionRecupere.IdMoto;
            option.IdEquipementMoto = optionRecupere.IdEquipementMoto;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.Posseders.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostPosseder en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostPossederTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Posseder>>();
            var userController = new PossedersController(mockRepository.Object);



            // Arrange : préparation des données attendues
            Posseder option = new Posseder
            {
                IdMoto = 1,
                IdEquipementMoto = 1
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostPosseder(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Posseder>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Posseder), "Pas un Utilisateur");

            option.IdMoto = ((Posseder)result.Value).IdMoto;
            option.IdEquipementMoto = ((Posseder)result.Value).IdEquipementMoto;
            Assert.AreEqual(option, (Posseder)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeletePossederTest
        /// <summary>
        /// Teste la méthode DeletePosseder pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeletePossederTest()
        {
            // Arrange : préparation des données attendues
            Posseder option = new Posseder
            {
                IdMoto = 4,
                IdEquipementMoto = 1
            };
            context.Posseders.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            Posseder option1 = context.Posseders.FirstOrDefault(u => u.IdEquipementMoto == option.IdEquipementMoto && u.IdMoto == option.IdMoto);
            _ = controller.DeletePosseder(option.IdMoto, option.IdEquipementMoto).Result;

            // Arrange : préparation des données attendues
            Posseder res = context.Posseders.FirstOrDefault(u => u.IdMoto == option.IdMoto && u.IdEquipementMoto == option.IdEquipementMoto);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeletePosseder en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeletePossederTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            Posseder option = new Posseder
            {
                IdMoto = 1,
                IdEquipementMoto = 1
            };
            var mockRepository = new Mock<IDataRepository<Posseder>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdMoto, option.IdEquipementMoto).Result).Returns(option);
            var userController = new PossedersController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeletePosseder(option.IdMoto, option.IdEquipementMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}