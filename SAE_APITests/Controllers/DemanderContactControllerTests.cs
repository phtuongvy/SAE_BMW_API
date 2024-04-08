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
    public class DemanderContactControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private DemanderContactController controller;
        private BMWDBContext context;
        private IDataRepository<DemanderContact> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur DemanderContact.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new DemanderContactManager(context);
            controller = new DemanderContactController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur DemanderContactController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void DemanderContactControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new DemanderContactManager(context);

            // Act : appel de la méthode à tester
            var option = new DemanderContactController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetDemanderContactTest

        /// <summary>
        /// Teste la méthode GetDemanderContacts pour vérifier qu'elle retourne la liste correcte des éléments DemanderContact.
        /// </summary>
        [TestMethod()]
        public void GetDemanderContactTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<DemanderContact> expected = context.DemanderContacts.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetDemanderContacts().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetDemanderContactByIdTest

        /// <summary>
        /// Teste la méthode GetDemanderContactById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetDemanderContactByIdTest()
        {
            // Arrange : préparation des données attendues
            DemanderContact expected = context.DemanderContacts.Find(11);
            // Act : appel de la méthode à tester
            var res = controller.GetDemanderContactById(expected.IdReservationOffre).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetDemanderContactById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetDemanderContactByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<DemanderContact>>();

            DemanderContact option = new DemanderContact
            {
                IdReservationOffre = 1,

            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdReservationOffre).Result).Returns(option);
            var userController = new DemanderContactController(mockRepository.Object);
            var actionResult = userController.GetDemanderContactById(option.IdReservationOffre).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as DemanderContact);
        }
        #endregion

        #region Test PutDemanderContactTestAsync
        /// <summary>
        /// Teste la méthode PutDemanderContact pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutDemanderContacts_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var aPourTailles = new DemanderContact { IdReservationOffre = 1 };

            // Act
            var result = await controller.PutDemanderContact(3, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutDemanderContact en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutDemanderContacts_ReturnsBadRequestResult_WhenDemanderContactDoesNotExistAsync()
        {

            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<DemanderContact>>();
            var _controller = new DemanderContactController(mockRepository.Object);

            var aPourTailles = new DemanderContact { IdReservationOffre = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((DemanderContact)null);

            // Act
            var result = await _controller.PutDemanderContact(1, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutDemanderContacts_ReturnsNotFoundResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<DemanderContact>>();
            var _controller = new DemanderContactController(mockRepository.Object);

            var aPourTailles = new DemanderContact { IdReservationOffre = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(aPourTailles);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<DemanderContact>(), It.IsAny<DemanderContact>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutDemanderContact(1, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostDemanderContactTestAsync

        /// <summary>
        /// Teste la méthode PostDemanderContact pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostDemanderContactTestAsync()
        {
            // Arrange : préparation des données attendues
            DemanderContact option = new DemanderContact
            {
                IdReservationOffre = 1,
                Objet = "test",
                ObjetDeLaDemande = "test",
                
                

            };

            // Act : appel de la méthode à tester
            var result = controller.PostDemanderContact(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            DemanderContact? optionRecupere = context.DemanderContacts
                .Where(u => u.IdReservationOffre == option.IdReservationOffre)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdReservationOffre = optionRecupere.IdReservationOffre;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.DemanderContacts.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostDemanderContact en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostDemanderContactTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<DemanderContact>>();
            var userController = new DemanderContactController(mockRepository.Object);



            // Arrange : préparation des données attendues
            DemanderContact option = new DemanderContact
            {
                IdReservationOffre = 1,

            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostDemanderContact(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<DemanderContact>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(DemanderContact), "Pas un Utilisateur");

            option.IdReservationOffre = ((DemanderContact)result.Value).IdReservationOffre;
            Assert.AreEqual(option, (DemanderContact)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteDemanderContactTest
        /// <summary>
        /// Teste la méthode DeleteDemanderContact pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteDemanderContactTest()
        {
            // Arrange : préparation des données attendues
            DemanderContact option = new DemanderContact
            {
                IdReservationOffre = 1,
                Objet = "test",
                ObjetDeLaDemande = "test"
            };
            context.DemanderContacts.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            DemanderContact option1 = context.DemanderContacts.FirstOrDefault(u => u.IdReservationOffre == option.IdReservationOffre);
            _ = controller.DeleteDemanderContact(option.IdReservationOffre).Result;

            // Arrange : préparation des données attendues
            DemanderContact res = context.DemanderContacts.FirstOrDefault(u => u.IdReservationOffre == option.IdReservationOffre);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteDemanderContact en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteDemanderContactTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            DemanderContact option = new DemanderContact
            {
                IdReservationOffre = 1,

            };
            var mockRepository = new Mock<IDataRepository<DemanderContact>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdReservationOffre).Result).Returns(option);
            var userController = new DemanderContactController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteDemanderContact(option.IdReservationOffre).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}