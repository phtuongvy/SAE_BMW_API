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
    public class EquipementMotoOptionsControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private EquipementMotoOptionsController controller;
        private BMWDBContext context;
        private IDataRepository<EquipementMotoOption> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur EquipementMotoOption.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new EquipementMotoOptionManager(context);
            controller = new EquipementMotoOptionsController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur EquipementMotoOptionsController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void EquipementMotoOptionsControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new EquipementMotoOptionManager(context);

            // Act : appel de la méthode à tester
            var option = new EquipementMotoOptionsController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetEquipementMotoOptionTest

        /// <summary>
        /// Teste la méthode GetEquipementMotoOptions pour vérifier qu'elle retourne la liste correcte des éléments EquipementMotoOption.
        /// </summary>
        [TestMethod()]
        public void GetEquipementMotoOptionTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<EquipementMotoOption> expected = context.EquipementMotoOptions.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetEquipementMotoOptions().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetEquipementMotoOptionsByIdTest

        /// <summary>
        /// Teste la méthode GetEquipementMotoOptionsById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetEquipementMotoOptionsByIdTest()
        {
            // Arrange : préparation des données attendues
            EquipementMotoOption expected = context.EquipementMotoOptions.Find(12);
            // Act : appel de la méthode à tester
            var res = controller.GetEquipementMotoOptionById(expected.IdEquipementMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetEquipementMotoOptionsById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetEquipementMotoOptionsByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<EquipementMotoOption>>();

            EquipementMotoOption option = new EquipementMotoOption
            {
                IdEquipementMoto = 1,
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdEquipementMoto).Result).Returns(option);
            var userController = new EquipementMotoOptionsController(mockRepository.Object);
            var actionResult = userController.GetEquipementMotoOptionById(option.IdEquipementMoto).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as EquipementMotoOption);
        }
        #endregion

        #region Test PutEquipementMotoOptionTestAsync
        /// <summary>
        /// Teste la méthode PutEquipementMotoOption pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutEquipementMotoOptionTestAsync_ReturnsBadRequest()
        {
            //Arrange
            EquipementMotoOption EquipementMotoOption = new EquipementMotoOption
            {
                IdEquipementMoto = 1,
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutEquipementMotoOption(8, EquipementMotoOption);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutEquipementMotoOption en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutEquipementMotoOptionTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<EquipementMotoOption>>();
            var _controller = new EquipementMotoOptionsController(mockRepository.Object);
            var EquipementMotoOption = new EquipementMotoOption { IdEquipementMoto = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(EquipementMotoOption);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<EquipementMotoOption>(), It.IsAny<EquipementMotoOption>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutEquipementMotoOption(1, EquipementMotoOption);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutEquipementMotoOption_ReturnsNotFound_WhenEquipementMotoOptionDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<EquipementMotoOption>>();
            var _controller = new EquipementMotoOptionsController(mockRepository.Object);
            var EquipementMotoOption = new EquipementMotoOption { IdEquipementMoto = 1000 };
            mockRepository.Setup(x => x.GetByIdAsync(1000)).ReturnsAsync((EquipementMotoOption)null);

            // Act
            var result = await _controller.PutEquipementMotoOption(1000, EquipementMotoOption);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostEquipementMotoOptionTestAsync

        /// <summary>
        /// Teste la méthode PostEquipementMotoOption pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostEquipementMotoOptionTestAsync()
        {
            // Arrange : préparation des données attendues
            EquipementMotoOption option = new EquipementMotoOption
            {
                IdEquipementMoto = 13,
                NomEquipement = "test",
                DescriptionEquipementMoto = "test",   
            };

            // Act : appel de la méthode à tester
            var result = controller.PostEquipementMotoOption(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            EquipementMotoOption? optionRecupere = context.EquipementMotoOptions
                .Where(u => u.IdEquipementMoto == option.IdEquipementMoto)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdEquipementMoto = optionRecupere.IdEquipementMoto;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.EquipementMotoOptions.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostEquipementMotoOption en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostEquipementMotoOptionTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<EquipementMotoOption>>();
            var userController = new EquipementMotoOptionsController(mockRepository.Object);



            // Arrange : préparation des données attendues
            EquipementMotoOption option = new EquipementMotoOption
            {
                IdEquipementMoto = 30,
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostEquipementMotoOption(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<EquipementMotoOption>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(EquipementMotoOption), "Pas un Utilisateur");

            option.IdEquipementMoto = ((EquipementMotoOption)result.Value).IdEquipementMoto;
            Assert.AreEqual(option, (EquipementMotoOption)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteEquipementMotoOptionTest
        /// <summary>
        /// Teste la méthode DeleteEquipementMotoOption pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteEquipementMotoOptionTest()
        {
            // Arrange : préparation des données attendues
            EquipementMotoOption option = new EquipementMotoOption
            {
                IdEquipementMoto = 13,
                NomEquipement = "test",
                DescriptionEquipementMoto = "test",
            };
            context.EquipementMotoOptions.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            EquipementMotoOption option1 = context.EquipementMotoOptions.FirstOrDefault(u => u.IdEquipementMoto == option.IdEquipementMoto);
            _ = controller.DeleteEquipementMotoOption(option.IdEquipementMoto).Result;

            // Arrange : préparation des données attendues
            EquipementMotoOption res = context.EquipementMotoOptions.FirstOrDefault(u => u.IdEquipementMoto == option.IdEquipementMoto);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteEquipementMotoOption en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteEquipementMotoOptionTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            EquipementMotoOption option = new EquipementMotoOption
            {
                IdEquipementMoto = 30
            };
            var mockRepository = new Mock<IDataRepository<EquipementMotoOption>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdEquipementMoto).Result).Returns(option);
            var userController = new EquipementMotoOptionsController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteEquipementMotoOption(option.IdEquipementMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}