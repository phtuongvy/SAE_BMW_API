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
    public class CompteClientControllerTests
    {
        private CompteClientController controller;
        private BMWDBContext context;
        private IDataRepository<CompteClient> dataRepository;


        //[testinitialize]
        public void init()
        {
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new CompteClientManager(context);
            controller = new CompteClientController(dataRepository);
        }


        /// <summary>
        /// Test Contrôleur 
        /// </summary>

        [TestMethod()]
        public void CompteClientControllerTest()
        {

        }

        /// <summary>
        /// Test GetUtilisateursTest 
        /// </summary>
        [TestMethod()]
        public void GetUtilisateursTest()
        {
            // Arrange
            List<CompteClient> client = context.CompteClients.ToList();
            // Act
            var res = controller.GetUtilisateurs().Result;
            // Assert
            CollectionAssert.AreEqual(client, res.Value.ToList(), "Les listes ne sont pas identiques");
        }

        /// <summary>
        /// Test GetUtilisateurByIdTest 
        /// </summary>
        [TestMethod()]
        public void GetUtilisateurByIdTest()
        {
            // Arrange
            CompteClient client = context.CompteClients.Find(1);
            // Act
            var res = controller.GetUtilisateurById(1).Result;
            // Assert
            Assert.AreEqual(client, res.Value);
        }

        [TestMethod()]
        public void GetUtilisateurByIdTest_AvecMoq()
        {
            // Arrange
            var fakeId = 100;
            var mockRepository = new Mock<IDataRepository<CompteClient>>();
            var userController = new CompteClientController(mockRepository.Object);

            CompteClient client = new CompteClient
            {
                IdCompteClient = fakeId,
                NomClient = "test",
                PrenomClient = "test",
                CiviliteClient = "H",
                NumeroClient = "0651243978",
                Email = "254.test@etu.univ",
                DatenaissanceClient = new DateTime(2010,02,18),
                Password = "testmotdepasse123!",
                ClientRole = "user"
            };
            // Act

            mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(client);
            var actionResult = userController.GetUtilisateurById(100).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(client, actionResult.Value as CompteClient);
        }

        /// <summary>
        /// Test PutUtilisateurTest 
        /// </summary>
        [TestMethod()]
        public void PutUtilisateurTest()
        {
            // Arrange
            CompteClient client = context.CompteClients.Find(1);

            // Act
            var res = controller.PutUtilisateur(1, client);

            // Arrange
            CompteClient client_nouveau = context.CompteClients.Find(1);
            Assert.AreEqual(client, client_nouveau);
        }

        [TestMethod]
        public void PutUtilisateurTest_AvecMoq()
        {
            // Arrange
            var fakeId = 100;
            var equipementToUpdate = new CompteClient
            {
                IdCompteClient = fakeId,
                NomClient = "test",
                PrenomClient = "test",
                CiviliteClient = "H",
                NumeroClient = "0651243978",
                Email = "254.test@etu.univ",
                DatenaissanceClient = new DateTime(2010, 02, 18),
                Password = "testmotdepasse123!",
                ClientRole = "user"
            };

            var mockRepository = new Mock<IDataRepository<CompteClient>>();
            mockRepository.Setup(x => x.GetByIdAsync(fakeId))
                .ReturnsAsync(equipementToUpdate); // Simule la récupération de l'équipement existant
            mockRepository.Setup(x => x.UpdateAsync(equipementToUpdate, equipementToUpdate)).Returns(Task.CompletedTask);

            var controller = new CompteClientController(mockRepository.Object);

            // Act
            var actionResult = controller.PutUtilisateur(fakeId, equipementToUpdate).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult)); // On s'attend à ce qu'aucun contenu ne soit retourné pour une mise à jour réussie
            mockRepository.Verify(); // Vérifie que toutes les configurations vérifiables sur le mock ont bien été appelées
        }


        /// <summary>
        /// Test PostUtilisateurTest 
        /// </summary>
        [TestMethod()]
        public void PostUtilisateurTestAvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<CompteClient>>();
            var userController = new CompteClientController(mockRepository.Object);
            var fakeId = 100;
            // Arrange
            CompteClient client = new CompteClient
            {
                IdCompteClient = fakeId,
                NomClient = "test",
                PrenomClient = "test",
                CiviliteClient = "H",
                NumeroClient = "0651243978",
                Email = "254.test@etu.univ",
                DatenaissanceClient = new DateTime(2010, 02, 18),
                Password = "testmotdepasse123!",
                ClientRole = "user"
            };

            // Act
            var actionResult = userController.PostUtilisateur(client).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<CompteClient>), "Pas un ActionResult<Commande>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(CompteClient), "Pas un Commande");
            client.IdCompteClient = ((CompteClient)result.Value).IdCompteClient;
            Assert.AreEqual(client, (CompteClient)result.Value, "Commande pas identiques");
        }

        /// <summary>
        /// Test PostUtilisateurTest 
        /// </summary>
        [TestMethod]
        public void PostUtilisateurTest()
        {
            //// Arrange

            CompteClient client = new CompteClient
            {
                NomClient = "test",
                PrenomClient = "test",
                CiviliteClient = "H",
                NumeroClient = "0651243978",
                Email = "254.test@etu.univ",
                DatenaissanceClient = new DateTime(2010,02,18),
                Password = "testmotdepasse123!",
                ClientRole = "user"
            };
            // Act
            var actionResult = controller.PostUtilisateur(client).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<CompteClient>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(CompteClient), "Pas un Utilisateur");
            client.IdCompteClient = ((CompteClient)result.Value).IdCompteClient;
            Assert.AreEqual(client, (CompteClient)result.Value, "Utilisateurs pas identiques");

            context.CompteClients.Remove(client);
            context.SaveChangesAsync();

        }

        /// <summary>
        /// Test DeleteUtilisateurTest 
        /// </summary>
        [TestMethod()]
        public void DeleteUtilisateurTest()
        {
            // Arrange

            CompteClient client = new CompteClient
            {
                NomClient = "test",
                PrenomClient = "test",
                CiviliteClient = "H",
                NumeroClient = "0651243978",
                Email = "254.test@etu.univ",
                DatenaissanceClient = new DateTime(2010, 02, 18),
                Password = "testmotdepasse123!",
                ClientRole = "user"
            };

            context.CompteClients.AddAsync(client);
            context.SaveChanges();

            // Act
            CompteClient deletedClient = context.CompteClients.FirstOrDefault(u => u.IdCompteClient == client.IdCompteClient);
            _ = controller.DeleteUtilisateur(deletedClient.IdCompteClient).Result;

            // Arrange
            CompteClient res = context.CompteClients.FirstOrDefault(u => u.IdCompteClient == deletedClient.IdCompteClient);
            Assert.IsNull(res, "equipement non supprimé");
        }

        [TestMethod()]
        public void DeleteUtilisateurTest_MOq()
        {
            var mockRepository = new Mock<IDataRepository<CompteClient>>();
             var userController = new CompteClientController(mockRepository.Object);
            var fakeId = 100;
            // Arrange
            CompteClient client = new CompteClient
            {
                IdCompteClient = fakeId,
                NomClient = "test",
                PrenomClient = "test",
                CiviliteClient = "H",
                NumeroClient = "0651243978",
                Email = "254.test@etu.univ",
                DatenaissanceClient = new DateTime(2010, 02, 18),
                Password = "testmotdepasse123!",
                ClientRole = "user"
            };

            // Act
            mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(client);
            var actionResult = userController.DeleteUtilisateur(client.IdCompteClient).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}