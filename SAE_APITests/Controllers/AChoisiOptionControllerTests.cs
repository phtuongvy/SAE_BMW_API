using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
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
            List<AChoisiOption> expected = context.AChoisiOptions.ToList();
            // Act
            var res = controller.GetAChoisiOptions().Result;
            // Assert
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }

        /// <summary>
        /// Test GetCarteBancaireById 
        /// </summary>

        [TestMethod()]
        public void GetAChoisiOptionByIdTest()
        {
            // Arrange
            AChoisiOption expected = context.AChoisiOptions.Find(40,1 );
            // Act
            var res = controller.GetAChoisiOptionById(40 , 1).Result;
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
                IdConfigurationMoto = 40,
                IdEquipementMoto = 1,
            };
            // Act
            mockRepository.Setup(x => x.GetByIdAsync(40,1).Result).Returns(option);
            var userController = new AChoisiOptionController(mockRepository.Object);

            var actionResult = userController.GetAChoisiOptionById(40, 1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as AChoisiOption);
        }

        #region Test PutAChoisiOptionTestAsync
        /// <summary>
        /// Teste la méthode PutAChoisiOption pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutAChoisiOption_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var aChoisiOption = new AChoisiOption { IdConfigurationMoto = 1, IdEquipementMoto = 2 };

            // Act
            var result = await controller.PutAChoisiOption(3, 4, aChoisiOption);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutAChoisiOption_ReturnsNotFound_WhenAChoisiOptionDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<AChoisiOption>>();
            var _controller = new AChoisiOptionController(mockRepository.Object);
            var aChoisiOption = new AChoisiOption { IdConfigurationMoto = 1, IdEquipementMoto = 2 };
            mockRepository.Setup(x => x.GetByIdAsync(1, 2)).ReturnsAsync((AChoisiOption)null);

            // Act
            var result = await _controller.PutAChoisiOption(1, 2, aChoisiOption);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task PutAChoisiOption_ReturnsCreatedAtAction_WhenUpdateIsSuccessful()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<AChoisiOption>>();
            var _controller = new AChoisiOptionController(mockRepository.Object);
            var aChoisiOption = new AChoisiOption { IdConfigurationMoto = 1, IdEquipementMoto = 2 };
            mockRepository.Setup(x => x.GetByIdAsync(1, 2)).ReturnsAsync(aChoisiOption);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<AChoisiOption>(), It.IsAny<AChoisiOption>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutAChoisiOption(1, 2, aChoisiOption);

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult)); // Cela ne retourne pas de valeur mais vérifie le type
            var createdAtActionResult = result as CreatedAtActionResult; // Utilisez 'as' pour tenter de convertir
            Assert.IsNotNull(createdAtActionResult); // Assurez-vous que la conversion a réussi
            Assert.AreEqual("GetAChoisiOptions", createdAtActionResult.ActionName); // Vérifiez que le nom de l'action est correct
        }
        #endregion

        /// <summary>
        /// Test PostUtilisateur 
        /// </summary>
        /// 
        [TestMethod()]
        public async Task PostAChoisiOptionTestAsync()
        {
            // Arrange

            AChoisiOption option = new AChoisiOption
            {
                IdConfigurationMoto = 40,
                IdEquipementMoto = 7,
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

            context.AChoisiOptions.Remove(option);
            await context.SaveChangesAsync();
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
                IdConfigurationMoto = 40,
                IdEquipementMoto = 7,
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
                IdConfigurationMoto = 40,
                IdEquipementMoto = 7,
            };
            context.AChoisiOptions.Add(option);
            context.SaveChanges();

            // Act
            AChoisiOption option1= context.AChoisiOptions.FirstOrDefault(u => u.IdEquipementMoto == option.IdEquipementMoto && u.IdConfigurationMoto == option.IdConfigurationMoto);
            _ = controller.DeleteAChoisiOption(option.IdConfigurationMoto, option.IdEquipementMoto).Result;

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
                IdConfigurationMoto = 40,
                IdEquipementMoto = 7,
            };
            var mockRepository = new Mock<IDataRepository<AChoisiOption>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdConfigurationMoto, option.IdEquipementMoto).Result).Returns(option);
            var userController = new AChoisiOptionController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteAChoisiOption(option.IdConfigurationMoto, option.IdEquipementMoto).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}