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
    public class EquipementsControllerTests
    {
        private EquipementsController controller;
        private BMWDBContext context;
        private IDataRepository<Equipement> dataRepository;


        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new EquipementManager(context);
            controller = new EquipementsController(dataRepository);
        }

        /// <summary>
        /// Test Contrôleur 
        /// </summary>

        [TestMethod()]
        public void EquipementsControllerTest()
        {

        }

        /// <summary>
        /// Test GetEquipementTest 
        /// </summary>
        [TestMethod()]
        public void GetEquipementTest()
        {
            // Arrange
            List<Equipement> expected = context.Equipements.ToList();
            // Act
            var res = controller.GetEquipement().Result;
            // Assert
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }

        /// <summary>
        /// Test GetEquipementByIdTest 
        /// </summary>
        [TestMethod()]
        public void GetEquipementByIdTest()
        {
            // Arrange
            Equipement expected = context.Equipements.Find(1);
            // Act
            var res = controller.GetEquipementById(1).Result;
            // Assert
            Assert.AreEqual(expected, res.Value);
        }

        [TestMethod()]
        public void GetEquipementByIdTest_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Equipement>>();
            var userController = new EquipementsController(mockRepository.Object);

            Equipement commande = new Equipement
            {
                IdEquipement = 100,
                IdSegment = 1,
                IdCollection = 1,
                IdTypeEquipement = 1,
                NomEquipement = "Casque bleu ",
                DescriptionEquipement = "blabla",
                DetailEquipement = "oui",
                DureeEquipement = "3",
                PrixEquipement = 6762,
                Sexe = "F"
            };
            // Act

            mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(commande);
            var actionResult = userController.GetEquipementById(100).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(commande, actionResult.Value as Equipement);
        }

        /// <summary>
        /// Test PutEquipementTest 
        /// </summary>
        [TestMethod()]
        public void PutEquipementTest()
        {
            // Arrange
            Equipement equipement = context.Equipements.Find(1);

            // Act
            var res = controller.PutEquipement(1, equipement);

            // Arrange
            Equipement equipement_nouveau = context.Equipements.Find(1);
            Assert.AreEqual(equipement, equipement_nouveau);
        }

        [TestMethod]
        public void PutEquipementTest_AvecMoq()
        {
            // Arrange
            var fakeId = 100;
            var equipementToUpdate = new Equipement
            {
                IdEquipement = fakeId,
                IdSegment = 1,
                IdCollection = 1,
                IdTypeEquipement = 1,
                NomEquipement = "Casque bleu",
                DescriptionEquipement = "blabla",
                DetailEquipement = "oui",
                DureeEquipement = "3",
                PrixEquipement = 6762,
                Sexe = "F"
            };

            var mockRepository = new Mock<IDataRepository<Equipement>>();
            mockRepository.Setup(x => x.GetByIdAsync(fakeId))
                .ReturnsAsync(equipementToUpdate); // Simule la récupération de l'équipement existant
            mockRepository.Setup(x => x.UpdateAsync(equipementToUpdate, equipementToUpdate)).Returns(Task.CompletedTask);

            var controller = new EquipementsController(mockRepository.Object);

            // Act
            var actionResult = controller.PutEquipement(fakeId, equipementToUpdate).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult)); // On s'attend à ce qu'aucun contenu ne soit retourné pour une mise à jour réussie
            mockRepository.Verify(); // Vérifie que toutes les configurations vérifiables sur le mock ont bien été appelées
        }
    

        /// <summary>
        /// Test PostEquipementTest 
        /// </summary>
        [TestMethod()]
        public void PostEquipementTestAvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Equipement>>();
            var userController = new EquipementsController(mockRepository.Object);

            // Arrange
            Equipement equipement = new Equipement
            {
                IdEquipement = 100,
                IdSegment = 1,
                IdCollection = 1,
                IdTypeEquipement = 1,
                NomEquipement = "Casque bleu ",
                DescriptionEquipement = "blabla",
                DetailEquipement = "oui",
                DureeEquipement = "3",
                PrixEquipement = 6762,
                Sexe = "F"
            };

            // Act
            var actionResult = userController.PostEquipement(equipement).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Equipement>), "Pas un ActionResult<Commande>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Equipement), "Pas un Commande");
            equipement.IdEquipement = ((Equipement)result.Value).IdEquipement;
            Assert.AreEqual(equipement, (Equipement)result.Value, "Commande pas identiques");
        }

        /// <summary>
        /// Test PostEquipementTest 
        /// </summary>
        [TestMethod]
        public void PostEquipementTest()
        {
            //// Arrange

            Equipement equipement = new Equipement
            {
                IdEquipement = 10000,
                IdSegment = 1,
                IdCollection = 1,
                IdTypeEquipement = 1,
                NomEquipement = "Casque bleu ",
                DescriptionEquipement = "blabla",
                DetailEquipement = "oui",
                DureeEquipement = "3",
                PrixEquipement = 6762,
                Sexe = "F"
            };

            //// Act
            var res = controller.PostEquipement(equipement).Result;

            //// Arrange
            Equipement? equipement_nouveau = context.Equipements.Find(10000);
            equipement.IdEquipement = equipement_nouveau.IdEquipement;
            Assert.AreEqual(equipement, equipement_nouveau, "equipements pas identiques ");
            controller.DeleteEquipement(equipement.IdEquipement);
            
        }

        /// <summary>
        /// Test DeleteEquipementTest 
        /// </summary>
        [TestMethod()]
        public void DeleteEquipementTest()
        {
            // Arrange

            Equipement equipement = new Equipement
            {
                IdEquipement = 100,
                IdSegment = 1,
                IdCollection = 1,
                IdTypeEquipement = 1,
                NomEquipement = "Casque bleu ",
                DescriptionEquipement = "blabla",
                DetailEquipement = "oui",
                DureeEquipement = "3",
                PrixEquipement = 6762,
                Sexe = "F"
            };

            context.Equipements.Add(equipement);
            context.SaveChanges();

            // Act
            controller.DeleteEquipement(equipement.IdEquipement);
            context.SaveChanges();

            // Arrange
            Equipement res = context.Equipements.Find(equipement.IdEquipement);
            Assert.IsNull(res, "Commande non supprimé");
        }

        [TestMethod()]
        public void DeleteEquipementTest_MOq()
        {
            var mockRepository = new Mock<IDataRepository<Equipement>>();
            var userController = new EquipementsController(mockRepository.Object);

            // Arrange
            Equipement equipement = new Equipement
            {
                IdEquipement = 100,
                IdSegment = 1,
                IdCollection = 1,
                IdTypeEquipement = 1,
                NomEquipement = "Casque bleu ",
                DescriptionEquipement = "blabla",
                DetailEquipement = "oui",
                DureeEquipement = "3",
                PrixEquipement = 6762,
                Sexe = "F"
            };

            // Act
            mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(equipement);
            var actionResult = userController.DeleteEquipement(equipement.IdEquipement).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}