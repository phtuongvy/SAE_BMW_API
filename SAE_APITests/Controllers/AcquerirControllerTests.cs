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

        /// <summary>
        /// Test PutCarteBancaireTest 
        /// </summary>
        [TestMethod()]
        public async Task PutAcquerirTestAsync()
        {
            //Arrange
            Acquerir optionAtester = new Acquerir
            {
                IdCompteClient = 40,
                IdCb = 7,
            };

            Acquerir optionUptade = new Acquerir
            {
                IdCompteClient = 40,
                IdCb = 7,
            };


            // Act
            var res = await controller.PutAcquerir(optionAtester.IdCompteClient, optionAtester.IdCb, optionUptade);

            // Arrange
            var nouvelleoption = controller.GetAcquerirById(optionUptade.IdCompteClient, optionUptade.IdCb).Result;
            Assert.AreEqual(optionUptade, res);

            context.Acquerirs.Remove(optionUptade);
            await context.SaveChangesAsync();
        }

        [TestMethod]
        public void PutAcquerirTestAvecMoq()
        {

            // Arrange
            Acquerir optionToUpdate = new Acquerir
            {
                IdCompteClient = 40,
                IdCb = 5,
            };
            Acquerir updatedOption = new Acquerir
            {
                IdCompteClient = 100,
                IdCb = 100,
            };

            var mockRepository = new Mock<IDataRepository<Acquerir>>();
            mockRepository.Setup(repo => repo.GetByIdAsync(21000)).ReturnsAsync(optionToUpdate);
            mockRepository.Setup(repo => repo.UpdateAsync(optionToUpdate, updatedOption)).Returns(Task.CompletedTask);


            var controller = new AcquerirController(mockRepository.Object);

            // Act
            var result = controller.PutAcquerir(40, 5, updatedOption).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<Acquerir>), "La réponse n'est pas du type attendu Acquerir");
        }

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