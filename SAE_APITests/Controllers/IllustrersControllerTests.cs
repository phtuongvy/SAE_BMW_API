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
    public class IllustrersControllerTests
    {

        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private IllustrersController controller;
        private BMWDBContext context;
        private IDataRepository<Illustrer> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur Illustrer.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new IllustrerManager(context);
            controller = new IllustrersController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur IllustrersController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void IllustrersControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new IllustrerManager(context);

            // Act : appel de la méthode à tester
            var option = new IllustrersController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetIllustrerTest

        /// <summary>
        /// Teste la méthode GetIllustrers pour vérifier qu'elle retourne la liste correcte des éléments Illustrer.
        /// </summary>
        [TestMethod()]
        public void GetIllustrerTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<Illustrer> expected = context.Illustrers.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetIllustrers().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetIllustrerByIdTest

        /// <summary>
        /// Teste la méthode GetIllustrerById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetIllustrerByIdTest()
        {
            // Arrange : préparation des données attendues
            Illustrer expected = context.Illustrers.Find(1, 1);
            // Act : appel de la méthode à tester
            var res = controller.GetIllustrerById(expected.IdMoto, expected.IdPhoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetIllustrerById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetIllustrerByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Illustrer>>();

            Illustrer option = new Illustrer
            {
                IdMoto = 1,
                IdPhoto = 1,
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdMoto, option.IdPhoto).Result).Returns(option);
            var userController = new IllustrersController(mockRepository.Object);
            var actionResult = userController.GetIllustrerById(option.IdMoto, option.IdPhoto).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as Illustrer);
        }
        #endregion

        #region Test PutIllustrerTestAsync
        /// <summary>
        /// Teste la méthode PutIllustrer pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutIllustrerTestAsync_ReturnsBadRequest()
        {
            //Arrange
            Illustrer Illustrer = new Illustrer
            {
                IdMoto = 40,
                IdPhoto = 7,
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutIllustrer(3, 4, Illustrer);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutIllustrer en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutIllustrerTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Illustrer>>();
            var _controller = new IllustrersController(mockRepository.Object);
            var Illustrer = new Illustrer { IdMoto = 1, IdPhoto = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1, 2)).ReturnsAsync(Illustrer);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Illustrer>(), It.IsAny<Illustrer>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutIllustrer(1, 2, Illustrer);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutIllustrer_ReturnsNotFound_WhenIllustrerDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Illustrer>>();
            var _controller = new IllustrersController(mockRepository.Object);
            var Illustrer = new Illustrer { IdMoto = 1000, IdPhoto = 1000 };
            mockRepository.Setup(x => x.GetByIdAsync(1000, 1000)).ReturnsAsync((Illustrer)null);

            // Act
            var result = await _controller.PutIllustrer(1000, 1000, Illustrer);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostIllustrerTestAsync

        /// <summary>
        /// Teste la méthode PostIllustrer pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostIllustrerTestAsync()
        {
            // Arrange : préparation des données attendues
            Illustrer option = new Illustrer
            {
                IdMoto = 1,
                IdPhoto = 7,
            };

            // Act : appel de la méthode à tester
            var result = controller.PostIllustrer(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Illustrer? optionRecupere = context.Illustrers
                .Where(u => u.IdMoto == option.IdMoto && u.IdPhoto == option.IdPhoto)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdMoto = optionRecupere.IdMoto;
            option.IdPhoto = optionRecupere.IdPhoto;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.Illustrers.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostIllustrer en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostIllustrerTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Illustrer>>();
            var userController = new IllustrersController(mockRepository.Object);



            // Arrange : préparation des données attendues
            Illustrer option = new Illustrer
            {
                IdMoto = 1,
                IdPhoto = 7,
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostIllustrer(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Illustrer>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Illustrer), "Pas un Utilisateur");

            option.IdMoto = ((Illustrer)result.Value).IdMoto;
            option.IdPhoto = ((Illustrer)result.Value).IdPhoto;
            Assert.AreEqual(option, (Illustrer)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteIllustrerTest
        /// <summary>
        /// Teste la méthode DeleteIllustrer pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteIllustrerTest()
        {
            // Arrange : préparation des données attendues
            Illustrer option = new Illustrer
            {
                IdMoto = 1,
                IdPhoto = 7,
            };
            context.Illustrers.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            Illustrer option1 = context.Illustrers.FirstOrDefault(u => u.IdPhoto == option.IdPhoto && u.IdMoto == option.IdMoto);
            _ = controller.DeleteIllustrer(option.IdMoto, option.IdPhoto).Result;

            // Arrange : préparation des données attendues
            Illustrer res = context.Illustrers.FirstOrDefault(u => u.IdMoto == option.IdMoto && u.IdPhoto == option.IdPhoto);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteIllustrer en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteIllustrerTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            Illustrer option = new Illustrer
            {
                IdMoto = 1,
                IdPhoto = 7,
            };
            var mockRepository = new Mock<IDataRepository<Illustrer>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdMoto, option.IdPhoto).Result).Returns(option);
            var userController = new IllustrersController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteIllustrer(option.IdMoto, option.IdPhoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}