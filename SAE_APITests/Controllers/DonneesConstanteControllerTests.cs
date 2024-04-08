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
    public class DonneesConstanteControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private DonneesConstanteController controller;
        private BMWDBContext context;
        private IDataRepository<DonneesConstante> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur DonneesConstante.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new DonneesConstanteManager(context);
            controller = new DonneesConstanteController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur DonneesConstanteController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void DonneesConstanteControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new DonneesConstanteManager(context);

            // Act : appel de la méthode à tester
            var option = new DonneesConstanteController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetDonneesConstanteTest

        /// <summary>
        /// Teste la méthode GetDonneesConstantes pour vérifier qu'elle retourne la liste correcte des éléments DonneesConstante.
        /// </summary>
        [TestMethod()]
        public void GetDonneesConstanteTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<DonneesConstante> expected = context.DonneesConstantes.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetDonneesConstantes().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetDonneesConstanteByIdTest

        /// <summary>
        /// Teste la méthode GetDonneesConstanteById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetDonneesConstanteByIdTest()
        {
            // Arrange : préparation des données attendues
            DonneesConstante expected = context.DonneesConstantes.Find(1);
            // Act : appel de la méthode à tester
            var res = controller.GetDonneesConstanteById(expected.IdDonnesConstante).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetDonneesConstanteById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetDonneesConstanteByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<DonneesConstante>>();

            DonneesConstante option = new DonneesConstante
            {
                IdDonnesConstante = 1,

            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdDonnesConstante).Result).Returns(option);
            var userController = new DonneesConstanteController(mockRepository.Object);
            var actionResult = userController.GetDonneesConstanteById(option.IdDonnesConstante).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as DonneesConstante);
        }
        #endregion

        #region Test PutDonneesConstanteTestAsync
        /// <summary>
        /// Teste la méthode PutDonneesConstante pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutDonneesConstantes_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var aPourTailles = new DonneesConstante { IdDonnesConstante = 1 };

            // Act
            var result = await controller.PutDonneesConstante(3, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutDonneesConstante en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutDonneesConstantes_ReturnsBadRequestResult_WhenDonneesConstanteDoesNotExistAsync()
        {

            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<DonneesConstante>>();
            var _controller = new DonneesConstanteController(mockRepository.Object);

            var aPourTailles = new DonneesConstante { IdDonnesConstante = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((DonneesConstante)null);

            // Act
            var result = await _controller.PutDonneesConstante(1, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutDonneesConstantes_ReturnsNotFoundResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<DonneesConstante>>();
            var _controller = new DonneesConstanteController(mockRepository.Object);

            var aPourTailles = new DonneesConstante { IdDonnesConstante = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(aPourTailles);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<DonneesConstante>(), It.IsAny<DonneesConstante>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutDonneesConstante(1, aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostDonneesConstanteTestAsync

        /// <summary>
        /// Teste la méthode PostDonneesConstante pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostDonneesConstanteTestAsync()
        {
            // Arrange : préparation des données attendues
            DonneesConstante option = new DonneesConstante
            {
                IdDonnesConstante = 100,

            };

            // Act : appel de la méthode à tester
            var result = controller.PostDonneesConstante(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            DonneesConstante? optionRecupere = context.DonneesConstantes
                .Where(u => u.IdDonnesConstante == option.IdDonnesConstante)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdDonnesConstante = optionRecupere.IdDonnesConstante;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.DonneesConstantes.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostDonneesConstante en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostDonneesConstanteTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<DonneesConstante>>();
            var userController = new DonneesConstanteController(mockRepository.Object);



            // Arrange : préparation des données attendues
            DonneesConstante option = new DonneesConstante
            {
                IdDonnesConstante = 1,

            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostDonneesConstante(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<DonneesConstante>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(DonneesConstante), "Pas un Utilisateur");

            option.IdDonnesConstante = ((DonneesConstante)result.Value).IdDonnesConstante;
            Assert.AreEqual(option, (DonneesConstante)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteDonneesConstanteTest
        /// <summary>
        /// Teste la méthode DeleteDonneesConstante pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteDonneesConstanteTest()
        {
            // Arrange : préparation des données attendues
            DonneesConstante option = new DonneesConstante
            {
                IdDonnesConstante = 100,

            };
            context.DonneesConstantes.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            DonneesConstante option1 = context.DonneesConstantes.FirstOrDefault(u => u.IdDonnesConstante == option.IdDonnesConstante);
            _ = controller.DeleteDonneesConstante(option.IdDonnesConstante).Result;

            // Arrange : préparation des données attendues
            DonneesConstante res = context.DonneesConstantes.FirstOrDefault(u => u.IdDonnesConstante == option.IdDonnesConstante);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteDonneesConstante en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteDonneesConstanteTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            DonneesConstante option = new DonneesConstante
            {
                IdDonnesConstante = 1,

            };
            var mockRepository = new Mock<IDataRepository<DonneesConstante>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdDonnesConstante).Result).Returns(option);
            var userController = new DonneesConstanteController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteDonneesConstante(option.IdDonnesConstante).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}