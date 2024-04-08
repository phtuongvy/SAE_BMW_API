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
    public class EstimersControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private EstimersController controller;
        private BMWDBContext context;
        private IDataRepository<Estimer> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur Estimer.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new EstimerManager(context);
            controller = new EstimersController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur EstimersController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void EstimersControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new EstimerManager(context);

            // Act : appel de la méthode à tester
            var option = new EstimersController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetEstimerTest

        /// <summary>
        /// Teste la méthode GetEstimers pour vérifier qu'elle retourne la liste correcte des éléments Estimer.
        /// </summary>
        [TestMethod()]
        public void GetEstimerTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<Estimer> expected = context.Estimers.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetEstimers().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetEstimerByIdTest

        /// <summary>
        /// Teste la méthode GetEstimerById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetEstimerByIdTest()
        {
            // Arrange : préparation des données attendues
            Estimer expected = context.Estimers.Find(1 , 1 , 1);
            // Act : appel de la méthode à tester
            var res = controller.GetEstimerById(expected.IdCompteClient, expected.IdMoyenDePaiement, expected.IdEstimationMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetEstimerById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetEstimerByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Estimer>>();

            Estimer option = new Estimer
            {
                IdMoyenDePaiement = 1,
                IdCompteClient = 1,
                IdEstimationMoto = 1,
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdCompteClient, option.IdMoyenDePaiement, option.IdEstimationMoto).Result).Returns(option);
            var userController = new EstimersController(mockRepository.Object);
            var actionResult = userController.GetEstimerById(option.IdCompteClient, option.IdMoyenDePaiement, option.IdEstimationMoto).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as Estimer);
        }
        #endregion

        #region Test PutEstimerTestAsync
        /// <summary>
        /// Teste la méthode PutEstimer pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutEstimers_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var aPourTailles = new Estimer { IdCompteClient = 1, IdEstimationMoto = 1 };

            // Act
            var result = await controller.PutEstimer(3, 2, 1, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutEstimer en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutEstimers_ReturnsBadRequestResult_WhenEstimerDoesNotExistAsync()
        {

            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Estimer>>();
            var _controller = new EstimersController(mockRepository.Object);

            var aPourTailles = new Estimer { IdCompteClient = 1, IdEstimationMoto = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((Estimer)null);

            // Act
            var result = await _controller.PutEstimer(1, 1, 1, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutEstimers_ReturnsNotFoundResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Estimer>>();
            var _controller = new EstimersController(mockRepository.Object);

            var aPourTailles = new Estimer { IdCompteClient = 1, IdEstimationMoto = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(aPourTailles);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Estimer>(), It.IsAny<Estimer>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutEstimer(1, 1, 1, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        #endregion

        #region Test PostEstimerTestAsync

        /// <summary>
        /// Teste la méthode PostEstimer pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostEstimerTestAsync()
        {
            // Arrange : préparation des données attendues
            Estimer option = new Estimer
            {
                IdMoyenDePaiement = 1,
                IdCompteClient = 20,
                IdEstimationMoto = 7,
            };

            // Act : appel de la méthode à tester
            var result = controller.PostEstimer(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Estimer? optionRecupere = context.Estimers
                .Where(u => u.IdMoyenDePaiement == option.IdMoyenDePaiement && u.IdCompteClient == option.IdCompteClient && option.IdEstimationMoto == u.IdEstimationMoto)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdMoyenDePaiement = optionRecupere.IdMoyenDePaiement;
            option.IdCompteClient = optionRecupere.IdCompteClient;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.Estimers.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostEstimer en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostEstimerTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Estimer>>();
            var userController = new EstimersController(mockRepository.Object);



            // Arrange : préparation des données attendues
            Estimer option = new Estimer
            {
                IdMoyenDePaiement = 1,
                IdCompteClient = 7,
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostEstimer(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Estimer>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Estimer), "Pas un Utilisateur");

            option.IdMoyenDePaiement = ((Estimer)result.Value).IdMoyenDePaiement;
            option.IdCompteClient = ((Estimer)result.Value).IdCompteClient;
            Assert.AreEqual(option, (Estimer)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteEstimerTest
        /// <summary>
        /// Teste la méthode DeleteEstimer pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteEstimerTest()
        {
            // Arrange : préparation des données attendues
            Estimer option = new Estimer
            {
                IdMoyenDePaiement = 1,
                IdCompteClient = 20,
                IdEstimationMoto = 7,
            };
            context.Estimers.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            Estimer option1 = context.Estimers.FirstOrDefault(u => u.IdCompteClient == option.IdCompteClient && u.IdMoyenDePaiement == option.IdMoyenDePaiement);
            _ = controller.DeleteEstimer(option.IdCompteClient, (Int32)option.IdMoyenDePaiement, (Int32)option.IdEstimationMoto).Result;

            // Arrange : préparation des données attendues
            Estimer res = context.Estimers.FirstOrDefault(u => u.IdMoyenDePaiement == option.IdMoyenDePaiement && u.IdCompteClient == option.IdCompteClient);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteEstimer en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteEstimerTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            Estimer option = new Estimer
            {
                IdMoyenDePaiement = 1,
                IdCompteClient = 7,
                IdEstimationMoto = 7,
            };
            var mockRepository = new Mock<IDataRepository<Estimer>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdCompteClient, (Int32)option.IdMoyenDePaiement, (Int32)option.IdEstimationMoto).Result).Returns(option);
            var userController = new EstimersController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteEstimer(option.IdCompteClient, (Int32)option.IdMoyenDePaiement, (Int32)option.IdEstimationMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}