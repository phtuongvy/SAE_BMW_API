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
    public class AChoisiControllerTests
    {
       
        private AChoisiController controller;
        private BMWDBContext context;
        private IDataRepository<AChoisi> dataRepository;


        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new AChoisiManager(context);
            controller = new AChoisiController(dataRepository);
        }

        /// <summary>   
        /// Test Contrôleur 
        /// </summary>
        [TestMethod()]
        public void AChoisiControllerTest()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new AChoisiManager(context);

            // Act
            var option = new AChoisiController(dataRepository);

            // Assert
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }


        /// <summary>
        /// Test GetCarteBancaireTest 
        /// </summary>
        [TestMethod()]
        public void GetAChoisiTest()
        {
            // Arrange
            List<AChoisi> expected = context.Achoisis.ToList();
            // Act
            var res = controller.GetAChoisis().Result;
            // Assert
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }

        /// <summary>
        /// Test GetCarteBancaireById 
        /// </summary>

        [TestMethod()]
        public void GetAChoisiByIdTest()
        {
            // Arrange
            AChoisi expected = context.Achoisis.Find(1, 1);
            // Act
            var res = controller.GetAChoisiById(expected.IDPack, expected.IDConfigurationMoto).Result;
            // Assert
            Assert.AreEqual(expected, res.Value);
        }

        [TestMethod]
        public void GetAChoisiByIdTest_AvecMoq()
        {
            // Arrange

            var mockRepository = new Mock<IDataRepository<AChoisi>>();




            AChoisi option = new AChoisi
            {
                IDPack = 1,
                IDConfigurationMoto = 1,
            };
            // Act
            mockRepository.Setup(x => x.GetByIdAsync(option.IDPack, option.IDConfigurationMoto).Result).Returns(option);
            var userController = new AChoisiController(mockRepository.Object);

            var actionResult = userController.GetAChoisiById(option.IDPack, option.IDConfigurationMoto).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as AChoisi);
        }

        /// <summary>
        /// Test PutCarteBancaireTest 
        /// </summary>
        [TestMethod()]
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


            // Act
            var res = await controller.PutAChoisi(optionAtester.IDPack, optionAtester.IDConfigurationMoto, optionUptade);

            // Arrange
            var nouvelleoption = controller.GetAChoisiById(optionUptade.IDPack, optionUptade.IDConfigurationMoto).Result;
            Assert.AreEqual(optionUptade, res);

            context.Achoisis.Remove(optionUptade);
            await context.SaveChangesAsync();
        }

        [TestMethod]
        public void PutAChoisiTestAvecMoq()
        {

            // Arrange
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

            // Act
            var result = controller.PutAChoisi(40, 5, updatedOption).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<AChoisi>), "La réponse n'est pas du type attendu AChoisi");
        }

        /// <summary>
        /// Test PostUtilisateur 
        /// </summary>
        /// 
        [TestMethod()]
        public async Task PostAChoisiTestAsync()
        {
            // Arrange

            AChoisi option = new AChoisi
            {
                IDPack = 1,
                IDConfigurationMoto = 7,
            };

            // Act
            var result = controller.PostAChoisi(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert
            // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
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

        [TestMethod]
        public void PostAChoisiTest_Mok()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<AChoisi>>();
            var userController = new AChoisiController(mockRepository.Object);



            // Arrange
            AChoisi option = new AChoisi
            {
                IDPack = 1,
                IDConfigurationMoto = 7,
            };

            // Act
            var actionResult = userController.PostAChoisi(option).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<AChoisi>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(AChoisi), "Pas un Utilisateur");

            option.IDPack = ((AChoisi)result.Value).IDPack;
            option.IDConfigurationMoto = ((AChoisi)result.Value).IDConfigurationMoto;
            Assert.AreEqual(option, (AChoisi)result.Value, "Utilisateurs pas identiques");
        }



        /// <summary>
        /// Test Delete 
        /// </summary>

        [TestMethod()]
        public void DeleteAChoisiTest()
        {
            // Arrange
            AChoisi option = new AChoisi
            {
                IDPack = 1,
                IDConfigurationMoto = 7,
            };
            context.Achoisis.Add(option);
            context.SaveChanges();

            // Act
            AChoisi option1 = context.Achoisis.FirstOrDefault(u => u.IDConfigurationMoto == option.IDConfigurationMoto && u.IDPack == option.IDPack);
            _ = controller.DeleteAChoisi(option.IDPack, option.IDConfigurationMoto).Result;

            // Arrange
            AChoisi res = context.Achoisis.FirstOrDefault(u => u.IDPack == option.IDPack && u.IDConfigurationMoto == option.IDConfigurationMoto);
            Assert.IsNull(res, "utilisateur non supprimé");
        }


        [TestMethod]
        public void DeleteAChoisiTest_AvecMoq()
        {

            // Arrange
            AChoisi option = new AChoisi
            {
                IDPack = 1,
                IDConfigurationMoto = 7,
            };
            var mockRepository = new Mock<IDataRepository<AChoisi>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IDPack, option.IDConfigurationMoto).Result).Returns(option);
            var userController = new AChoisiController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteAChoisi(option.IDPack, option.IDConfigurationMoto).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
    
}