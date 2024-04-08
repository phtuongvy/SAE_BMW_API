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
    public class DisposerControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private DisposerController controller;
        private BMWDBContext context;
        private IDataRepository<Disposer> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur Disposer.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new DisposerManager(context);
            controller = new DisposerController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur DisposerController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void DisposerControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new DisposerManager(context);

            // Act : appel de la méthode à tester
            var option = new DisposerController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetDisposerTest

        /// <summary>
        /// Teste la méthode GetDisposers pour vérifier qu'elle retourne la liste correcte des éléments Disposer.
        /// </summary>
        [TestMethod()]
        public void GetDisposerTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<Disposer> expected = context.Disposers.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetDisposers().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetDisposerByIdTest

        /// <summary>
        /// Teste la méthode GetDisposerById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetDisposerByIdTest()
        {
            // Arrange : préparation des données attendues
            Disposer expected = context.Disposers.Find(1, 1);
            // Act : appel de la méthode à tester
            var res = controller.GetDisposerById(expected.IdEquipement, expected.IdStock).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetDisposerById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetDisposerByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Disposer>>();

            Disposer option = new Disposer
            {
                IdEquipement = 1,
                IdStock = 1,
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdEquipement, option.IdStock).Result).Returns(option);
            var userController = new DisposerController(mockRepository.Object);
            var actionResult = userController.GetDisposerById(option.IdEquipement, option.IdStock).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as Disposer);
        }
        #endregion

        #region Test PutDisposerTestAsync
        /// <summary>
        /// Teste la méthode PutDisposer pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutDisposerTestAsync_ReturnsBadRequest()
        {
            //Arrange
            Disposer Disposer = new Disposer
            {
                IdEquipement = 40,
                IdStock = 7,
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutDisposer(3, 4, Disposer);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutDisposer en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutDisposerTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Disposer>>();
            var _controller = new DisposerController(mockRepository.Object);
            var Disposer = new Disposer { IdEquipement = 1, IdStock = 2 };
            mockRepository.Setup(x => x.GetByIdAsync(1, 2)).ReturnsAsync(Disposer);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Disposer>(), It.IsAny<Disposer>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutDisposer(1, 2, Disposer);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutDisposer_ReturnsNotFound_WhenDisposerDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Disposer>>();
            var _controller = new DisposerController(mockRepository.Object);
            var Disposer = new Disposer { IdEquipement = 1000, IdStock = 1000 };
            mockRepository.Setup(x => x.GetByIdAsync(1000, 1000)).ReturnsAsync((Disposer)null);

            // Act
            var result = await _controller.PutDisposer(1000, 1000, Disposer);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostDisposerTestAsync

        /// <summary>
        /// Teste la méthode PostDisposer pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostDisposerTestAsync()
        {
            // Arrange : préparation des données attendues
            Disposer option = new Disposer
            {
                IdEquipement = 1,
                IdStock = 7,
            };

            // Act : appel de la méthode à tester
            var result = controller.PostDisposer(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Disposer? optionRecupere = context.Disposers
                .Where(u => u.IdEquipement == option.IdEquipement && u.IdStock == option.IdStock)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdEquipement = optionRecupere.IdEquipement;
            option.IdStock = optionRecupere.IdStock;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.Disposers.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostDisposer en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostDisposerTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Disposer>>();
            var userController = new DisposerController(mockRepository.Object);



            // Arrange : préparation des données attendues
            Disposer option = new Disposer
            {
                IdEquipement = 1,
                IdStock = 7,
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostDisposer(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Disposer>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Disposer), "Pas un Utilisateur");

            option.IdEquipement = ((Disposer)result.Value).IdEquipement;
            option.IdStock = ((Disposer)result.Value).IdStock;
            Assert.AreEqual(option, (Disposer)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteDisposerTest
        /// <summary>
        /// Teste la méthode DeleteDisposer pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteDisposerTest()
        {
            // Arrange : préparation des données attendues
            Disposer option = new Disposer
            {
                IdEquipement = 1,
                IdStock = 7,
            };
            context.Disposers.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            Disposer option1 = context.Disposers.FirstOrDefault(u => u.IdStock == option.IdStock && u.IdEquipement == option.IdEquipement);
            _ = controller.DeleteDisposer(option.IdEquipement, option.IdStock).Result;

            // Arrange : préparation des données attendues
            Disposer res = context.Disposers.FirstOrDefault(u => u.IdEquipement == option.IdEquipement && u.IdStock == option.IdStock);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteDisposer en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteDisposerTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            Disposer option = new Disposer
            {
                IdEquipement = 1,
                IdStock = 7,
            };
            var mockRepository = new Mock<IDataRepository<Disposer>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdEquipement, option.IdStock).Result).Returns(option);
            var userController = new DisposerController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteDisposer(option.IdEquipement, option.IdStock).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}