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
    public class FavorisControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private FavorisController controller;
        private BMWDBContext context;
        private IDataRepository<Favoris> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur Favoris.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new FavorisManager(context);
            controller = new FavorisController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur FavorisController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void FavorisControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new FavorisManager(context);

            // Act : appel de la méthode à tester
            var option = new FavorisController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetFavorisTest

        /// <summary>
        /// Teste la méthode GetFavoriss pour vérifier qu'elle retourne la liste correcte des éléments Favoris.
        /// </summary>
        [TestMethod()]
        public void GetFavorisTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<Favoris> expected = context.Favoriss.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetFavoriss().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetFavorisByIdTest

        /// <summary>
        /// Teste la méthode GetFavorisById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetFavorisByIdTest()
        {
            // Arrange : préparation des données attendues
            Favoris expected = context.Favoriss.Find(1, 1);
            // Act : appel de la méthode à tester
            var res = controller.GetFavorisById(expected.IdCompteClient, expected.IdConcessionnaire).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetFavorisById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetFavorisByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Favoris>>();

            Favoris option = new Favoris
            {
                IdCompteClient = 1,
                IdConcessionnaire = 1,
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdCompteClient, option.IdConcessionnaire).Result).Returns(option);
            var userController = new FavorisController(mockRepository.Object);
            var actionResult = userController.GetFavorisById(option.IdCompteClient, option.IdConcessionnaire).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as Favoris);
        }
        #endregion

        #region Test PutFavorisTestAsync
        /// <summary>
        /// Teste la méthode PutFavoris pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutFavorisTestAsync_ReturnsBadRequest()
        {
            //Arrange
            Favoris Favoris = new Favoris
            {
                IdCompteClient = 40,
                IdConcessionnaire = 7,
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutFavoris(3, 4, Favoris);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutFavoris en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutFavorisTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Favoris>>();
            var _controller = new FavorisController(mockRepository.Object);
            var Favoris = new Favoris { IdCompteClient = 1, IdConcessionnaire = 2 };
            mockRepository.Setup(x => x.GetByIdAsync(1, 2)).ReturnsAsync(Favoris);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Favoris>(), It.IsAny<Favoris>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutFavoris(1, 2, Favoris);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutFavoris_ReturnsNotFound_WhenFavorisDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Favoris>>();
            var _controller = new FavorisController(mockRepository.Object);
            var Favoris = new Favoris { IdCompteClient = 1, IdConcessionnaire = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1, 1)).ReturnsAsync((Favoris)null);

            // Act
            var result = await _controller.PutFavoris(1, 1, Favoris);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        #endregion

        #region Test PostFavorisTestAsync

        /// <summary>
        /// Teste la méthode PostFavoris pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostFavorisTestAsync()
        {
            // Arrange : préparation des données attendues
            Favoris option = new Favoris
            {
                IdCompteClient = 1,
                IdConcessionnaire = 7,
            };

            // Act : appel de la méthode à tester
            var result = controller.PostFavoris(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Favoris? optionRecupere = context.Favoriss
                .Where(u => u.IdCompteClient == option.IdCompteClient && u.IdConcessionnaire == option.IdConcessionnaire)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdCompteClient = optionRecupere.IdCompteClient;
            option.IdConcessionnaire = optionRecupere.IdConcessionnaire;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.Favoriss.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostFavoris en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostFavorisTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Favoris>>();
            var userController = new FavorisController(mockRepository.Object);



            // Arrange : préparation des données attendues
            Favoris option = new Favoris
            {
                IdCompteClient = 1,
                IdConcessionnaire = 7,
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostFavoris(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Favoris>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Favoris), "Pas un Utilisateur");

            option.IdCompteClient = ((Favoris)result.Value).IdCompteClient;
            option.IdConcessionnaire = ((Favoris)result.Value).IdConcessionnaire;
            Assert.AreEqual(option, (Favoris)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteFavorisTest
        /// <summary>
        /// Teste la méthode DeleteFavoris pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteFavorisTest()
        {
            // Arrange : préparation des données attendues
            Favoris option = new Favoris
            {
                IdCompteClient = 1,
                IdConcessionnaire = 7,
            };
            context.Favoriss.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            Favoris option1 = context.Favoriss.FirstOrDefault(u => u.IdConcessionnaire == option.IdConcessionnaire && u.IdCompteClient == option.IdCompteClient);
            _ = controller.DeleteFavoris(option.IdCompteClient, option.IdConcessionnaire).Result;

            // Arrange : préparation des données attendues
            Favoris res = context.Favoriss.FirstOrDefault(u => u.IdCompteClient == option.IdCompteClient && u.IdConcessionnaire == option.IdConcessionnaire);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteFavoris en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteFavorisTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            Favoris option = new Favoris
            {
                IdCompteClient = 1,
                IdConcessionnaire = 7,
            };
            var mockRepository = new Mock<IDataRepository<Favoris>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdCompteClient, option.IdConcessionnaire).Result).Returns(option);
            var userController = new FavorisController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteFavoris(option.IdCompteClient, option.IdConcessionnaire).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}