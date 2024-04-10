using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class TailleEquipementsControllerTest
    {
        private TailleEquipementsController controller;
        private BMWDBContext context;
        private IDataRepository<TailleEquipement> dataRepository;


        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new TailleEquipementManager(context);
            controller = new TailleEquipementsController(dataRepository);
        }


        /// <summary>
        /// Test Contrôleur 
        /// </summary>

        [TestMethod()]
        public void TypeEquipementsControllersTest()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new TailleEquipementManager(context);

            // Act
            var option = new TailleEquipementsController(dataRepository);

            // Assert
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");
        }

        /// <summary>
        /// Test GetUtilisateursTest 
        /// </summary>
        [TestMethod()]
        public void GetTailleEquipementsTest()
        {
            // Arrange
            List<TailleEquipement> taille = context.TailleEquipements.ToList();
            // Act
            var res = controller.GetTailleEquipements().Result;
            // Assert
            CollectionAssert.AreEqual(taille, res.Value.ToList(), "Les listes ne sont pas identiques");
        }

        /// <summary>
        /// Test GetUtilisateurByIdTest 
        /// </summary>
        [TestMethod()]
        public void GetTailleEquipementByIdTest()
        {
            // Arrange
            TailleEquipement taille = context.TailleEquipements.Find(1);
            // Act
            var res = controller.GetTailleEquipementById(1).Result;
            // Assert
            Assert.AreEqual(taille, res.Value);
        }

        [TestMethod()]
        public void GetTailleEquipementByIdTest_AvecMoq()
        {
            // Arrange
            var fakeId = 100;
            var mockRepository = new Mock<IDataRepository<TailleEquipement>>();
            var userController = new TailleEquipementsController(mockRepository.Object);

            TailleEquipement taille = new TailleEquipement
            {
                IdTailleEquipement = 100,
                NomTailleEquipement = "Gros"
            };
            // Act

            mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(taille);
            var actionResult = userController.GetTailleEquipementById(100).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(taille, actionResult.Value as TailleEquipement);
        }

        /// <summary>
        /// Test PutUtilisateurTest 
        /// </summary>
        [TestMethod()]
        public void PutTailleEquipementTest()
        {
            // Arrange
            TailleEquipement taille = context.TailleEquipements.Find(1);

            // Act
            var res = controller.PutTailleEquipement(1, taille);

            // Arrange
            TailleEquipement taille_nouveau = context.TailleEquipements.Find(1);
            Assert.AreEqual(taille, taille_nouveau);
        }

        [TestMethod]
        public void PutTailleEquipementTest_AvecMoq()
        {
            // Arrange
            var fakeId = 100;
            var tailleToUpdate = new TailleEquipement
            {
                IdTailleEquipement = 100,
                NomTailleEquipement = "Gros"
            };

            var mockRepository = new Mock<IDataRepository<TailleEquipement>>();
            mockRepository.Setup(x => x.GetByIdAsync(fakeId))
                .ReturnsAsync(tailleToUpdate); // Simule la récupération de l'équipement existant
            mockRepository.Setup(x => x.UpdateAsync(tailleToUpdate, tailleToUpdate)).Returns(Task.CompletedTask);

            var controller = new TailleEquipementsController(mockRepository.Object);

            // Act
            var actionResult = controller.PutTailleEquipement(fakeId, tailleToUpdate).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult)); // On s'attend à ce qu'aucun contenu ne soit retourné pour une mise à jour réussie
            mockRepository.Verify(); // Vérifie que toutes les configurations vérifiables sur le mock ont bien été appelées
        }


        /// <summary>
        /// Test PostUtilisateurTest 
        /// </summary>
        [TestMethod()]
        public void PostTailleEquipementTestAvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<TailleEquipement>>();
            var userController = new TailleEquipementsController(mockRepository.Object);
            var fakeId = 100;
            // Arrange
            TailleEquipement taille = new TailleEquipement
            {
                IdTailleEquipement = 100,
                NomTailleEquipement = "Gros"
            };

            // Act
            var actionResult = userController.PostTailleEquipement(taille).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<TailleEquipement>), "Pas un ActionResult<Commande>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var okResult = actionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(okResult);
            var resultValue = okResult.Value as TailleEquipement;
            Assert.IsNotNull(resultValue);
        }

        /// <summary>
        /// Test PostUtilisateurTest 
        /// </summary>
        [TestMethod]
        public void PostTailleEquipementTest()
        {
            //// Arrange

            TailleEquipement taille = new TailleEquipement
            {
                NomTailleEquipement = "Gros"
            };
            // Act
            var actionResult = controller.PostTailleEquipement(taille).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un OkObjectResult");
            var okResult = actionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(TailleEquipement), "Pas un CompteClient");
            var tailleResult = okResult.Value as TailleEquipement;
            Assert.IsNotNull(tailleResult);

            context.TailleEquipements.Remove(taille);
            context.SaveChangesAsync();

        }

        /// <summary>
        /// Test DeleteUtilisateurTest 
        /// </summary>
        [TestMethod()]
        public void DeleteTailleEquipementTest()
        {
            // Arrange

            TailleEquipement taille = new TailleEquipement
            {
                NomTailleEquipement = "Gros"
            };

            context.TailleEquipements.Add(taille);
            context.SaveChanges();

            // Act
            TailleEquipement deletedTaille = context.TailleEquipements.FirstOrDefault(u => u.IdTailleEquipement == taille.IdTailleEquipement);
            _ = controller.DeleteTailleEquipement(deletedTaille.IdTailleEquipement).Result;

            // Arrange
            TailleEquipement res = context.TailleEquipements.FirstOrDefault(u => u.IdTailleEquipement == deletedTaille.IdTailleEquipement);
            Assert.IsNull(res, "equipement non supprimé");
        }

        [TestMethod()]
        public void DeleteTailleEquipementTest_MOq()
        {
            var mockRepository = new Mock<IDataRepository<TailleEquipement>>();
            var userController = new TailleEquipementsController(mockRepository.Object);
            var fakeId = 100;
            // Arrange
            TailleEquipement taille = new TailleEquipement
            {
                IdTailleEquipement = 100,
                NomTailleEquipement = "Gros"
            };

            // Act
            mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(taille);
            var actionResult = userController.DeleteTailleEquipement(taille.IdTailleEquipement).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

    }
}