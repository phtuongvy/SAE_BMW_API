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
    public class ReservationsControllerTests
    {

        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private ReservationsController controller;
        private BMWDBContext context;
        private IDataRepository<Reservation> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur Reservation.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new ReservationManager(context);
            controller = new ReservationsController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur ReservationsController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void ReservationsControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new ReservationManager(context);

            // Act : appel de la méthode à tester
            var option = new ReservationsController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetReservationTest

        /// <summary>
        /// Teste la méthode GetReservations pour vérifier qu'elle retourne la liste correcte des éléments Reservation.
        /// </summary>
        [TestMethod()]
        public void GetReservationTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<Reservation> expected = context.Reservations.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetReservations().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetReservationByIdTest

        /// <summary>
        /// Teste la méthode GetReservationById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetReservationByIdTest()
        {
            // Arrange : préparation des données attendues
            Reservation expected = context.Reservations.Find(1);
            // Act : appel de la méthode à tester
            var res = controller.GetReservationById(expected.IdReservationOffre).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetReservationById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetReservationByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Reservation>>();

            Reservation option = new Reservation
            {
                IdReservationOffre = 100,
                IdConcessionnaire = 1,
                FinancementReservationOffre = "test",
                AssuranceReservation = "test"
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdReservationOffre).Result).Returns(option);
            var userController = new ReservationsController(mockRepository.Object);
            var actionResult = userController.GetReservationById(option.IdReservationOffre).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as Reservation);
        }
        #endregion

        #region Test PutReservationTestAsync
        /// <summary>
        /// Teste la méthode PutReservation pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutReservationTestAsync_ReturnsBadRequest()
        {
            //Arrange
            Reservation addresse = new Reservation
            {
                IdReservationOffre = 1,
                IdConcessionnaire = 1,
                FinancementReservationOffre = "test",
                AssuranceReservation = "test"
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutReservation(11, addresse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutReservation en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutReservationTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Reservation>>();
            var _controller = new ReservationsController(mockRepository.Object);
            var addresse = new Reservation { IdReservationOffre = 100, };
            mockRepository.Setup(x => x.GetByIdAsync(11)).ReturnsAsync(addresse);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Reservation>(), It.IsAny<Reservation>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutReservation(100, addresse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutReservation_ReturnsNotFound_WhenReservationDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Reservation>>();
            var _controller = new ReservationsController(mockRepository.Object);
            var addresse = new Reservation { IdReservationOffre = 100, };
            mockRepository.Setup(x => x.GetByIdAsync(1000, 1000)).ReturnsAsync((Reservation)null);

            // Act
            var result = await _controller.PutReservation(100, addresse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }
        #endregion

        #region Test PostReservationTestAsync

        /// <summary>
        /// Teste la méthode PostReservation pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostReservationTestAsync()
        {
            // Arrange : préparation des données attendues
            Reservation option = new Reservation
            {
                IdReservationOffre = 11,
                IdConcessionnaire = 1,
                FinancementReservationOffre = "test",
                AssuranceReservation = "test"
            };

            // Act : appel de la méthode à tester
            var result = controller.PostReservation(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Reservation? optionRecupere = context.Reservations
                .Where(u => u.IdReservationOffre == option.IdReservationOffre)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdReservationOffre = optionRecupere.IdReservationOffre;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.Reservations.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostReservation en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostReservationTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Reservation>>();
            var userController = new ReservationsController(mockRepository.Object);



            // Arrange : préparation des données attendues
            Reservation option = new Reservation
            {
                IdReservationOffre = 100,
                IdConcessionnaire = 1,
                FinancementReservationOffre = "test",
                AssuranceReservation = "test"
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostReservation(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Reservation>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Reservation), "Pas un Utilisateur");

            option.IdReservationOffre = ((Reservation)result.Value).IdReservationOffre;

            Assert.AreEqual(option, (Reservation)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteReservationTest
        /// <summary>
        /// Teste la méthode DeleteReservation pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteReservationTest()
        {
            // Arrange : préparation des données attendues
            Reservation option = new Reservation
            {
                IdReservationOffre = 11,
                IdConcessionnaire = 1,
                FinancementReservationOffre = "test",
                AssuranceReservation = "test"
            };

            context.Reservations.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            Reservation option1 = context.Reservations.FirstOrDefault(u => u.IdReservationOffre == option.IdReservationOffre);
            _ = controller.DeleteReservation(option.IdReservationOffre).Result;

            // Arrange : préparation des données attendues
            Reservation res = context.Reservations.FirstOrDefault(u => u.IdReservationOffre == option.IdReservationOffre);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteReservation en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteReservationTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            Reservation option = new Reservation
            {
                IdReservationOffre = 100,
                IdConcessionnaire = 1,
                FinancementReservationOffre = "test",
                AssuranceReservation = "test"
            };
            var mockRepository = new Mock<IDataRepository<Reservation>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdReservationOffre).Result).Returns(option);
            var userController = new ReservationsController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteReservation(option.IdReservationOffre).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}