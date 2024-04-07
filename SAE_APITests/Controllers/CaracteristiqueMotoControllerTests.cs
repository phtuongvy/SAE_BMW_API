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
    /// Classe de test pour CaracteristiqueMotoController. Contient des tests unitaires pour tester le comportement du contrôleur CaracteristiqueMoto.
    /// </summary>
    [TestClass()]
    public class CaracteristiqueMotoControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private CaracteristiqueMotoController controller;
        private BMWDBContext context;
        private IDataRepository<CaracteristiqueMoto> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur CaracteristiqueMoto.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new CaracteristiqueMotoManager(context);
            controller = new CaracteristiqueMotoController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur CaracteristiqueMotoController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void CaracteristiqueMotoControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new CaracteristiqueMotoManager(context);

            // Act : appel de la méthode à tester
            var option = new CaracteristiqueMotoController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetCaracteristiqueMotoTest

        /// <summary>
        /// Teste la méthode GetCaracteristiqueMotos pour vérifier qu'elle retourne la liste correcte des éléments CaracteristiqueMoto.
        /// </summary>
        [TestMethod()]
        public void GetCaracteristiqueMotoTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<CaracteristiqueMoto> expected = context.CaracteristiqueMotos.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetCaracteristiqueMotos().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetCaracteristiqueMotoByIdTest

        /// <summary>
        /// Teste la méthode GetCaracteristiqueMotoById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetCaracteristiqueMotoByIdTest()
        {
            // Arrange : préparation des données attendues
            CaracteristiqueMoto expected = context.CaracteristiqueMotos.Find(1);
            // Act : appel de la méthode à tester
            var res = controller.GetCaracteristiqueMotoById(expected.IdCaracteristiqueMoto, expected.IdCategorieCaracteristiqueMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetCaracteristiqueMotoById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetCaracteristiqueMotoByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<CaracteristiqueMoto>>();

            CaracteristiqueMoto option = new CaracteristiqueMoto
            {
                IdCaracteristiqueMoto = 1,
                IdCategorieCaracteristiqueMoto = 1,
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdCaracteristiqueMoto, option.IdCategorieCaracteristiqueMoto).Result).Returns(option);
            var userController = new CaracteristiqueMotoController(mockRepository.Object);
            var actionResult = userController.GetCaracteristiqueMotoById(option.IdCaracteristiqueMoto, option.IdCategorieCaracteristiqueMoto).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as CaracteristiqueMoto);
        }
        #endregion

        #region Test PutCaracteristiqueMotoTestAsync
        /// <summary>
        /// Teste la méthode PutCaracteristiqueMoto pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutCaracteristiqueMotoTestAsync()
        {
            //Arrange
            CaracteristiqueMoto optionAtester = new CaracteristiqueMoto
            {
                IdCaracteristiqueMoto = 69,
                IdCategorieCaracteristiqueMoto = 5,
            };

            CaracteristiqueMoto optionUptade = new CaracteristiqueMoto
            {
                IdCaracteristiqueMoto = 69,
                IdCategorieCaracteristiqueMoto = 4,
            };


            // Act : appel de la méthode à tester
            var res = await controller.PutCaracteristiqueMoto(optionAtester.IdCaracteristiqueMoto, optionAtester.IdCategorieCaracteristiqueMoto, optionUptade);

            // Arrange : préparation des données attendues
            var nouvelleoption = controller.GetCaracteristiqueMotoById(optionUptade.IdCaracteristiqueMoto, optionUptade.IdCategorieCaracteristiqueMoto).Result;
            Assert.AreEqual(optionUptade, res);

            context.CaracteristiqueMotos.Remove(optionUptade);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PutCaracteristiqueMoto en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public void PutCaracteristiqueMotoTestAvecMoq()
        {

            // Arrange : préparation des données attendues
            CaracteristiqueMoto optionToUpdate = new CaracteristiqueMoto
            {
                IdCaracteristiqueMoto = 40,
                IdCategorieCaracteristiqueMoto = 5,
            };
            CaracteristiqueMoto updatedOption = new CaracteristiqueMoto
            {
                IdCaracteristiqueMoto = 100,
                IdCategorieCaracteristiqueMoto = 100,
            };

            var mockRepository = new Mock<IDataRepository<CaracteristiqueMoto>>();
            mockRepository.Setup(repo => repo.GetByIdAsync(21000)).ReturnsAsync(optionToUpdate);
            mockRepository.Setup(repo => repo.UpdateAsync(optionToUpdate, updatedOption)).Returns(Task.CompletedTask);


            var controller = new CaracteristiqueMotoController(mockRepository.Object);

            // Act : appel de la méthode à tester
            var result = controller.PutCaracteristiqueMoto(40, 5, updatedOption).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(result, typeof(ActionResult<CaracteristiqueMoto>), "La réponse n'est pas du type attendu CaracteristiqueMoto");
        }
        #endregion

        #region Test PostCaracteristiqueMotoTestAsync

        /// <summary>
        /// Teste la méthode PostCaracteristiqueMoto pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostCaracteristiqueMotoTestAsync()
        {
            // Arrange : préparation des données attendues
            CaracteristiqueMoto option = new CaracteristiqueMoto
            {
                IdCaracteristiqueMoto = 69,
                IdCategorieCaracteristiqueMoto = 5,
                NomCaracteristiqueMoto = "test",
                ValeurCaracteristiqueMoto = "test"
            };

            // Act : appel de la méthode à tester
            var result = controller.PostCaracteristiqueMoto(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            CaracteristiqueMoto? optionRecupere = context.CaracteristiqueMotos
                .Where(u => u.IdCaracteristiqueMoto == option.IdCaracteristiqueMoto && u.IdCategorieCaracteristiqueMoto == option.IdCategorieCaracteristiqueMoto)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdCaracteristiqueMoto = optionRecupere.IdCaracteristiqueMoto;
            option.IdCategorieCaracteristiqueMoto = optionRecupere.IdCategorieCaracteristiqueMoto;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.CaracteristiqueMotos.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostCaracteristiqueMoto en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostCaracteristiqueMotoTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<CaracteristiqueMoto>>();
            var userController = new CaracteristiqueMotoController(mockRepository.Object);



            // Arrange : préparation des données attendues
            CaracteristiqueMoto option = new CaracteristiqueMoto
            {
                IdCaracteristiqueMoto = 69,
                IdCategorieCaracteristiqueMoto = 5,
                NomCaracteristiqueMoto = "test"
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostCaracteristiqueMoto(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<CaracteristiqueMoto>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(CaracteristiqueMoto), "Pas un Utilisateur");

            option.IdCaracteristiqueMoto = ((CaracteristiqueMoto)result.Value).IdCaracteristiqueMoto;
            option.IdCategorieCaracteristiqueMoto = ((CaracteristiqueMoto)result.Value).IdCategorieCaracteristiqueMoto;
            Assert.AreEqual(option, (CaracteristiqueMoto)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteCaracteristiqueMotoTest
        /// <summary>
        /// Teste la méthode DeleteCaracteristiqueMoto pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteCaracteristiqueMotoTest()
        {
            // Arrange : préparation des données attendues
            CaracteristiqueMoto option = new CaracteristiqueMoto
            {
                IdCaracteristiqueMoto = 69,
                IdCategorieCaracteristiqueMoto = 5,
                NomCaracteristiqueMoto = "test",
                ValeurCaracteristiqueMoto = "test"
            };
            context.CaracteristiqueMotos.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            CaracteristiqueMoto option1 = context.CaracteristiqueMotos.FirstOrDefault(u => u.IdCategorieCaracteristiqueMoto == option.IdCategorieCaracteristiqueMoto && u.IdCaracteristiqueMoto == option.IdCaracteristiqueMoto);
            _ = controller.DeleteCaracteristiqueMoto(option.IdCaracteristiqueMoto, option.IdCategorieCaracteristiqueMoto).Result;

            // Arrange : préparation des données attendues
            CaracteristiqueMoto res = context.CaracteristiqueMotos.FirstOrDefault(u => u.IdCaracteristiqueMoto == option.IdCaracteristiqueMoto && u.IdCategorieCaracteristiqueMoto == option.IdCategorieCaracteristiqueMoto);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteCaracteristiqueMoto en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteCaracteristiqueMotoTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            CaracteristiqueMoto option = new CaracteristiqueMoto
            {
                IdCaracteristiqueMoto = 1,
                IdCategorieCaracteristiqueMoto = 7,
            };
            var mockRepository = new Mock<IDataRepository<CaracteristiqueMoto>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdCaracteristiqueMoto, option.IdCategorieCaracteristiqueMoto).Result).Returns(option);
            var userController = new CaracteristiqueMotoController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteCaracteristiqueMoto(option.IdCaracteristiqueMoto, option.IdCategorieCaracteristiqueMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}