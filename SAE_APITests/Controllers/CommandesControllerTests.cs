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
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
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
            // Arrange
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new CommandeManager(context);

            // Act
            var cartebancaire = new CommandesController(dataRepository);

            // Assert
            Assert.IsNotNull(cartebancaire, "L'instance de MaClasse ne devrait pas être null.");

        }

        /// <summary>
        /// Test Get 
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
        /// Test GetByIdTest 
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
        /// Test Put 
        /// </summary>
        [TestMethod()]
        public void PutCommandeTest()
        {
            Commande userAtester = context.Commandes.Find(1);

            // Act
            var res = controller.PutCommande(1, userAtester);

            // Arrange
            Commande nouvellecarte = context.Commandes.Find(1);
            Assert.AreEqual(userAtester, nouvellecarte);
        }

        [TestMethod]
        public void PutCommandeTest_AvecMoq()
        {
           Commande commande = new Commande
           {
               IdCommande = 100,
               PrixFraisLivraison = 30,
               DateCommande = new DateTime(2003, 3, 10),
               PrixTotal = 100,
           };

           Commande commande2 = new Commande
           {
               IdCommande = 100,
               PrixFraisLivraison = 30,
               DateCommande = new DateTime(2003, 3, 10),
               PrixTotal = 100,
           };

           //mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(userToUpdate);
           //mockRepository.Setup(x => x.UpdateAsync(userToUpdate, userUpdated)).Returns(Task.CompletedTask);

           //// Act
           //var actionResult = userController.PutUtilisateur(100, userUpdated).Result;

           // Assert
           //Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Not a NoContentResult");

        }

        /// <summary>
        /// Test Post
        /// </summary>
        [TestMethod()]
        public void PostCommandeTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Commande>>();
            var userController = new CommandesController(mockRepository.Object);



            // Arrange
            Commande commande1 = new Commande
            {
                IdCommande = 100,
                PrixFraisLivraison = 30,
                DateCommande = new DateTime(2003, 3, 10),
                PrixTotal = 100,
            };

            // Act
            var actionResult = userController.PostCommande(commande1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Commande>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Commande), "Pas un Commande");

            commande1.IdCommande = ((Commande)result.Value).IdCommande;

            Assert.AreEqual(commande1, (Commande)result.Value, "Commande pas identiques");
        }
        /// <summary>
        /// Test Post Avex MOQ 
        /// </summary>
        [TestMethod()]
        public void PostCommandeTest_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Commande>>();
            var userController = new CommandesController(mockRepository.Object);

            // Arrange
            Commande catre = new Commande
            {
                IdCommande = 100,
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
            //// Crée une série fictive pour supprimer
            //var carteASupprimer = new Commande()
            //{
            //    IdCommande = 100,
            //    PrixFraisLivraison = 30,
            //    DateCommande = new DateTime(10 / 03 / 2003),
            //    PrixTotal = 100,
            //};

            //_context.Utilisateurs.Add(utilisateurASupprimer);
            //_context.SaveChanges();

            //// Ajoute la série fictive à la base de données pour la supprimer ensuite
            //utilisateursController.DeleteUtilisateur(utilisateurASupprimer.UtilisateurId).Wait();

            //List<Utilisateur> listeUtilisateurs = utilisateursController.GetUtilisateurs().Result.Value.ToList();

            //// Vérifie si la série a été supprimée en essayant de la récupérer
            //var deletedUser = listeUtilisateurs.FirstOrDefault(u => u.UtilisateurId == utilisateurASupprimer.UtilisateurId);
            //Assert.IsNull(deletedUser, "L'utilisateur n'a pas été supprimée de la base de données.");
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