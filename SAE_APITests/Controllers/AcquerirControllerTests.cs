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
    public class AcquerirControllerTests
    {
        private AcquerirController controller;
        private BMWDBContext context;
        private IDataRepository<Acquerir> dataRepository;


        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new AcquerirManager(context);
            controller = new AcquerirController(dataRepository);
        }

        /// <summary>   
        /// Test Contrôleur 
        /// </summary>
        [TestMethod()]
        public void AcquerirControllerTest()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new AcquerirManager(context);

            // Act
            var option = new AcquerirController(dataRepository);

            // Assert
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }


        /// <summary>
        /// Test GetCarteBancaireTest 
        /// </summary>
        [TestMethod()]
        public void GetAcquerirTest()
        {
            // Arrange
            List<Acquerir> expected = context.Acquerirs.ToList();
            // Act
            var res = controller.GetAcquerirs().Result;
            // Assert
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }

        /// <summary>
        /// Test GetCarteBancaireById 
        /// </summary>

        [TestMethod()]
        public void GetAcquerirByIdTest()
        {
            // Arrange
            Acquerir expected = context.Acquerirs.Find(1, 1);
            // Act
            var res = controller.GetAcquerirById(expected.IdCompteClient, expected.IdCb).Result;
            // Assert
            Assert.AreEqual(expected, res.Value);
        }

        [TestMethod]
        public void GetAcquerirByIdTest_AvecMoq()
        {
            // Arrange

            var mockRepository = new Mock<IDataRepository<Acquerir>>();




            Acquerir option = new Acquerir
            {
                IdCompteClient = 1,
                IdCb = 1,
            };
            // Act
            mockRepository.Setup(x => x.GetByIdAsync(option.IdCompteClient, option.IdCb).Result).Returns(option);
            var userController = new AcquerirController(mockRepository.Object);

            var actionResult = userController.GetAcquerirById(option.IdCompteClient, option.IdCb).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as Acquerir);
        }

        #region Test PutAChoisiTestAsync
        /// <summary>
        /// Teste la méthode PutAChoisi pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutAChoisiTestAsync_ReturnsBadRequest()
        {
            // Arrange
            var acquerir = new Acquerir { IdCompteClient = 1, IdCb = 2 };

            // Act
            var result = await controller.PutAcquerir(3, 4, acquerir);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutAChoisi en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutAChoisiTestAvecMoqAsync()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Acquerir>>();
            var _controller = new AcquerirController(mockRepository.Object);
            var acquerir = new Acquerir { IdCompteClient = 1, IdCb = 2 };
            mockRepository.Setup(x => x.GetByIdAsync(1, 2)).ReturnsAsync((Acquerir)null);

            // Act
            var result = await _controller.PutAcquerir(1, 2, acquerir);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PutAChoisi_ReturnsNotFound_WhenAChoisiDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Acquerir>>();
            var _controller = new AcquerirController(mockRepository.Object);
            var acquerir = new Acquerir { IdCompteClient = 1, IdCb = 2 };
            mockRepository.Setup(x => x.GetByIdAsync(1, 2)).ReturnsAsync(new Acquerir());
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<Acquerir>(), It.IsAny<Acquerir>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutAcquerir(1, 2, acquerir);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }
        #endregion

        /// <summary>
        /// Test PostUtilisateur 
        /// </summary>
        /// 
        [TestMethod()]
        public async Task PostAcquerirTestAsync()
        {
            // Arrange

            Acquerir option = new Acquerir
            {
                IdCompteClient = 1,
                IdCb = 7,
            };

            // Act
            var result = controller.PostAcquerir(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert
            // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            Acquerir? optionRecupere = context.Acquerirs
                .Where(u => u.IdCompteClient == option.IdCompteClient && u.IdCb == option.IdCb)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdCompteClient = optionRecupere.IdCompteClient;
            option.IdCb = optionRecupere.IdCb;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.Acquerirs.Remove(option);
            await context.SaveChangesAsync();
        }

        [TestMethod]
        public void PostAcquerirTest_Mok()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Acquerir>>();
            var userController = new AcquerirController(mockRepository.Object);



            // Arrange
            Acquerir option = new Acquerir
            {
                IdCompteClient = 1,
                IdCb = 7,
            };

            // Act
            var actionResult = userController.PostAcquerir(option).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Acquerir>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Acquerir), "Pas un Utilisateur");

            option.IdCompteClient = ((Acquerir)result.Value).IdCompteClient;
            option.IdCb = ((Acquerir)result.Value).IdCb;
            Assert.AreEqual(option, (Acquerir)result.Value, "Utilisateurs pas identiques");
        }



        /// <summary>
        /// Test Delete 
        /// </summary>

        [TestMethod()]
        public void DeleteAcquerirTest()
        {
            // Arrange
            Acquerir option = new Acquerir
            {
                IdCompteClient = 1,
                IdCb = 7,
            };
            context.Acquerirs.Add(option);
            context.SaveChanges();

            // Act
            Acquerir option1 = context.Acquerirs.FirstOrDefault(u => u.IdCb == option.IdCb && u.IdCompteClient == option.IdCompteClient);
            _ = controller.DeleteAcquerir(option.IdCompteClient, option.IdCb).Result;

            // Arrange
            Acquerir res = context.Acquerirs.FirstOrDefault(u => u.IdCompteClient == option.IdCompteClient && u.IdCb == option.IdCb);
            Assert.IsNull(res, "utilisateur non supprimé");
        }


        [TestMethod]
        public void DeleteAcquerirTest_AvecMoq()
        {

            // Arrange
            Acquerir option = new Acquerir
            {
                IdCompteClient = 1,
                IdCb = 7,
            };
            var mockRepository = new Mock<IDataRepository<Acquerir>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdCompteClient, option.IdCb).Result).Returns(option);
            var userController = new AcquerirController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteAcquerir(option.IdCompteClient, option.IdCb).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}