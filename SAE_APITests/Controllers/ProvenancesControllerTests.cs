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
    public class ProvenancesControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private ProvenancesController controller;
        private BMWDBContext context;
        private IDataRepository<Provenance> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur AChoisi.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new ProvenanceManager(context);
            controller = new ProvenancesController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur AChoisiController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void ProvenancesControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new ProvenanceManager(context);

            // Act : appel de la méthode à tester
            var option = new ProvenancesController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetAChoisiTest

        /// <summary>
        /// Teste la méthode GetAChoisis pour vérifier qu'elle retourne la liste correcte des éléments AChoisi.
        /// </summary>
        [TestMethod()]
        public void GetProvenancesTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<Provenance> expected = context.Provenances.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetProvenances().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetAChoisiByIdTest

        /// <summary>
        /// Teste la méthode GetAChoisiById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetProvenanceByIdTest()
        {
            // Arrange : préparation des données attendues
            Provenance expected = context.Provenances.Find(1, 6);
            // Act : appel de la méthode à tester
            var res = controller.GetProvenanceById(expected.IdCommande, expected.IdConcessionnaire).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetAChoisiById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetProvenanceByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Provenance>>();

            Provenance provenance = new Provenance
            {
                IdCommande = 1,
                IdConcessionnaire = 1
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(provenance.IdCommande, provenance.IdConcessionnaire).Result).Returns(provenance);
            var userController = new ProvenancesController(mockRepository.Object);
            var actionResult = userController.GetProvenanceById(provenance.IdCommande, provenance.IdConcessionnaire).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(provenance, actionResult.Value as Provenance);
        }
        #endregion

        #region Test PutAChoisiTestAsync
        /// <summary>
        /// Teste la méthode PutAChoisi pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutProvenanceTestAsync_ReturnsBadRequest()
        {
            //Arrange
            Provenance provenance = new Provenance
            {
                IdCommande = 1,
                IdConcessionnaire = 1
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutProvenance(3, 4, provenance);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutAChoisi en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutProvenanceTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Provenance>>();
            var _controller = new ProvenancesController(mockRepository.Object);
            var provenance = new Provenance { IdCommande = 2, IdConcessionnaire = 7 };
            mockRepository.Setup(x => x.GetByIdAsync(2, 7)).ReturnsAsync(provenance);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Provenance>(), It.IsAny<Provenance>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutProvenance(2, 7, provenance);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutProvenance_ReturnsNotFound_WhenProvenanceDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Provenance>>();
            var _controller = new ProvenancesController(mockRepository.Object);
            var provenance = new Provenance { IdCommande = 1000, IdConcessionnaire = 1000 };
            mockRepository.Setup(x => x.GetByIdAsync(1000, 1000)).ReturnsAsync((Provenance)null);

            // Act
            var result = await _controller.PutProvenance(1000, 1000, provenance);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostAChoisiTestAsync

        /// <summary>
        /// Teste la méthode PostAChoisi pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostProvenanceTestAsync()
        {
            // Arrange : préparation des données attendues
            Provenance provenance = new Provenance
            {
                IdCommande = 1,
                IdConcessionnaire = 1
            };

            // Act : appel de la méthode à tester
            var result = controller.PostProvenance(provenance).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Provenance? optionRecupere = context.Provenances
                .Where(u => u.IdCommande == provenance.IdCommande && u.IdConcessionnaire == provenance.IdConcessionnaire)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            provenance.IdCommande = optionRecupere.IdCommande;
            provenance.IdConcessionnaire = optionRecupere.IdConcessionnaire;
            Assert.AreEqual(optionRecupere, provenance, "Utilisateurs pas identiques");

            context.Provenances.Remove(provenance);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostAChoisi en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostProvenanceTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Provenance>>();
            var userController = new ProvenancesController(mockRepository.Object);



            // Arrange : préparation des données attendues
            Provenance provenance = new Provenance
            {
                IdCommande = 1,
                IdConcessionnaire = 1
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostProvenance(provenance).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Provenance>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Provenance), "Pas un Utilisateur");

            provenance.IdCommande = ((Provenance)result.Value).IdCommande;
            provenance.IdConcessionnaire = ((Provenance)result.Value).IdConcessionnaire;
            Assert.AreEqual(provenance, (Provenance)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteAChoisiTest
        /// <summary>
        /// Teste la méthode DeleteAChoisi pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteProvenanceTest()
        {
            // Arrange : préparation des données attendues
            Provenance provenance = new Provenance
            {
                IdCommande = 1,
                IdConcessionnaire = 1
            };
            context.Provenances.Add(provenance);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            Provenance option1 = context.Provenances.FirstOrDefault(u => u.IdConcessionnaire == provenance.IdConcessionnaire && u.IdCommande == provenance.IdCommande);
            _ = controller.DeleteProvenance(provenance.IdCommande, provenance.IdConcessionnaire).Result;

            // Arrange : préparation des données attendues
            Provenance res = context.Provenances.FirstOrDefault(u => u.IdCommande == provenance.IdCommande && u.IdConcessionnaire == provenance.IdConcessionnaire);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteAChoisi en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteProvenanceTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            Provenance provenance = new Provenance
            {
                IdCommande=1,
                IdConcessionnaire=1
            };
            var mockRepository = new Mock<IDataRepository<Provenance>>();
            mockRepository.Setup(x => x.GetByIdAsync(provenance.IdCommande, provenance.IdConcessionnaire).Result).Returns(provenance);
            var userController = new ProvenancesController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteProvenance(provenance.IdCommande, provenance.IdConcessionnaire).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}