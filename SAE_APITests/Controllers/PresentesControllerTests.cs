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
    public class PresentesControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private PresentesController controller;
        private BMWDBContext context;
        private IDataRepository<Presente> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur Presente.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new PresenteManager(context);
            controller = new PresentesController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur PresenteController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void PresenteControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new PresenteManager(context);

            // Act : appel de la méthode à tester
            var option = new PresentesController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetPresenteTest

        /// <summary>
        /// Teste la méthode GetPresentes pour vérifier qu'elle retourne la liste correcte des éléments Presente.
        /// </summary>
        [TestMethod()]
        public void GetPresenteTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<Presente> expected = context.Presentes.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetPresentes().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetPresenteByIdTest

        /// <summary>
        /// Teste la méthode GetPresenteById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetPresenteByIdTest()
        {
            // Arrange : préparation des données attendues
            Presente expected = context.Presentes.Find(39,1,4);
            // Act : appel de la méthode à tester
            var res = controller.GetPresenteById(expected.IdPhoto, expected.IdEquipement, expected.IdCouleurEquipement).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetPresenteById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetPresenteByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Presente>>();

            Presente option = new Presente
            {
                IdEquipement = 1,
                IdPhoto = 1,
                IdCouleurEquipement = 1,
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdPhoto, option.IdEquipement, option.IdCouleurEquipement).Result).Returns(option);
            var userController = new PresentesController(mockRepository.Object);
            var actionResult = userController.GetPresenteById(option.IdPhoto, option.IdEquipement, option.IdCouleurEquipement).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as Presente);
        }
        #endregion

        #region Test PutPresenteTestAsync
        /// <summary>
        /// Teste la méthode PutPresente pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutPresentes_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var aPourTailles = new Presente { IdPhoto = 1, IdCouleurEquipement = 1 };

            // Act
            var result = await controller.PutPresente(3, 2, 1, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutPresente en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutPresentes_ReturnsBadRequestResult_WhenPresenteDoesNotExistAsync()
        {

            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Presente>>();
            var _controller = new PresentesController(mockRepository.Object);

            var aPourTailles = new Presente { IdPhoto = 1, IdCouleurEquipement = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Presente)null);

            // Act
            var result = await _controller.PutPresente(1, 1, 1, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutPresentes_ReturnsNotFoundResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Presente>>();
            var _controller = new PresentesController(mockRepository.Object);

            var aPourTailles = new Presente { IdPhoto = 1, IdCouleurEquipement = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(aPourTailles);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Presente>(), It.IsAny<Presente>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutPresente(1, 1, 1, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        #endregion

        #region Test PostPresenteTestAsync

        /// <summary>
        /// Teste la méthode PostPresente pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostPresenteTestAsync()
        {
            // Arrange : préparation des données attendues
            Presente option = new Presente
            {
                IdEquipement = 1,
                IdPhoto = 7,
                IdCouleurEquipement = 7,
            };

            // Act : appel de la méthode à tester
            var result = controller.PostPresente(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Presente? optionRecupere = context.Presentes
                .Where(u => u.IdEquipement == option.IdEquipement && u.IdPhoto == option.IdPhoto && option.IdCouleurEquipement == u.IdCouleurEquipement)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdEquipement = optionRecupere.IdEquipement;
            option.IdPhoto = optionRecupere.IdPhoto;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.Presentes.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostPresente en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostPresenteTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Presente>>();
            var userController = new PresentesController(mockRepository.Object);



            // Arrange : préparation des données attendues
            Presente option = new Presente
            {
                IdEquipement = 1,
                IdPhoto = 7,
                IdCouleurEquipement = 7,
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostPresente(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Presente>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Presente), "Pas un Utilisateur");

            option.IdEquipement = ((Presente)result.Value).IdEquipement;
            option.IdPhoto = ((Presente)result.Value).IdPhoto;
            Assert.AreEqual(option, (Presente)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeletePresenteTest
        /// <summary>
        /// Teste la méthode DeletePresente pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeletePresenteTest()
        {
            // Arrange : préparation des données attendues
            Presente option = new Presente
            {
                IdEquipement = 1,
                IdPhoto = 7,
                IdCouleurEquipement = 7,
            };
            context.Presentes.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            Presente option1 = context.Presentes.FirstOrDefault(u => u.IdPhoto == option.IdPhoto && u.IdEquipement == option.IdEquipement);
            _ = controller.DeletePresente(option.IdPhoto, (Int32)option.IdEquipement, (Int32)option.IdCouleurEquipement).Result;

            // Arrange : préparation des données attendues
            Presente res = context.Presentes.FirstOrDefault(u => u.IdEquipement == option.IdEquipement && u.IdPhoto == option.IdPhoto);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeletePresente en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeletePresenteTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            Presente option = new Presente
            {
                IdEquipement = 1,
                IdPhoto= 7,
                IdCouleurEquipement = 7,
            };
            var mockRepository = new Mock<IDataRepository<Presente>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdPhoto, (Int32)option.IdEquipement, (Int32)option.IdCouleurEquipement).Result).Returns(option);
            var userController = new PresentesController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeletePresente(option.IdPhoto, (Int32)option.IdEquipement, (Int32)option.IdCouleurEquipement).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}