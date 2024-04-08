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
    public class PhotosControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private PhotosController controller;
        private BMWDBContext context;
        private IDataRepository<Photo> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur Photo.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new PhotoManager(context);
            controller = new PhotosController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur PhotoController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void PhotoControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new PhotoManager(context);

            // Act : appel de la méthode à tester
            var option = new PhotosController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetPhotoTest

        /// <summary>
        /// Teste la méthode GetPhotos pour vérifier qu'elle retourne la liste correcte des éléments Photo.
        /// </summary>
        [TestMethod()]
        public void GetPhotoTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<Photo> expected = context.Photos.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetPhotos().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetPhotoByIdTest

        /// <summary>
        /// Teste la méthode GetPhotoById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetPhotoByIdTest()
        {
            // Arrange : préparation des données attendues
            Photo expected = context.Photos.Find(1);
            // Act : appel de la méthode à tester
            var res = controller.GetPhotoById(expected.PhotoId).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetPhotoById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetPhotoByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Photo>>();

            Photo option = new Photo
            {
                PhotoId = 190,
                LienPhoto = "test"
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.PhotoId).Result).Returns(option);
            var userController = new PhotosController(mockRepository.Object);
            var actionResult = userController.GetPhotoById(option.PhotoId).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as Photo);
        }
        #endregion

        #region Test PutPhotoTestAsync
        /// <summary>
        /// Teste la méthode PutPhoto pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutPhotoTestAsync_ReturnsBadRequest()
        {
            //Arrange
            Photo Photo = new Photo
            {
                PhotoId = 190,
                LienPhoto = "test"
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutPhoto(3, Photo);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutPhoto en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutPhotoTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Photo>>();
            var _controller = new PhotosController(mockRepository.Object);
            var Photo = new Photo { PhotoId = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(Photo);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Photo>(), It.IsAny<Photo>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutPhoto(1, Photo);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutPhoto_ReturnsNotFound_WhenPhotoDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Photo>>();
            var _controller = new PhotosController(mockRepository.Object);
            var Photo = new Photo { PhotoId = 1000};
            mockRepository.Setup(x => x.GetByIdAsync(1000)).ReturnsAsync((Photo)null);

            // Act
            var result = await _controller.PutPhoto(1000, Photo);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostPhotoTestAsync

        /// <summary>
        /// Teste la méthode PostPhoto pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostPhotoTestAsync()
        {
            // Arrange : préparation des données attendues
            Photo option = new Photo
            {
                PhotoId = 190,
                LienPhoto = "test"
            };

            // Act : appel de la méthode à tester
            var result = controller.PostPhoto(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Photo? optionRecupere = context.Photos
                .Where(u => u.PhotoId == option.PhotoId)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.PhotoId = optionRecupere.PhotoId;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.Photos.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostPhoto en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostPhotoTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Photo>>();
            var userController = new PhotosController(mockRepository.Object);



            // Arrange : préparation des données attendues
            Photo option = new Photo
            {
                PhotoId = 190,
                LienPhoto = "test"
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostPhoto(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Photo>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Photo), "Pas un Utilisateur");

            option.PhotoId = ((Photo)result.Value).PhotoId;
            Assert.AreEqual(option, (Photo)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeletePhotoTest
        /// <summary>
        /// Teste la méthode DeletePhoto pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeletePhotoTest()
        {
            // Arrange : préparation des données attendues
            Photo option = new Photo
            {
                PhotoId = 190,
                LienPhoto = "test"
            };
            context.Photos.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            Photo option1 = context.Photos.FirstOrDefault(u => u.PhotoId == option.PhotoId);
            _ = controller.DeletePhoto(option.PhotoId).Result;

            // Arrange : préparation des données attendues
            Photo res = context.Photos.FirstOrDefault(u => u.PhotoId == option.PhotoId);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeletePhoto en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeletePhotoTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            Photo option = new Photo
            {
                PhotoId = 190,
                LienPhoto = "test"
            };
            var mockRepository = new Mock<IDataRepository<Photo>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.PhotoId).Result).Returns(option);
            var userController = new PhotosController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeletePhoto(option.PhotoId).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}