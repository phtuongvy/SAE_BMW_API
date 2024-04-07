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
    /// Classe de test pour AdressesController. Contient des tests unitaires pour tester le comportement du contrôleur Adresse.
    /// </summary>
    /// 
    [TestClass()]
    public class AdressesControllerTests
    {
       
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private AdressesController controller;
        private BMWDBContext context;
        private IDataRepository<Adresse> dataRepository;
        #endregion

        #region Test Initialization

        /// <summary>
        /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
        /// Initialise le contexte de base de données, le référentiel de données et le contrôleur Adresse.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            // Création du gestionnaire de données et du contrôleur à tester
            dataRepository = new AdresseManager(context);
            controller = new AdressesController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur AdressesController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void AdressesControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new AdresseManager(context);

            // Act : appel de la méthode à tester
            var option = new AdressesController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetAdresseTest

        /// <summary>
        /// Teste la méthode GetAdresses pour vérifier qu'elle retourne la liste correcte des éléments Adresse.
        /// </summary>
        [TestMethod()]
        public void GetAdresseTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<Adresse> expected = context.Adresses.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetAdresses().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetAdresseByIdTest

        /// <summary>
        /// Teste la méthode GetAdresseById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetAdresseByIdTest()
        {
            // Arrange : préparation des données attendues
            Adresse expected = context.Adresses.Find(1);
            // Act : appel de la méthode à tester
            var res = controller.GetAdresseById(expected.IdAdresse).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetAdresseById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetAdresseByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Adresse>>();

            Adresse option = new Adresse
            {
                IdAdresse = 100,
                CodePostal = "test",
                Ville = "test",
                Pays = "test",
                TypeAdresse = "test",
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IdAdresse).Result).Returns(option);
            var userController = new AdressesController(mockRepository.Object);
            var actionResult = userController.GetAdresseById(option.IdAdresse).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as Adresse);
        }
        #endregion

        #region Test PutAdresseTestAsync
        /// <summary>
        /// Teste la méthode PutAdresse pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutAdresseTestAsync_ReturnsBadRequest()
        {
            //Arrange
            Adresse addresse = new Adresse
            {
                IdAdresse = 100,
                CodePostal = "test",
                Ville = "test",
                Pays = "test",
                TypeAdresse = "test",
            };

            // Act : appel de la méthode à tester
            var result = await controller.PutAdresse(1, addresse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutAdresse en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutAdresseTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Adresse>>();
            var _controller = new AdressesController(mockRepository.Object);
            var addresse = new Adresse { IdAdresse = 100, };
            mockRepository.Setup(x => x.GetByIdAsync(1, 2)).ReturnsAsync(addresse);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Adresse>(), It.IsAny<Adresse>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutAdresse(100,  addresse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutAdresse_ReturnsNotFound_WhenAdresseDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Adresse>>();
            var _controller = new AdressesController(mockRepository.Object);
            var addresse = new Adresse { IdAdresse = 100, };
            mockRepository.Setup(x => x.GetByIdAsync(1000, 1000)).ReturnsAsync((Adresse)null);

            // Act
            var result = await _controller.PutAdresse(100, addresse);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
        #endregion

        #region Test PostAdresseTestAsync

        /// <summary>
        /// Teste la méthode PostAdresse pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public async Task PostAdresseTestAsync()
        {
            // Arrange : préparation des données attendues
            Adresse option = new Adresse
            {
                IdAdresse = 100,
                CodePostal = "test",
                Ville = "test",
                Pays = "test",
                TypeAdresse = "test",
            }; 

            // Act : appel de la méthode à tester
            var result = controller.PostAdresse(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Adresse? optionRecupere = context.Adresses
                .Where(u => u.IdAdresse == option.IdAdresse)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdAdresse = optionRecupere.IdAdresse;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.Adresses.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostAdresse en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostAdresseTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<Adresse>>();
            var userController = new AdressesController(mockRepository.Object);



            // Arrange : préparation des données attendues
            Adresse option = new Adresse
            {
                IdAdresse = 100,
                CodePostal = "test",
                Ville = "test",
                Pays = "test",
                TypeAdresse = "test",
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostAdresse(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Adresse>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Adresse), "Pas un Utilisateur");

            option.IdAdresse = ((Adresse)result.Value).IdAdresse;
           
            Assert.AreEqual(option, (Adresse)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteAdresseTest
        /// <summary>
        /// Teste la méthode DeleteAdresse pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteAdresseTest()
        {
            // Arrange : préparation des données attendues
            Adresse option = new Adresse
            {
                IdAdresse = 100,
                CodePostal = "test",
                Ville = "test", 
                Pays =  "test",
                TypeAdresse = "test",
            };

            context.Adresses.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            Adresse option1 = context.Adresses.FirstOrDefault(u => u.IdAdresse == option.IdAdresse );
            _ = controller.DeleteAdresse(option.IdAdresse).Result;

            // Arrange : préparation des données attendues
            Adresse res = context.Adresses.FirstOrDefault(u => u.IdAdresse == option.IdAdresse );
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteAdresse en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteAdresseTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            Adresse option = new Adresse
            {
                IdAdresse = 100,
                CodePostal = "test"
            };
            var mockRepository = new Mock<IDataRepository<Adresse>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdAdresse).Result).Returns(option);
            var userController = new AdressesController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteAdresse(option.IdAdresse).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }    
}