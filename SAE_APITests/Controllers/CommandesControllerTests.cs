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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SAE_API.Controllers.Tests
{
    [TestClass()]
    public class CommandesControllerTests
    {
        private CommandesController controller;
        private BMWDBContext context;
        private IDataRepository<Commande> dataRepository;


        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server=localhost;port=5432;Database=RatingFilmsDB; uid=postgres;password=postgres;");
            context = new BMWDBContext(builder.Options);
            dataRepository = new CommandeManager(context);
            controller = new CommandesController(dataRepository);
        }

        /// <summary>
        /// Test Contrôleur 
        /// </summary>

        [TestMethod()]
        public void CommandesControllerTest()
        {

        }

        /// <summary>
        /// Test GetCommandesTest 
        /// </summary>
        [TestMethod()]
        public void GetCommandesTest()
        {
            // Arrange
            List<Commande> expected = context.Commandes.ToList();
            // Act
            var res = controller.GetCommandes().Result;
            // Assert
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }

        /// <summary>
        /// Test GetCommandeByIdTest 
        /// </summary>
        [TestMethod()]
        public void GetCommandeByIdTest()
        {
            // Arrange
            Commande expected = context.Commandes.Find(1);
            // Act
            var res = controller.GetCommandeById(1).Result;
            // Assert
            Assert.AreEqual(expected, res.Value);
        }

        [TestMethod()]
        public void GetCommandeByIdTest_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Commande>>();


            Commande commande = new Commande
            {
                IdCommande = 100,
                PrixFraisLivraison = 30,
                DateCommande = new DateTime(10 / 03 / 2003),
                PrixTotal = 100,
            };
            // Act

            mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(commande);
            var userController = new CommandesController(mockRepository.Object);

            var actionResult = userController.GetCommandeById(100).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(commande, actionResult.Value as Commande);
        }

        /// <summary>
        /// Test PutCommande 
        /// </summary>
        [TestMethod()]
        public void PutCommandeTest()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            Commande commande = context.Commandes.Find(1);

            // Act
            var res = controller.PutCommande(1, commande);

            // Arrange
            CarteBancaire nouvellecarte = context.CartesBancaires.Find(1);
            Assert.AreEqual(commande, nouvellecarte);
        }

        [TestMethod]
        public void PutUtilisateurTest_AvecMoq()
        {
            // Arrange
            byte[] byteArray = { 0x0A, 0x0B, 0x0C, 0x0D };

            Commande commande = new Commande
            {
                IdCommande = 100,
                PrixFraisLivraison = 30,
                DateCommande = new DateTime(10 / 03 / 2003),
                PrixTotal = 100,
            };

            Commande commande2 = new Commande
            {
                IdCommande = 101,
                PrixFraisLivraison = 30,
                DateCommande = new DateTime(10 / 03 / 2003),
                PrixTotal = 100,
            };

            var mockRepository = new Mock<IDataRepository<Commande>>();
            mockRepository.Setup(x => x.GetByIdAsync(101).Result).Returns(commande2);
            var userController = new CommandesController(mockRepository.Object);

            // Act
            var actionResult = userController.PutCommande(2, commande).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        /// <summary>
        /// Test PostUtilisateur 
        /// </summary>
        [TestMethod()]
        public void PostCommandeTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Commande>>();
            var userController = new CommandesController(mockRepository.Object);

            // Arrange
            Commande catre = new Commande
            {
                IdCommande = 1,
                PrixFraisLivraison = 30,
                DateCommande = new DateTime(10 / 03 / 2003),
                PrixTotal = 100,
            };
            // Act
            var actionResult = userController.PostCommande(catre).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Commande>), "Pas un ActionResult<Commande>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Commande), "Pas un Commande");
            catre.IdCommande = ((Commande)result.Value).IdCommande;
            Assert.AreEqual(catre, (Commande)result.Value, "Commande pas identiques");
        }

        /// <summary>
        /// Test DeleteUtilisateu 
        /// </summary>
        [TestMethod()]
        public void DeleteCommandeTest()
        {
            // Arrange

            Commande commande = new Commande
            {
                IdCommande = 100,
                PrixFraisLivraison = 30,
                DateCommande = new DateTime(10 / 03 / 2003),
                PrixTotal = 100,
            };

            context.Commandes.Add(commande);
            context.SaveChanges();

            // Act
            Commande deletedCommande = context.Commandes.FirstOrDefault(u => u.IdCommande == commande.IdCommande);
            _ = controller.DeleteCommande(deletedCommande.IdCommande).Result;

            // Arrange
            Commande res = context.Commandes.FirstOrDefault(u => u.IdCommande == commande.IdCommande);
            Assert.IsNull(res, "Commande non supprimé");
        }

        [TestMethod()]
        public void DeleteCommandeTest_MOq()
        {
            var mockRepository = new Mock<IDataRepository<Commande>>();
            var userController = new CommandesController(mockRepository.Object);

            // Arrange
            Commande commande = new Commande
            {
                IdCommande = 100,
                PrixFraisLivraison = 30,
                DateCommande = new DateTime(10 / 03 / 2003),
                PrixTotal = 100,
            };

            // Act
            mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(commande);
            var actionResult = userController.DeleteCommande(commande.IdCommande).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}