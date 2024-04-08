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
    public class PeutUtilisersControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private PeutUtilisersController controller;
        private BMWDBContext context;
        private IDataRepository<PeutUtiliser> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur PeutUtiliser.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new PeutUtiliserManager(context);
            controller = new PeutUtilisersController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur PeutUtiliserController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void PeutUtiliserControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new PeutUtiliserManager(context);

            // Act : appel de la méthode à tester
            var option = new PeutUtilisersController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetPeutUtiliserTest

        /// <summary>
        /// Teste la méthode GetPeutUtilisers pour vérifier qu'elle retourne la liste correcte des éléments PeutUtiliser.
        /// </summary>
        [TestMethod()]
        public void GetPeutUtiliserTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<PeutUtiliser> expected = context.PeutUtilisers.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetPeutUtilisers().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetPeutUtiliserByIdTest

        /// <summary>
        /// Teste la méthode GetPeutUtiliserById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetPeutUtiliserByIdTest()
        {
            // Arrange : préparation des données attendues
            PeutUtiliser expected = context.PeutUtilisers.Find(1, 1);
            // Act : appel de la méthode à tester
            var res = controller.GetPeutUtiliserById(expected.IdEquipementMoto, expected.IdPack).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetPeutUtiliserById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetPeutUtiliserByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<PeutUtiliser>>();

            PeutUtiliser option = new PeutUtiliser
            {
                IdEquipementMoto = 4,
                IdPack = 2,
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdEquipementMoto, option.IdPack).Result).Returns(option);
            var userController = new PeutUtilisersController(mockRepository.Object);
            var actionResult = userController.GetPeutUtiliserById(option.IdEquipementMoto, option.IdPack).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as PeutUtiliser);
        }
        #endregion

        #region Test PutPeutUtiliserTestAsync
        /// <summary>
        /// Teste la méthode PutPeutUtiliser pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutPeutUtiliserTestAsync_ReturnsBadRequest()
        {
            //Arrange
            PeutUtiliser PeutUtiliser = new PeutUtiliser
            {
                IdEquipementMoto = 4,
                IdPack = 2,
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutPeutUtiliser(3, 4, PeutUtiliser);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutPeutUtiliser en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutPeutUtiliserTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<PeutUtiliser>>();
            var _controller = new PeutUtilisersController(mockRepository.Object);
            var PeutUtiliser = new PeutUtiliser { IdEquipementMoto = 1, IdPack = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1, 1)).ReturnsAsync(PeutUtiliser);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<PeutUtiliser>(), It.IsAny<PeutUtiliser>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutPeutUtiliser(1, 1, PeutUtiliser);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutPeutUtiliser_ReturnsNotFound_WhenPeutUtiliserDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<PeutUtiliser>>();
            var _controller = new PeutUtilisersController(mockRepository.Object);
            var PeutUtiliser = new PeutUtiliser { IdEquipementMoto = 1000, IdPack = 1000 };
            mockRepository.Setup(x => x.GetByIdAsync(1000, 1000)).ReturnsAsync((PeutUtiliser)null);

            // Act
            var result = await _controller.PutPeutUtiliser(1000, 1000, PeutUtiliser);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        #region Test PostPeutUtiliserTestAsync

        /// <summary>
        /// Teste la méthode PostPeutUtiliser pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostPeutUtiliserTestAsync()
        {
            // Arrange : préparation des données attendues
            PeutUtiliser option = new PeutUtiliser
            {
                IdEquipementMoto = 6,
                IdPack = 1,
            };

            // Act : appel de la méthode à tester
            var result = controller.PostPeutUtiliser(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            PeutUtiliser? optionRecupere = context.PeutUtilisers
                .Where(u => u.IdEquipementMoto == option.IdEquipementMoto && u.IdPack == option.IdPack)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdEquipementMoto = optionRecupere.IdEquipementMoto;
            option.IdPack = optionRecupere.IdPack;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.PeutUtilisers.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostPeutUtiliser en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostPeutUtiliserTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<PeutUtiliser>>();
            var userController = new PeutUtilisersController(mockRepository.Object);



            // Arrange : préparation des données attendues
            PeutUtiliser option = new PeutUtiliser
            {
                IdEquipementMoto = 4,
                IdPack = 2,
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostPeutUtiliser(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<PeutUtiliser>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(PeutUtiliser), "Pas un Utilisateur");

            option.IdEquipementMoto = ((PeutUtiliser)result.Value).IdEquipementMoto;
            option.IdPack = ((PeutUtiliser)result.Value).IdPack;
            Assert.AreEqual(option, (PeutUtiliser)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeletePeutUtiliserTest
        /// <summary>
        /// Teste la méthode DeletePeutUtiliser pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeletePeutUtiliserTest()
        {
            // Arrange : préparation des données attendues
            PeutUtiliser option = new PeutUtiliser
            {
                IdEquipementMoto = 6,
                IdPack = 1,
            };
            context.PeutUtilisers.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            PeutUtiliser option1 = context.PeutUtilisers.FirstOrDefault(u => u.IdPack == option.IdPack && u.IdEquipementMoto == option.IdEquipementMoto);
            _ = controller.DeletePeutUtiliser(option.IdEquipementMoto, option.IdPack).Result;

            // Arrange : préparation des données attendues
            PeutUtiliser res = context.PeutUtilisers.FirstOrDefault(u => u.IdEquipementMoto == option.IdEquipementMoto && u.IdPack == option.IdPack);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeletePeutUtiliser en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeletePeutUtiliserTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            PeutUtiliser option = new PeutUtiliser
            {
                IdEquipementMoto = 4,
                IdPack = 2,
            };
            var mockRepository = new Mock<IDataRepository<PeutUtiliser>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdEquipementMoto, option.IdPack).Result).Returns(option);
            var userController = new PeutUtilisersController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeletePeutUtiliser(option.IdEquipementMoto, option.IdPack).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }
}