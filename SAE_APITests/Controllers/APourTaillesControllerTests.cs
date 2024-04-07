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
    /// <summary>
    /// Classe de test pour APourTailleController. Contient des tests unitaires pour tester le comportement du contrôleur APourTaille.
    /// </summary>
    /// 
    [TestClass()]
    public class APourTaillesControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private APourTaillesController controller;
        private BMWDBContext context;
        private IDataRepository<APourTaille> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur APourTaille.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new APourTailleManager(context);
            controller = new APourTaillesController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur APourTailleController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void APourTailleControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new APourTailleManager(context);

            // Act : appel de la méthode à tester
            var option = new APourTaillesController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetAPourTailleTest

        /// <summary>
        /// Teste la méthode GetAPourTailles pour vérifier qu'elle retourne la liste correcte des éléments APourTaille.
        /// </summary>
        [TestMethod()]
        public void GetAPourTailleTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<APourTaille> expected = context.APourTailles.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetAPourTailles().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetAPourTailleByIdTest

        /// <summary>
        /// Teste la méthode GetAPourTailleById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetAPourTailleByIdTest()
        {
            // Arrange : préparation des données attendues
            APourTaille expected = context.APourTailles.Find(1, 1);
            // Act : appel de la méthode à tester
            var res = controller.GetAPourTaillesById(expected.IdEquipement, expected.IdTailleEquipement).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetAPourTailleById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetAPourTailleByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<APourTaille>>();

            APourTaille option = new APourTaille
            {
                IdEquipement = 1,
                IdTailleEquipement = 1,
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdEquipement, option.IdTailleEquipement).Result).Returns(option);
            var userController = new APourTaillesController(mockRepository.Object);
            var actionResult = userController.GetAPourTaillesById(option.IdEquipement, option.IdTailleEquipement).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as APourTaille);
        }
        #endregion

        #region Test PutAPourTailleTestAsync
        /// <summary>
        /// Teste la méthode PutAPourTaille pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutAPourTailleTestAsync()
        {
            //Arrange
            APourTaille optionAtester = new APourTaille
            {
                IdEquipement = 40,
                IdTailleEquipement = 7,
            };

            APourTaille optionUptade = new APourTaille
            {
                IdEquipement = 40,
                IdTailleEquipement = 7,
            };


            // Act : appel de la méthode à tester
            var res = await controller.PutAPourTailles(optionAtester.IdEquipement, optionAtester.IdTailleEquipement, optionUptade);

            // Arrange : préparation des données attendues
            var nouvelleoption = controller.GetAPourTaillesById(optionUptade.IdEquipement, optionUptade.IdTailleEquipement).Result;
            Assert.AreEqual(optionUptade, res);

            context.APourTailles.Remove(optionUptade);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PutAPourTaille en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public void PutAPourTailleTestAvecMoq()
        {

            // Arrange : préparation des données attendues
            APourTaille optionToUpdate = new APourTaille
            {
                IdEquipement = 40,
                IdTailleEquipement = 5,
            };
            APourTaille updatedOption = new APourTaille
            {
                IdEquipement = 100,
                IdTailleEquipement = 100,
            };

            var mockRepository = new Mock<IDataRepository<APourTaille>>();
            mockRepository.Setup(repo => repo.GetByIdAsync(21000)).ReturnsAsync(optionToUpdate);
            mockRepository.Setup(repo => repo.UpdateAsync(optionToUpdate, updatedOption)).Returns(Task.CompletedTask);


            var controller = new APourTaillesController(mockRepository.Object);

            // Act : appel de la méthode à tester
            var result = controller.PutAPourTailles(40, 5, updatedOption).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(result, typeof(ActionResult<APourTaille>), "La réponse n'est pas du type attendu APourTaille");
        }
        #endregion

        #region Test PostAPourTailleTestAsync

        /// <summary>
        /// Teste la méthode PostAPourTaille pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostAPourTailleTestAsync()
        {
            // Arrange : préparation des données attendues
            APourTaille option = new APourTaille
            {
                IdEquipement = 1,
                IdTailleEquipement = 7,
            };

            // Act : appel de la méthode à tester
            var result = controller.PostAPourTailles(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            APourTaille? optionRecupere = context.APourTailles
                .Where(u => u.IdEquipement == option.IdEquipement && u.IdTailleEquipement == option.IdTailleEquipement)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdEquipement = optionRecupere.IdEquipement;
            option.IdTailleEquipement = optionRecupere.IdTailleEquipement;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.APourTailles.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostAPourTaille en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostAPourTailleTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<APourTaille>>();
            var userController = new APourTaillesController(mockRepository.Object);



            // Arrange : préparation des données attendues
            APourTaille option = new APourTaille
            {
                IdEquipement = 1,
                IdTailleEquipement = 7,
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostAPourTailles(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<APourTaille>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(APourTaille), "Pas un Utilisateur");

            option.IdEquipement = ((APourTaille)result.Value).IdEquipement;
            option.IdTailleEquipement = ((APourTaille)result.Value).IdTailleEquipement;
            Assert.AreEqual(option, (APourTaille)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteAPourTailleTest
        /// <summary>
        /// Teste la méthode DeleteAPourTaille pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteAPourTailleTest()
        {
            // Arrange : préparation des données attendues
            APourTaille option = new APourTaille
            {
                IdEquipement = 1,
                IdTailleEquipement = 7,
            };
            context.APourTailles.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            APourTaille option1 = context.APourTailles.FirstOrDefault(u => u.IdTailleEquipement == option.IdTailleEquipement && u.IdEquipement == option.IdEquipement);
            _ = controller.DeleteAPourTailles(option.IdEquipement, option.IdTailleEquipement).Result;

            // Arrange : préparation des données attendues
            APourTaille res = context.APourTailles.FirstOrDefault(u => u.IdEquipement == option.IdEquipement && u.IdTailleEquipement == option.IdTailleEquipement);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteAPourTaille en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteAPourTailleTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            APourTaille option = new APourTaille
            {
                IdEquipement = 1,
                IdTailleEquipement = 7,
            };
            var mockRepository = new Mock<IDataRepository<APourTaille>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdEquipement, option.IdTailleEquipement).Result).Returns(option);
            var userController = new APourTaillesController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteAPourTailles(option.IdEquipement, option.IdTailleEquipement).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}