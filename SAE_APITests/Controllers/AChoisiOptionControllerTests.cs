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
    public class AChoisiOptionControllerTests
    {
        private AChoisiOptionController controller;
        private BMWDBContext context;
        private IDataRepository<AChoisiOption> dataRepository;


        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new AChoisiOptionManager(context);
            controller = new AChoisiOptionController(dataRepository);
        }

        /// <summary>   
        /// Test Contrôleur 
        /// </summary>
        [TestMethod()]
        public void AChoisiOptionControllerTest()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new AChoisiOptionManager(context);

            // Act
            var option = new AChoisiOptionController(dataRepository);

            // Assert
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }


        /// <summary>
        /// Test GetCarteBancaireTest 
        /// </summary>
        [TestMethod()]
        public void GetAChoisiOptionTest()
        {
            // Arrange
            List<CarteBancaire> expected = context.CartesBancaires.ToList();
            // Act
            var res = controller.GetAChoisiOptions().Result;
            // Assert
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }

        /// <summary>
        /// Test GetCarteBancaireById 
        /// </summary>

        [TestMethod()]
        public void GetCarteBancaireByIdTest()
        {
            // Arrange
            AChoisiOption expected = context.AChoisiOptions.Find(1);
            // Act
            var res = controller.GetAChoisiOptionById(1).Result;
            // Assert
            Assert.AreEqual(expected, res.Value);
        }

        [TestMethod]
        public void GetAChoisiOptionByIdTest_AvecMoq()
        {
            // Arrange

            var mockRepository = new Mock<IDataRepository<AChoisiOption>>();

            


            AChoisiOption option = new AChoisiOption
            {
                IdConfigurationMoto = 1,
                IdEquipementMoto = 1,
            };
            // Act
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(option);
            var userController = new AChoisiOptionController(mockRepository.Object);

            var actionResult = userController.GetAChoisiOptionById(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as AChoisiOption);
        }

        /// <summary>
        /// Test PutCarteBancaireTest 
        /// </summary>
        [TestMethod()]
        public void PutAChoisiOptionTest()
        {
            // Arrange
            AChoisiOption optionAtester = context.AChoisiOptions.Find(1);

            // Act
            var res = controller.PutAChoisiOption(1, optionAtester);

            // Arrange
            AChoisiOption nouvelleoption = context.AChoisiOptions.Find(1);
            Assert.AreEqual(optionAtester, nouvelleoption);
        }

        [TestMethod]
        public void PutAChoisiOptionTestAvecMoq()
        {

            // Arrange
            AChoisiOption optionToUpdate = new AChoisiOption
            {
                IdConfigurationMoto = 200,
                IdEquipementMoto = 100,
            };
            AChoisiOption updatedOption = new AChoisiOption
            {
                IdConfigurationMoto = 100,
                IdEquipementMoto = 100,
            };

            var mockRepository = new Mock<IDataRepository<AChoisiOption>>();
            mockRepository.Setup(repo => repo.GetByIdAsync(21000)).ReturnsAsync(optionToUpdate);
            mockRepository.Setup(repo => repo.UpdateAsync(optionToUpdate, updatedOption)).Returns(Task.CompletedTask);


            var controller = new AChoisiOptionController(mockRepository.Object);

            // Act
            var result = controller.PutAChoisiOption(200, updatedOption).Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult), "La réponse n'est pas du type attendu NoContentResult");
        }

        /// <summary>
        /// Test PostUtilisateur 
        /// </summary>
        /// 
        [TestMethod()]
        public void PostAChoisiOptionTest()
        {
            // Arrange

            AChoisiOption option = new AChoisiOption
            {
                IdConfigurationMoto = 1000,
                IdEquipementMoto = 1000,
            };

            // Act
            var result = controller.PostAChoisiOption(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert
            // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            AChoisiOption? optionRecupere = context.AChoisiOptions
                .Where(u => u.IdConfigurationMoto == option.IdConfigurationMoto && u.IdEquipementMoto == option.IdEquipementMoto)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdConfigurationMoto = optionRecupere.IdConfigurationMoto;
            option.IdEquipementMoto = optionRecupere.IdEquipementMoto;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");
        }

        [TestMethod]
        public void PostAChoisiOptionTest_Mok()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<AChoisiOption>>();
            var userController = new AChoisiOptionController(mockRepository.Object);



            // Arrange
            AChoisiOption option = new AChoisiOption
            {
                IdConfigurationMoto = 1000,
                IdEquipementMoto = 1000,
            };
            // Act
            var actionResult = userController.PostAChoisiOption(option).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<AChoisiOption>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(AChoisiOption), "Pas un Utilisateur");
            option.IdConfigurationMoto = ((AChoisiOption)result.Value).IdConfigurationMoto;
            option.IdEquipementMoto = ((AChoisiOption)result.Value).IdEquipementMoto;
            Assert.AreEqual(option, (AChoisiOption)result.Value, "Utilisateurs pas identiques");
        }



        /// <summary>
        /// Test Delete 
        /// </summary>

        [TestMethod()]
        public void DeleteAChoisiOptionTest()
        {
            // Arrange
            AChoisiOption option = new AChoisiOption
            {
                IdConfigurationMoto = 1000,
                IdEquipementMoto = 1000,
            };
            context.AChoisiOptions.Add(option);
            context.SaveChanges();

            // Act
            AChoisiOption optionCarte = context.AChoisiOptions.FirstOrDefault(u => u.IdEquipementMoto == option.IdEquipementMoto && u.IdConfigurationMoto == option.IdConfigurationMoto);
            _ = controller.DeleteAChoisiOption(optionCarte.IdConfigurationMoto).Result;

            // Arrange
            AChoisiOption res = context.AChoisiOptions.FirstOrDefault(u => u.IdConfigurationMoto == option.IdConfigurationMoto && u.IdEquipementMoto == option.IdEquipementMoto);
            Assert.IsNull(res, "utilisateur non supprimé");
        }


        [TestMethod]
        public void DeleteAChoisiOptionTest_AvecMoq()
        {

            // Arrange
            AChoisiOption option = new AChoisiOption
            {
                IdConfigurationMoto = 1000,
                IdEquipementMoto = 1000,
            };
            var mockRepository = new Mock<IDataRepository<AChoisiOption>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(option);
            var userController = new AChoisiOptionController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteAChoisiOption(1000).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}