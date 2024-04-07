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
    public class CollectionsControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private CollectionsController controller;
        private BMWDBContext context;
        private IDataRepository<Collection> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur Collection.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new CollectionManager(context);
            controller = new CollectionsController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur CollectionsController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void CollectionsControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new CollectionManager(context);

            // Act : appel de la méthode à tester
            var option = new CollectionsController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetCollectionTest

        /// <summary>
        /// Teste la méthode GetCollections pour vérifier qu'elle retourne la liste correcte des éléments Collection.
        /// </summary>
        [TestMethod()]
        public void GetCollectionTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<Collection> expected = context.Collections.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetCollections().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetCollectionByIdTest

        /// <summary>
        /// Teste la méthode GetCollectionById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetCollectionByIdTest()
        {
            // Arrange : préparation des données attendues
            Collection expected = context.Collections.Find(1);
            // Act : appel de la méthode à tester
            var res = controller.GetCollectionById(expected.IdCollection).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetCollectionById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetCollectionByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Collection>>();

            Collection option = new Collection
            {
                IdCollection = 1,
                NomCollection = "test",
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdCollection).Result).Returns(option);
            var userController = new CollectionsController(mockRepository.Object);
            var actionResult = userController.GetCollectionById(option.IdCollection).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as Collection);
        }
        #endregion

        #region Test PutCollectionTestAsync
        /// <summary>
        /// Teste la méthode PutCollection pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutCollectionTestAsync()
        {
            //Arrange
            Collection optionAtester = new Collection
            {
                IdCollection = 40,
                NomCollection = "test",
            };

            Collection optionUptade = new Collection
            {
                IdCollection = 40,
                NomCollection = "test",
            };


            // Act : appel de la méthode à tester
            var res = await controller.PutCollection(optionAtester.IdCollection, optionUptade);

            // Arrange : préparation des données attendues
            var nouvelleoption = controller.GetCollectionById(optionUptade.IdCollection).Result;
            Assert.AreEqual(optionUptade, res);

            context.Collections.Remove(optionUptade);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PutCollection en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public void PutCollectionTestAvecMoq()
        {

            // Arrange : préparation des données attendues
            Collection optionToUpdate = new Collection
            {
                IdCollection = 100,
                NomCollection = "test",
            };
            Collection updatedOption = new Collection
            {
                IdCollection = 100,
                NomCollection = "test1",
            };

            var mockRepository = new Mock<IDataRepository<Collection>>();
            mockRepository.Setup(repo => repo.GetByIdAsync(21000)).ReturnsAsync(optionToUpdate);
            mockRepository.Setup(repo => repo.UpdateAsync(optionToUpdate, updatedOption)).Returns(Task.CompletedTask);


            var controller = new CollectionsController(mockRepository.Object);

            // Act : appel de la méthode à tester
            var result = controller.PutCollection(optionToUpdate.IdCollection, updatedOption).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(result, typeof(ActionResult<Collection>), "La réponse n'est pas du type attendu Collection");
        }
        #endregion

        #region Test PostCollectionTestAsync

        /// <summary>
        /// Teste la méthode PostCollection pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostCollectionTestAsync()
        {
            // Arrange : préparation des données attendues
            Collection option = new Collection
            {
                IdCollection = 100,
                NomCollection = "test",
            };

            // Act : appel de la méthode à tester
            var result = controller.PostCollection(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Collection? optionRecupere = context.Collections
                .Where(u => u.IdCollection == option.IdCollection )
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdCollection = optionRecupere.IdCollection;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.Collections.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostCollection en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostCollectionTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Collection>>();
            var userController = new CollectionsController(mockRepository.Object);



            // Arrange : préparation des données attendues
            Collection option = new Collection
            {
                IdCollection = 1,
                 NomCollection = "test",
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostCollection(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Collection>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Collection), "Pas un Utilisateur");

            option.IdCollection = ((Collection)result.Value).IdCollection;
            Assert.AreEqual(option, (Collection)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteCollectionTest
        /// <summary>
        /// Teste la méthode DeleteCollection pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteCollectionTest()
        {
            // Arrange : préparation des données attendues
            Collection option = new Collection
            {
                IdCollection = 100,
                 NomCollection = "test",
            };
            context.Collections.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            Collection option1 = context.Collections.FirstOrDefault(u => u.IdCollection == option.IdCollection);
            _ = controller.DeleteCollection(option.IdCollection).Result;

            // Arrange : préparation des données attendues
            Collection res = context.Collections.FirstOrDefault(u => u.IdCollection == option.IdCollection );
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteCollection en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteCollectionTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            Collection option = new Collection
            {
                IdCollection = 1,
                 NomCollection = "test",
            };
            var mockRepository = new Mock<IDataRepository<Collection>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdCollection).Result).Returns(option);
            var userController = new CollectionsController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteCollection(option.IdCollection).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}