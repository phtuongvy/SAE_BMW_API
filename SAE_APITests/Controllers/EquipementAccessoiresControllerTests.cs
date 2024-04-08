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
    public class EquipementAccessoiresControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private EquipementAccessoiresController controller;
        private BMWDBContext context;
        private IDataRepository<EquipementAccessoire> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur EquipementAccessoire.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new EquipementAccessoireManager(context);
            controller = new EquipementAccessoiresController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur EquipementAccessoiresController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void EquipementAccessoiresControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new EquipementAccessoireManager(context);

            // Act : appel de la méthode à tester
            var option = new EquipementAccessoiresController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetEquipementAccessoireTest

        /// <summary>
        /// Teste la méthode GetEquipementAccessoires pour vérifier qu'elle retourne la liste correcte des éléments EquipementAccessoire.
        /// </summary>
        [TestMethod()]
        public void GetEquipementAccessoireTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<EquipementAccessoire> expected = context.EquipementAccessoires.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetEquipementAccessoires().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetEquipementAccessoireByIdTest

        /// <summary>
        /// Teste la méthode GetEquipementAccessoireById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetEquipementAccessoireByIdTest()
        {
            // Arrange : préparation des données attendues
            EquipementAccessoire expected = context.EquipementAccessoires.Find(12);
            // Act : appel de la méthode à tester
            var res = controller.GetEquipementAccessoireById(expected.IdEquipementMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetEquipementAccessoireById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetEquipementAccessoireByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<EquipementAccessoire>>();

            EquipementAccessoire option = new EquipementAccessoire
            {
                IdEquipementMoto = 1,
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdEquipementMoto).Result).Returns(option);
            var userController = new EquipementAccessoiresController(mockRepository.Object);
            var actionResult = userController.GetEquipementAccessoireById(option.IdEquipementMoto).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as EquipementAccessoire);
        }
        #endregion

        #region Test PutEquipementAccessoireTestAsync
        /// <summary>
        /// Teste la méthode PutEquipementAccessoire pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutEquipementAccessoireTestAsync_ReturnsBadRequest()
        {
            //Arrange
            EquipementAccessoire EquipementAccessoire = new EquipementAccessoire
            {
                IdEquipementMoto = 1,  
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutEquipementAccessoire(8, EquipementAccessoire);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutEquipementAccessoire en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutEquipementAccessoireTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<EquipementAccessoire>>();
            var _controller = new EquipementAccessoiresController(mockRepository.Object);
            var EquipementAccessoire = new EquipementAccessoire { IdEquipementMoto = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(EquipementAccessoire);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<EquipementAccessoire>(), It.IsAny<EquipementAccessoire>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutEquipementAccessoire(1,  EquipementAccessoire);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutEquipementAccessoire_ReturnsNotFound_WhenEquipementAccessoireDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<EquipementAccessoire>>();
            var _controller = new EquipementAccessoiresController(mockRepository.Object);
            var EquipementAccessoire = new EquipementAccessoire { IdEquipementMoto= 1000 };
            mockRepository.Setup(x => x.GetByIdAsync(1000)).ReturnsAsync((EquipementAccessoire)null);

            // Act
            var result = await _controller.PutEquipementAccessoire(1000, EquipementAccessoire);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostEquipementAccessoireTestAsync

        /// <summary>
        /// Teste la méthode PostEquipementAccessoire pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostEquipementAccessoireTestAsync()
        {
            // Arrange : préparation des données attendues
            EquipementAccessoire option = new EquipementAccessoire
            {
                IdEquipementMoto = 1,
            };

            // Act : appel de la méthode à tester
            var result = controller.PostEquipementAccessoire(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            EquipementAccessoire? optionRecupere = context.EquipementAccessoires
                .Where(u => u.IdEquipementMoto == option.IdEquipementMoto )
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdEquipementMoto = optionRecupere.IdEquipementMoto;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.EquipementAccessoires.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostEquipementAccessoire en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostEquipementAccessoireTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<EquipementAccessoire>>();
            var userController = new EquipementAccessoiresController(mockRepository.Object);



            // Arrange : préparation des données attendues
            EquipementAccessoire option = new EquipementAccessoire
            {
                IdEquipementMoto = 30,
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostEquipementAccessoire(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<EquipementAccessoire>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(EquipementAccessoire), "Pas un Utilisateur");

            option.IdEquipementMoto = ((EquipementAccessoire)result.Value).IdEquipementMoto;
            Assert.AreEqual(option, (EquipementAccessoire)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteEquipementAccessoireTest
        /// <summary>
        /// Teste la méthode DeleteEquipementAccessoire pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteEquipementAccessoireTest()
        {
            // Arrange : préparation des données attendues
            EquipementAccessoire option = new EquipementAccessoire
            {
                IdEquipementMoto = 1
            };
            context.EquipementAccessoires.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            EquipementAccessoire option1 = context.EquipementAccessoires.FirstOrDefault(u => u.IdEquipementMoto == option.IdEquipementMoto );
            _ = controller.DeleteEquipementAccessoire(option.IdEquipementMoto).Result;

            // Arrange : préparation des données attendues
            EquipementAccessoire res = context.EquipementAccessoires.FirstOrDefault(u => u.IdEquipementMoto == option.IdEquipementMoto );
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteEquipementAccessoire en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteEquipementAccessoireTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            EquipementAccessoire option = new EquipementAccessoire
            {
                IdEquipementMoto =30
            };
            var mockRepository = new Mock<IDataRepository<EquipementAccessoire>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdEquipementMoto).Result).Returns(option);
            var userController = new EquipementAccessoiresController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteEquipementAccessoire(option.IdEquipementMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}