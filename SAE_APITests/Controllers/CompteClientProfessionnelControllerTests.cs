using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SAE_API.Controllers;
using SAE_API.Models.DataManager;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_API.Controllers.Tests
{
    [TestClass()]
    public class CompteClientProfessionnelControllerTests
    {
        private CompteClientProfessionnelController controller;
        private BMWDBContext context;
        private IDataRepository<CompteClientProfessionnel> dataRepository;


        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new CompteClientProfessionnelManager(context);
            controller = new CompteClientProfessionnelController(dataRepository);
        }


        /// <summary>
        /// Test Contrôleur 
        /// </summary>

        [TestMethod()]
        public void CompteClientProfessionnelControllerTest()
        {
            // Arrange : préparation des données attendues
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new CompteClientProfessionnelManager(context);

            // Act : appel de la méthode à tester
            var option = new CompteClientProfessionnelController(dataRepository);

            // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");
        }

        /// <summary>
        /// Test GetCompteClientProfessionnelsTest 
        /// </summary>
        [TestMethod()]
        public void GetCompteClientProfessionnelsTest()
        {
            // Arrange
            List<CompteClientProfessionnel> client = context.CompteClientProfessionnels.ToList();
            // Act
            var res = controller.GetCompteClientProfessionnels().Result;
            // Assert
            CollectionAssert.AreEqual(client, res.Value.ToList(), "Les listes ne sont pas identiques");
        }

        /// <summary>
        /// Test GetCompteClientProfessionnelByIdTest 
        /// </summary>
        [TestMethod()]
        public void GetCompteClientProfessionnelByIdTest()
        {
            // Arrange
            CompteClientProfessionnel client = context.CompteClientProfessionnels.Find(1);
            // Act
            var res = controller.GetCompteClientProfessionnelById(1).Result;
            // Assert
            Assert.AreEqual(client, res.Value);
        }

        [TestMethod()]
        public void GetCompteClientProfessionnelByIdTest_AvecMoq()
        {
            // Arrange
            var fakeId = 100;
            var mockRepository = new Mock<IDataRepository<CompteClientProfessionnel>>();
            var userController = new CompteClientProfessionnelController(mockRepository.Object);

            CompteClientProfessionnel client = new CompteClientProfessionnel
            {
                IdCompteClient = fakeId,

            };
            // Act

            mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(client);
            var actionResult = userController.GetCompteClientProfessionnelById(100).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(client, actionResult.Value as CompteClientProfessionnel);
        }

        /// <summary>
        /// Test PutCompteClientProfessionnelTest 
        /// </summary>
        [TestMethod()]
        public void PutCompteClientProfessionnelTest()
        {
            // Arrange
            CompteClientProfessionnel client = context.CompteClientProfessionnels.Find(1);

            // Act
            var res = controller.PutCompteClientProfessionnel(1, client);

            // Arrange
            CompteClientProfessionnel client_nouveau = context.CompteClientProfessionnels.Find(1);
            Assert.AreEqual(client, client_nouveau);
        }

        [TestMethod]
        public void PutCompteClientProfessionnelTest_AvecMoq()
        {
            // Arrange
            var fakeId = 100;
            var equipementToUpdate = new CompteClientProfessionnel
            {
                IdCompteClient = fakeId,

            };

            var mockRepository = new Mock<IDataRepository<CompteClientProfessionnel>>();
            mockRepository.Setup(x => x.GetByIdAsync(fakeId))
                .ReturnsAsync(equipementToUpdate); // Simule la récupération de l'équipement existant
            mockRepository.Setup(x => x.UpdateAsync(equipementToUpdate, equipementToUpdate)).Returns(Task.CompletedTask);

            var controller = new CompteClientProfessionnelController(mockRepository.Object);

            // Act
            var actionResult = controller.PutCompteClientProfessionnel(fakeId, equipementToUpdate).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult)); // On s'attend à ce qu'aucun contenu ne soit retourné pour une mise à jour réussie
            mockRepository.Verify(); // Vérifie que toutes les configurations vérifiables sur le mock ont bien été appelées
        }


        /// <summary>
        /// Test PostCompteClientProfessionnelTest 
        /// </summary>
        [TestMethod()]
        public void PostCompteClientProfessionnelTestAvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<CompteClientProfessionnel>>();
            var userController = new CompteClientProfessionnelController(mockRepository.Object);



            // Arrange
            CompteClientProfessionnel option = new CompteClientProfessionnel
            {
                IdCompteClient = 31
            };

            // Act
            var actionResult = userController.PostCompteClientProfessionnel(option).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<CompteClientProfessionnel>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(CompteClientProfessionnel), "Pas un Utilisateur");

            option.IdCompteClient = ((CompteClientProfessionnel)result.Value).IdCompteClient;

            Assert.AreEqual(option, (CompteClientProfessionnel)result.Value, "Utilisateurs pas identiques");
        }

        /// <summary>
        /// Test PostCompteClientProfessionnelTest 
        /// </summary>
        [TestMethod]
        public async Task PostCompteClientProfessionnelTest()
        {
            // Arrange : préparation des données attendues
            CompteClientProfessionnel compte = new CompteClientProfessionnel
            {
                IdCompteClient = 29,
                NomCompagnie = " test"
            };

            // Act : appel de la méthode à tester
            var result = controller.PostCompteClientProfessionnel(compte).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert : vérification que les données obtenues correspondent aux données attendues
            CompteClientProfessionnel? optionRecupere = context.CompteClientProfessionnels
                .Where(u => u.IdCompteClient == compte.IdCompteClient)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            compte.IdCompteClient = optionRecupere.IdCompteClient;

            Assert.AreEqual(optionRecupere, compte, "Utilisateurs pas identiques");

            context.CompteClientProfessionnels.Remove(compte);
            await context.SaveChangesAsync();

        }

        /// <summary>
        /// Test DeleteCompteClientProfessionnelTest 
        /// </summary>
        [TestMethod()]
        public void DeleteCompteClientProfessionnelTest()
        {
            // Arrange
            var fakeId = 30;
            CompteClientProfessionnel client = new CompteClientProfessionnel
            {
                IdCompteClient = fakeId,
                NomCompagnie = " test"
            };

            context.CompteClientProfessionnels.Add(client);
            context.SaveChanges();

            // Act
            CompteClientProfessionnel deletedClient = context.CompteClientProfessionnels.FirstOrDefault(u => u.IdCompteClient == client.IdCompteClient);
            _ = controller.DeleteCompteClientProfessionnel(deletedClient.IdCompteClient).Result;

            // Arrange
            CompteClientProfessionnel res = context.CompteClientProfessionnels.FirstOrDefault(u => u.IdCompteClient == deletedClient.IdCompteClient);
            Assert.IsNull(res, "equipement non supprimé");
        }

        [TestMethod()]
        public void DeleteCompteClientProfessionnelTest_MOq()
        {
            var mockRepository = new Mock<IDataRepository<CompteClientProfessionnel>>();
            var userController = new CompteClientProfessionnelController(mockRepository.Object);
            var fakeId = 5;
            // Arrange
            CompteClientProfessionnel client = new CompteClientProfessionnel
            {
                IdCompteClient = fakeId,

            };

            // Act
            mockRepository.Setup(x => x.GetByIdAsync(client.IdCompteClient).Result).Returns(client);
            var actionResult = userController.DeleteCompteClientProfessionnel(client.IdCompteClient).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}