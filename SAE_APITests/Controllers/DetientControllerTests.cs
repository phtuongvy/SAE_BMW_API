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
    public class DetientControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private DetientController controller;
        private BMWDBContext context;
        private IDataRepository<Detient> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur Detient.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new DetientManager(context);
            controller = new DetientController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur DetientController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void DetientControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new DetientManager(context);

            // Act : appel de la méthode à tester
            var option = new DetientController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetDetientTest

        /// <summary>
        /// Teste la méthode GetDetients pour vérifier qu'elle retourne la liste correcte des éléments Detient.
        /// </summary>
        [TestMethod()]
        public void GetDetientTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<Detient> expected = context.Detients.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetDetients().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetDetientByIdTest

        /// <summary>
        /// Teste la méthode GetDetientById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetDetientByIdTest()
        {
            // Arrange : préparation des données attendues
            Detient expected = context.Detients.Find(1, 2);
            // Act : appel de la méthode à tester
            var res = controller.GetDetientById(expected.IdEquipement, expected.IdConcessionnaire).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetDetientById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetDetientByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Detient>>();

            Detient option = new Detient
            {
                IdEquipement = 1,
                IdConcessionnaire = 1,

            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdEquipement , option.IdConcessionnaire).Result).Returns(option);
            var userController = new DetientController(mockRepository.Object);
            var actionResult = userController.GetDetientById(option.IdEquipement, option.IdConcessionnaire).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as Detient);
        }
        #endregion

        #region Test PutDetientTestAsync
        /// <summary>
        /// Teste la méthode PutDetient pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutDetients_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var aPourTailles = new Detient { IdEquipement = 39 , IdConcessionnaire = 2 };

            // Act
            var result = await controller.PutDetient(3, 3, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutDetient en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutDetients_ReturnsBadRequestResult_WhenDetientDoesNotExistAsync()
        {

            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Detient>>();
            var _controller = new DetientController(mockRepository.Object);

            var aPourTailles = new Detient { IdEquipement = 39, IdConcessionnaire = 2 };
            mockRepository.Setup(x => x.GetByIdAsync(39,2)).ReturnsAsync((Detient)null);

            // Act
            var result = await _controller.PutDetient(39, 2, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutDetients_ReturnsNotFoundResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Detient>>();
            var _controller = new DetientController(mockRepository.Object);

            var aPourTailles = new Detient { IdEquipement = 39, IdConcessionnaire = 2 };
            mockRepository.Setup(x => x.GetByIdAsync(39 , 2)).ReturnsAsync(aPourTailles);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Detient>(), It.IsAny<Detient>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutDetient(39, 2, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
        #endregion

        #region Test PostDetientTestAsync

        /// <summary>
        /// Teste la méthode PostDetient pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostDetientTestAsync()
        {
            // Arrange : préparation des données attendues
            Detient option = new Detient
            {
                IdEquipement = 40,
                IdConcessionnaire = 2,

            };

            // Act : appel de la méthode à tester
            var result = controller.PostDetient(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Detient? optionRecupere = context.Detients
                .Where(u => u.IdEquipement == option.IdEquipement && u.IdConcessionnaire == option.IdConcessionnaire)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdEquipement = optionRecupere.IdEquipement;
            option.IdConcessionnaire = optionRecupere.IdConcessionnaire;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.Detients.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostDetient en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostDetientTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Detient>>();
            var userController = new DetientController(mockRepository.Object);



            // Arrange : préparation des données attendues
            Detient option = new Detient
            {
                IdEquipement = 1,
                IdConcessionnaire = 1,

            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostDetient(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Detient>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Detient), "Pas un Utilisateur");

            option.IdEquipement = ((Detient)result.Value).IdEquipement;
            option.IdConcessionnaire = ((Detient)result.Value).IdConcessionnaire;
            Assert.AreEqual(option, (Detient)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteDetientTest
        /// <summary>
        /// Teste la méthode DeleteDetient pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteDetientTest()
        {
            // Arrange : préparation des données attendues
            Detient option = new Detient
            {
                IdEquipement = 40,
                IdConcessionnaire = 2,

            };
            context.Detients.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            Detient option1 = context.Detients.FirstOrDefault(u => u.IdEquipement == option.IdEquipement && u.IdConcessionnaire == option.IdConcessionnaire);
            _ = controller.DeleteDetient(option.IdEquipement, option.IdConcessionnaire).Result;

            // Arrange : préparation des données attendues
            Detient res = context.Detients.FirstOrDefault(u => u.IdEquipement == option.IdEquipement && u.IdConcessionnaire == option.IdConcessionnaire);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteDetient en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteDetientTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            Detient option = new Detient
            {
                IdEquipement = 1,
                IdConcessionnaire = 1,

            };
            var mockRepository = new Mock<IDataRepository<Detient>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdEquipement , option.IdConcessionnaire).Result).Returns(option);
            var userController = new DetientController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteDetient(option.IdEquipement, option.IdConcessionnaire ).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}