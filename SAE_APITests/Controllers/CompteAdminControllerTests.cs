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
    public class CompteAdminControllerTests
    {
        private CompteAdminController controller;
        private BMWDBContext context;
        private IDataRepository<CompteAdmin> dataRepository;


        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new CompteAdminManager(context);
            controller = new CompteAdminController(dataRepository);
        }


        /// <summary>
        /// Test Contrôleur 
        /// </summary>

        [TestMethod()]
        public void CompteAdminControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new CompteAdminManager(context);

            // Act : appel de la méthode à tester
            var option = new CompteAdminController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");
        }

        /// <summary>
        /// Test GetCompteAdminsTest 
        /// </summary>
        [TestMethod()]
        public void GetCompteAdminsTest()
        {
            // Arrange
            List<CompteAdmin> client = context.CompteAdmins.ToList();
            // Act
            var res = controller.GetCompteAdmins().Result;
            // Assert
            CollectionAssert.AreEqual(client, res.Value.ToList(), "Les listes ne sont pas identiques");
        }

        /// <summary>
        /// Test GetCompteAdminByIdTest 
        /// </summary>
        [TestMethod()]
        public void GetCompteAdminByIdTest()
        {
            // Arrange
            CompteAdmin client = context.CompteAdmins.Find(1);
            // Act
            var res = controller.GetCompteAdminById(1).Result;
            // Assert
            Assert.AreEqual(client, res.Value);
        }

        [TestMethod()]
        public void GetCompteAdminByIdTest_AvecMoq()
        {
            // Arrange
            var fakeId = 100;
            var mockRepository = new Mock<IDataRepository<CompteAdmin>>();
            var userController = new CompteAdminController(mockRepository.Object);

            CompteAdmin client = new CompteAdmin
            {
                IdCompteClient = fakeId,
                
            };
            // Act

            mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(client);
            var actionResult = userController.GetCompteAdminById(100).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(client, actionResult.Value as CompteAdmin);
        }

        /// <summary>
        /// Test PutCompteAdminTest 
        /// </summary>
        [TestMethod()]
        public void PutCompteAdminTest()
        {
            // Arrange
            CompteAdmin client = context.CompteAdmins.Find(1);

            // Act
            var res = controller.PutCompteAdmin(1, client);

            // Arrange
            CompteAdmin client_nouveau = context.CompteAdmins.Find(1);
            Assert.AreEqual(client, client_nouveau);
        }

        [TestMethod]
        public void PutCompteAdminTest_AvecMoq()
        {
            // Arrange
            var fakeId = 100;
            var equipementToUpdate = new CompteAdmin
            {
                IdCompteClient = fakeId,

            };

            var mockRepository = new Mock<IDataRepository<CompteAdmin>>();
            mockRepository.Setup(x => x.GetByIdAsync(fakeId))
                .ReturnsAsync(equipementToUpdate); // Simule la récupération de l'équipement existant
            mockRepository.Setup(x => x.UpdateAsync(equipementToUpdate, equipementToUpdate)).Returns(Task.CompletedTask);

            var controller = new CompteAdminController(mockRepository.Object);

            // Act
            var actionResult = controller.PutCompteAdmin(fakeId, equipementToUpdate).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult)); // On s'attend à ce qu'aucun contenu ne soit retourné pour une mise à jour réussie
            mockRepository.Verify(); // Vérifie que toutes les configurations vérifiables sur le mock ont bien été appelées
        }


        /// <summary>
        /// Test PostCompteAdminTest 
        /// </summary>
        [TestMethod()]
        public void PostCompteAdminTestAvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<CompteAdmin>>();
            var userController = new CompteAdminController(mockRepository.Object);
            var fakeId = 1;
            // Arrange
            CompteAdmin client = new CompteAdmin
            {
                IdCompteClient = fakeId,
               
            };

            // Act
            var actionResult = userController.PostCompteAdmin(client).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult), "Pas un OkObjectResult");
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var resultValue = okResult.Value as CompteAdmin;
            Assert.IsNotNull(resultValue);
        }

        /// <summary>
        /// Test PostCompteAdminTest 
        /// </summary>
        [TestMethod]
        public void PostCompteAdminTest()
        {
            //// Arrange
            var fakeId = 1;
            CompteAdmin client = new CompteAdmin
            {
                IdCompteClient = fakeId,
            };
            // Act
            var actionResult = controller.PostCompteAdmin(client).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(OkObjectResult), "Pas un OkObjectResult");
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(CompteAdmin), "Pas un CompteAdmin");
            var clientResult = okResult.Value as CompteAdmin;
            Assert.IsNotNull(clientResult);

            context.CompteAdmins.Remove(client);
            context.SaveChangesAsync();

        }

        /// <summary>
        /// Test DeleteCompteAdminTest 
        /// </summary>
        [TestMethod()]
        public void DeleteCompteAdminTest()
        {
            // Arrange
            var fakeId = 100;
            CompteAdmin client = new CompteAdmin
            {
                IdCompteClient = fakeId,
            };

            context.CompteAdmins.Add(client);
            context.SaveChanges();

            // Act
            CompteAdmin deletedClient = context.CompteAdmins.FirstOrDefault(u => u.IdCompteClient == client.IdCompteClient);
            _ = controller.DeleteCompteAdmin(deletedClient.IdCompteClient).Result;

            // Arrange
            CompteAdmin res = context.CompteAdmins.FirstOrDefault(u => u.IdCompteClient == deletedClient.IdCompteClient);
            Assert.IsNull(res, "equipement non supprimé");
        }

        [TestMethod()]
        public void DeleteCompteAdminTest_MOq()
        {
            var mockRepository = new Mock<IDataRepository<CompteAdmin>>();
            var userController = new CompteAdminController(mockRepository.Object);
            var fakeId = 1;
            // Arrange
            CompteAdmin client = new CompteAdmin
            {
                IdCompteClient = fakeId,
            
            };

            // Act
            mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(client);
            var actionResult = userController.DeleteCompteAdmin(client.IdCompteClient).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}