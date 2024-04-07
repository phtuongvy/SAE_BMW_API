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
using static System.Net.Mime.MediaTypeNames;

/// <summary>
/// Namespace contenant les tests pour le contrôleur AChoisiController.
/// </summary>

namespace SAE_API.Controllers.Tests
{
    /// <summary>
    /// Classe de test pour AChoisiController. Contient des tests unitaires pour tester le comportement du contrôleur AChoisi.
    /// </summary>
    /// 
    [TestClass()]
    public class AChoisiControllerTests
    {
        #region Private Fields
        // Déclaration des variables nécessaires pour les tests
        private AChoisiController controller;
        private BMWDBContext context;
        private IDataRepository<AChoisi> dataRepository;
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
            dataRepository = new AChoisiManager(context);
            controller = new AChoisiController(dataRepository);
        }
        #endregion

        #region Test Controller
        /// <summary>
        /// Teste l'instanciation du contrôleur AChoisiController pour s'assurer qu'elle n'est pas null.
        /// </summary>
        [TestMethod()]
        public void AChoisiControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new AChoisiManager(context);

            // Act : appel de la méthode à tester
            var option = new AChoisiController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }
        #endregion

        #region Test GetAChoisiTest

        /// <summary>
        /// Teste la méthode GetAChoisis pour vérifier qu'elle retourne la liste correcte des éléments AChoisi.
        /// </summary>
        [TestMethod()]
        public void GetAChoisiTest()
        {
            // Arrange : préparation des données attendues : préparation des données attendues
            List<AChoisi> expected = context.Achoisis.ToList();
            // Act : appel de la méthode à tester 
            var res = controller.GetAChoisis().Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues 
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }
        #endregion

        #region Test GetAChoisiByIdTest

        /// <summary>
        /// Teste la méthode GetAChoisiById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
        /// </summary>
        [TestMethod()]
        public void GetAChoisiByIdTest()
        {
            // Arrange : préparation des données attendues
            AChoisi expected = context.Achoisis.Find(1, 1);
            // Act : appel de la méthode à tester
            var res = controller.GetAChoisiById(expected.IDPack, expected.IDConfigurationMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.AreEqual(expected, res.Value);
        }

        /// <summary>
        /// Teste la méthode GetAChoisiById en utilisant un mock pour le référentiel de données.
        /// Permet de tester le contrôleur de manière isolée.
        /// </summary>
        [TestMethod]
        public void GetAChoisiByIdTest_AvecMoq()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<AChoisi>>();

            AChoisi option = new AChoisi
            {
                IDPack = 1,
                IDConfigurationMoto = 1,
            };

            // Act : appel de la méthode à tester
            mockRepository.Setup(x => x.GetByIdAsync(option.IDPack, option.IDConfigurationMoto).Result).Returns(option);
            var userController = new AChoisiController(mockRepository.Object);
            var actionResult = userController.GetAChoisiById(option.IDPack, option.IDConfigurationMoto).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as AChoisi);
        }
        #endregion

        #region Test PutAChoisiTestAsync
        /// <summary>
        /// Teste la méthode PutAChoisi pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutAChoisiTestAsync()
        {
            //Arrange
            AChoisi optionAtester = new AChoisi
            {
                IDPack = 40,
                IDConfigurationMoto = 7,
            };

            AChoisi optionUptade = new AChoisi
            {
                IDPack = 40,
                IDConfigurationMoto = 7,
            };


            // Act : appel de la méthode à tester
            var res = await controller.PutAChoisi(optionAtester.IDPack, optionAtester.IDConfigurationMoto, optionUptade);

            // Arrange : préparation des données attendues
            var nouvelleoption = controller.GetAChoisiById(optionUptade.IDPack, optionUptade.IDConfigurationMoto).Result;
            Assert.AreEqual(optionUptade, res);

            context.Achoisis.Remove(optionUptade);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PutAChoisi en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public void PutAChoisiTestAvecMoq()
        {

            // Arrange : préparation des données attendues
            AChoisi optionToUpdate = new AChoisi
            {
                IDPack = 40,
                IDConfigurationMoto = 5,
            };
            AChoisi updatedOption = new AChoisi
            {
                IDPack = 100,
                IDConfigurationMoto = 100,
            };

            var mockRepository = new Mock<IDataRepository<AChoisi>>();
            mockRepository.Setup(repo => repo.GetByIdAsync(21000)).ReturnsAsync(optionToUpdate);
            mockRepository.Setup(repo => repo.UpdateAsync(optionToUpdate, updatedOption)).Returns(Task.CompletedTask);


            var controller = new AChoisiController(mockRepository.Object);

            // Act : appel de la méthode à tester
            var result = controller.PutAChoisi(40, 5, updatedOption).Result;

            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(result, typeof(ActionResult<AChoisi>), "La réponse n'est pas du type attendu AChoisi");
        }
        #endregion

        #region Test PostAChoisiTestAsync

        /// <summary>
        /// Teste la méthode PostAChoisi pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
        /// </summary>
        
        [TestMethod()]
        public async Task PostAChoisiTestAsync()
        {
            // Arrange : préparation des données attendues
            AChoisi option = new AChoisi
            {
                IDPack = 1,
                IDConfigurationMoto = 7,
            };

            // Act : appel de la méthode à tester
            var result = controller.PostAChoisi(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            AChoisi? optionRecupere = context.Achoisis
                .Where(u => u.IDPack == option.IDPack && u.IDConfigurationMoto == option.IDConfigurationMoto)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IDPack = optionRecupere.IDPack;
            option.IDConfigurationMoto = optionRecupere.IDConfigurationMoto;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.Achoisis.Remove(option);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Teste la méthode PostAChoisi en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
        /// </summary>
        [TestMethod]
        public void PostAChoisiTest_Mok()
        {
            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<AChoisi>>();
            var userController = new AChoisiController(mockRepository.Object);



            // Arrange : préparation des données attendues
            AChoisi option = new AChoisi
            {
                IDPack = 1,
                IDConfigurationMoto = 7,
            };

            // Act : appel de la méthode à tester
            var actionResult = userController.PostAChoisi(option).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<AChoisi>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(AChoisi), "Pas un Utilisateur");

            option.IDPack = ((AChoisi)result.Value).IDPack;
            option.IDConfigurationMoto = ((AChoisi)result.Value).IDConfigurationMoto;
            Assert.AreEqual(option, (AChoisi)result.Value, "Utilisateurs pas identiques");
        }
        #endregion

        #region Test DeleteAChoisiTest
        /// <summary>
        /// Teste la méthode DeleteAChoisi pour vérifier que la suppression d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod()]
        public void DeleteAChoisiTest()
        {
            // Arrange : préparation des données attendues
            AChoisi option = new AChoisi
            {
                IDPack = 1,
                IDConfigurationMoto = 7,
            };
            context.Achoisis.Add(option);
            context.SaveChanges();

            // Act : appel de la méthode à tester
            AChoisi option1 = context.Achoisis.FirstOrDefault(u => u.IDConfigurationMoto == option.IDConfigurationMoto && u.IDPack == option.IDPack);
            _ = controller.DeleteAChoisi(option.IDPack, option.IDConfigurationMoto).Result;

            // Arrange : préparation des données attendues
            AChoisi res = context.Achoisis.FirstOrDefault(u => u.IDPack == option.IDPack && u.IDConfigurationMoto == option.IDConfigurationMoto);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        /// <summary>
        /// Teste la méthode DeleteAChoisi en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
        /// </summary>
        [TestMethod]
        public void DeleteAChoisiTest_AvecMoq()
        {

            // Arrange : préparation des données attendues
            AChoisi option = new AChoisi
            {
                IDPack = 1,
                IDConfigurationMoto = 7,
            };
            var mockRepository = new Mock<IDataRepository<AChoisi>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IDPack, option.IDConfigurationMoto).Result).Returns(option);
            var userController = new AChoisiController(mockRepository.Object);
            // Act : appel de la méthode à tester
            var actionResult = userController.DeleteAChoisi(option.IDPack, option.IDConfigurationMoto).Result;
            // Assert : vérification que les données obtenues correspondent aux données attendues
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
        #endregion
    }

}