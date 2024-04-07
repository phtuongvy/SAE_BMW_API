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
using System.Xml.Linq;

namespace SAE_API.Controllers.Tests
{
    [TestClass()]
    public class SegementsControllerTests
    {
        private SegementsController controller;
        private BMWDBContext context;
        private IDataRepository<Segement> dataRepository;


        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new SegementManager(context);
            controller = new SegementsController(dataRepository);
        }


        /// <summary>
        /// Test Contrôleur 
        /// </summary>

        [TestMethod()]
        public void SegementsControllerTest()
        {

        }

        /// <summary>
        /// Test GetUtilisateursTest 
        /// </summary>
        [TestMethod()]
        public void GetSegementsTest()
        {
            // Arrange
            List<Segement> segement = context.Segements.ToList();
            // Act
            var res = controller.GetSegements().Result;
            // Assert
            CollectionAssert.AreEqual(segement, res.Value.ToList(), "Les listes ne sont pas identiques");
        }

        /// <summary>
        /// Test GetUtilisateurByIdTest 
        /// </summary>
        [TestMethod()]
        public void GetSegementByIdTest()
        {
            // Arrange
            Segement segement = context.Segements.Find(1);
            // Act
            var res = controller.GetSegementById(1).Result;
            // Assert
            Assert.AreEqual(segement, res.Value);
        }

        [TestMethod()]
        public void GetSegementByIdTest_AvecMoq()
        {
            // Arrange
            var fakeId = 100;
            var mockRepository = new Mock<IDataRepository<Segement>>();
            var controller = new SegementsController(mockRepository.Object);

            Segement segement = new Segement
            {
                IdSegement = 100,
                NomSegement = "test"
            };
            // Act

            mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(segement);
            var actionResult = controller.GetSegementById(100).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(segement, actionResult.Value as Segement);
        }

        /// <summary>
        /// Test PutUtilisateurTest 
        /// </summary>
        [TestMethod()]
        public void PutSegementTest()
        {
            // Arrange
            Segement segement = context.Segements.Find(1);

            // Act
            var res = controller.PutSegement(1, segement);

            // Arrange
            Segement segement_nouveau = context.Segements.Find(1);
            Assert.AreEqual(segement, segement_nouveau);
        }

        [TestMethod]
        public void PutSegementTest_AvecMoq()
        {
            // Arrange
            var fakeId = 100;
            var segementToUpdate = new Segement
            {
                IdSegement = 100,
                NomSegement = "test"
            };

            var mockRepository = new Mock<IDataRepository<Segement>>();
            mockRepository.Setup(x => x.GetByIdAsync(fakeId))
                .ReturnsAsync(segementToUpdate); // Simule la récupération de l'équipement existant
            mockRepository.Setup(x => x.UpdateAsync(segementToUpdate, segementToUpdate)).Returns(Task.CompletedTask);

            var controller = new SegementsController(mockRepository.Object);

            // Act
            var actionResult = controller.PutSegement(fakeId, segementToUpdate).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult)); // On s'attend à ce qu'aucun contenu ne soit retourné pour une mise à jour réussie
            mockRepository.Verify(); // Vérifie que toutes les configurations vérifiables sur le mock ont bien été appelées
        }


        /// <summary>
        /// Test PostUtilisateurTest 
        /// </summary>
        [TestMethod()]
        public void PostSegementTestAvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Segement>>();
            var controller = new SegementsController(mockRepository.Object);
            var fakeId = 100;
            // Arrange
            Segement segement = new Segement
            {
                IdSegement = 100,
                NomSegement = "test"
            };

            // Act
            var actionResult = controller.PostSegement(segement).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Segement>), "Pas un ActionResult<Commande>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var okResult = actionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(okResult);
            var resultValue = okResult.Value as Segement;
            Assert.IsNotNull(resultValue);
        }

        /// <summary>
        /// Test PostUtilisateurTest 
        /// </summary>
        [TestMethod]
        public void PostSegementTest()
        {
            //// Arrange

            Segement segement = new Segement
            {
                NomSegement = "test"
            };
            // Act
            var actionResult = controller.PostSegement(segement).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un OkObjectResult");
            var okResult = actionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(Segement), "Pas un CompteClient");
            var tailleResult = okResult.Value as Segement;
            Assert.IsNotNull(tailleResult);

            context.Segements.Remove(segement);
            context.SaveChangesAsync();

        }

        /// <summary>
        /// Test DeleteUtilisateurTest 
        /// </summary>
        [TestMethod()]
        public void DeleteSegementTest()
        {
            // Arrange

            Segement segement = new Segement
            {
                NomSegement = "test"
            };

            context.Segements.Add(segement);
            context.SaveChanges();

            // Act
            Segement deletedSegement = context.Segements.FirstOrDefault(u => u.IdSegement == segement.IdSegement);
            _ = controller.DeleteSegement(deletedSegement.IdSegement).Result;

            // Arrange
            Segement res = context.Segements.FirstOrDefault(u => u.IdSegement == deletedSegement.IdSegement);
            Assert.IsNull(res, "equipement non supprimé");
        }

        [TestMethod()]
        public void DeleteSegementTest_MOq()
        {
            var mockRepository = new Mock<IDataRepository<Segement>>();
            var controller = new SegementsController(mockRepository.Object);
            var fakeId = 100;
            // Arrange
            Segement segement = new Segement
            {
                IdSegement = 100,
                NomSegement = "test"
            };

            // Act
            mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(segement);
            var actionResult = controller.DeleteSegement(segement.IdSegement).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}