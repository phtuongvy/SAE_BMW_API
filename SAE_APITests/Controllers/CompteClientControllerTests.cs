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


        [TestInitialize]
        public void Init()
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
        /// Test GetUtilisateurs 
        /// </summary>
        [TestMethod()]
        public void GetUtilisateursTest()
        {
            // Arrange
            List<CompteClient> expected = context.CompteClients.ToList();
            // Act
            var res = controller.GetUtilisateurs().Result;
            // Assert
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }

        /// <summary>
        /// Test GetUtilisateurById 
        /// </summary>
        [TestMethod()]
        public void GetUtilisateurByIdTest()
        {
            // Arrange
            CompteClient expected = context.CompteClients.Find(1);
            // Act
            var res = controller.GetUtilisateurById(1).Result;
            // Assert
            Assert.AreEqual(expected, res.Value);
        }

        [TestMethod()]
        public void GetUtilisateurByIdTest_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<CompteClient>>();

            ICollection<Acquerir> AcquisC = new List<Acquerir>
            {
                new Acquerir { /* initialisez les propriétés de l'objet ici */ },
                new Acquerir { /* un autre objet Acquerir */ }
            };

            byte[] byteArray = { 0x0A, 0x0B, 0x0C, 0x0D };
            CompteClient catre = new CompteClient
            {
                IdCompteClient = 100,
                NomClient = "Lamy",
                PrenomClient = "Evan",
                CiviliteClient = "M",
                NumeroClient = "06 92 0920912",
                Email = "ricardonunesemilio",
                DatenaissanceClient = new DateTime(15 / 11 / 2004),
                Password = byteArray,
                ClientRole = "Client"
            };
            // Act

            mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(catre);
            var userController = new CompteClientController(mockRepository.Object);

            var actionResult = userController.GetUtilisateurById(100).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(catre, actionResult.Value as CompteClient);
        }

        /// <summary>
        /// Test GetUtilisateurByName 
        /// </summary>

        [TestMethod()]
        public void GetUtilisateurByNameTest()
        {
            // Arrange
            CompteClient expected = context.CompteClients.Find(1);
            // Act
            var res = controller.GetUtilisateurByName(expected.NomClient).Result;

            Assert.AreEqual(expected, res.Value);
        }


        /// <summary>
        /// Test PutUtilisateur 
        /// </summary>
        [TestMethod()]
        public void PutUtilisateurTest()
        {

            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            CompteClient compte = context.CompteClients.Find(1);

            // Act
            var res = controller.PutUtilisateur(1, compte);

            // Arrange
            CompteClient compteClient = context.CompteClients.Find(1);
            Assert.AreEqual(compte, compteClient);
        }

        [TestMethod]
        public void PutUtilisateurTest_AvecMoq()
        {
            // Arrange
            byte[] byteArray = { 0x0A, 0x0B, 0x0C, 0x0D };

            CompteClient compte = new CompteClient
            {
                IdCompteClient = 100,
                NomClient = "Lamy",
                PrenomClient = "Evan",
                CiviliteClient = "M",
                NumeroClient = "06 92 0920912",
                Email = "Evan.lamy@gmail.com",
                DatenaissanceClient = new DateTime(15 / 11 / 2004),
                Password = byteArray,
                ClientRole = "Client"
            };

            CompteClient compte2 = new CompteClient
            {
                IdCompteClient = 101,
                NomClient = "Ricardo",
                PrenomClient = "NUNES",
                CiviliteClient = "M",
                NumeroClient = "06 92 32 32 43",
                Email = "ricardonunesemilio@gmail.com",
                DatenaissanceClient = new DateTime(15 / 11 / 2004),
                Password = byteArray,
                ClientRole = "Client"
            };

            var mockRepository = new Mock<IDataRepository<CompteClient>>();
            mockRepository.Setup(x => x.GetByIdAsync(101).Result).Returns(compte2);
            var userController = new CompteClientController(mockRepository.Object);

            // Act
            var actionResult = userController.PutUtilisateur(2, compte).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        /// <summary>
        /// Test PostUtilisateur 
        /// </summary>
        [TestMethod()]
        public void PostUtilisateurTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<CompteClient>>();
            var userController = new CompteClientController(mockRepository.Object);

            // Arrange
            byte[] byteArray = { 0x0A, 0x0B, 0x0C, 0x0D };
            CompteClient compte = new CompteClient
            {
                IdCompteClient = 100,
                NomClient = "Lamy",
                PrenomClient = "Evan",
                CiviliteClient = "M",
                NumeroClient = "06 92 0920912",
                Email = "ricardonunesemilio",
                DatenaissanceClient = new DateTime(15 / 11 / 2004),
                Password = byteArray,
                ClientRole = "Client"
            };

            // Act
            var actionResult = userController.PostUtilisateur(compte).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<CompteClient>), "Pas un ActionResult<Commande>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(CompteClient), "Pas un Commande");

            compte.IdCompteClient = ((CompteClient)result.Value).IdCompteClient;
            Assert.AreEqual(compte, (CompteClient)result.Value, "Commande pas identiques");
        }

        /// <summary>
        /// Test DeleteUtilisateu 
        /// </summary>
        [TestMethod()]
        public void DeleteUtilisateurTest()
        {
            // Arrange
            byte[] byteArray = { 0x0A, 0x0B, 0x0C, 0x0D };
            CompteClient compte = new CompteClient
            {
                IdCompteClient = 100,
                NomClient = "Lamy",
                PrenomClient = "Evan",
                CiviliteClient = "M",
                NumeroClient = "06 92 0920912",
                Email = "ricardonunesemilio",
                DatenaissanceClient = new DateTime(15 / 11 / 2004),
                Password = byteArray,
                ClientRole = "Client"
            };


            context.CompteClients.Add(compte);
            context.SaveChanges();

            // Act
            CompteClient deletedCarte = context.CompteClients.FirstOrDefault(u => u.IdCompteClient == compte.IdCompteClient);
            _ = controller.DeleteUtilisateur(deletedCarte.IdCompteClient).Result;

            // Arrange
            CompteClient res = context.CompteClients.FirstOrDefault(u => u.IdCompteClient == compte.IdCompteClient);
            Assert.IsNull(res, "utilisateur non supprimé");
        }

        [TestMethod()]
        public void DeleteUtilisateurTest_Moq()
        {
            var mockRepository = new Mock<IDataRepository<CompteClient>>();
            var userController = new CompteClientController(mockRepository.Object);
            // Arrange
            byte[] byteArray = { 0x0A, 0x0B, 0x0C, 0x0D };
            CompteClient compte = new CompteClient
            {
                IdCompteClient = 100,
                NomClient = "Lamy",
                PrenomClient = "Evan",
                CiviliteClient = "M",
                NumeroClient = "06 92 0920912",
                Email = "ricardonunesemilio",
                DatenaissanceClient = new DateTime(15 / 11 / 2004),
                Password = byteArray,
                ClientRole = "Client"
            };

            // Act
            mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(compte);
            var actionResult = userController.DeleteUtilisateur(compte.IdCompteClient).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}