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
    public class RepriseMotosControllerTests
    {
        private RepriseMotosController controller;
        private BMWDBContext context;
        private IDataRepository<RepriseMoto> dataRepository;


        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new RepriseMotoManager(context);
            controller = new RepriseMotosController(dataRepository);
        }
        /// <summary>
        /// Test Contrôleur 
        /// </summary>

        [TestMethod()]
        public void RepriseMotosControllerTest()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new RepriseMotoManager(context);

            // Act
            var option = new RepriseMotosController(dataRepository);

            // Assert
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");
        }

        /// <summary>
        /// Test GetUtilisateursTest 
        /// </summary>
        [TestMethod()]
        public void GetRepriseMotosTest()
        {
            // Arrange
            List<RepriseMoto> reprise = context.RepriseMotos.ToList();
            // Act
            var res = controller.GetRepriseMotos().Result;
            // Assert
            CollectionAssert.AreEqual(reprise, res.Value.ToList(), "Les listes ne sont pas identiques");
        }

        /// <summary>
        /// Test GetUtilisateurByIdTest 
        /// </summary>
        [TestMethod()]
        public void GetRepriseMotoByIdTest()
        {
            // Arrange
            RepriseMoto reprise = context.RepriseMotos.Find(1);
            // Act
            var res = controller.GetRepriseMotoById(1).Result;
            // Assert
            Assert.AreEqual(reprise, res.Value);
        }

        [TestMethod()]
        public void GetRepriseMotoByIdTest_AvecMoq()
        {
            // Arrange
            var fakeId = 100;
            var mockRepository = new Mock<IDataRepository<RepriseMoto>>();
            var controller = new RepriseMotosController(mockRepository.Object);

            RepriseMoto reprise = new RepriseMoto
            {
                IdEstimationMoto = 100,
                IdDateLivraison = 1,
                IdCompteClient = 1,
                MarqueEstimationMoto = "test",
                ModeleEstimationMoto = "test",
                MoisImmatriculation = 10,
                AnneImmatriculation = 2010,
                PrixEstimationMoto = 100,
                KilometrageEstimationMoto = 1000,
                VersionEstimationMoto = "test"
            };
            // Act

            mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(reprise);
            var actionResult = controller.GetRepriseMotoById(100).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(reprise, actionResult.Value as RepriseMoto);
        }

        /// <summary>
        /// Test PutUtilisateurTest 
        /// </summary>
        [TestMethod()]
        public void PutRepriseMotoTest()
        {
            // Arrange
            RepriseMoto reprise = context.RepriseMotos.Find(1);

            // Act
            var res = controller.PutRepriseMoto(1, reprise);

            // Arrange
            RepriseMoto reprise_nouveau = context.RepriseMotos.Find(1);
            Assert.AreEqual(reprise, reprise_nouveau);
        }

        [TestMethod]
        public void PutRepriseMotoTest_AvecMoq()
        {
            // Arrange
            var fakeId = 100;
            var repriseToUpdate = new RepriseMoto
            {
                IdEstimationMoto = 100,
                IdDateLivraison = 1,
                IdCompteClient = 1,
                MarqueEstimationMoto = "test",
                ModeleEstimationMoto = "test",
                MoisImmatriculation = 10,
                AnneImmatriculation = 2010,
                PrixEstimationMoto = 100,
                KilometrageEstimationMoto = 1000,
                VersionEstimationMoto = "test"
            };

            var mockRepository = new Mock<IDataRepository<RepriseMoto>>();
            mockRepository.Setup(x => x.GetByIdAsync(100))
                .ReturnsAsync(repriseToUpdate); // Simule la récupération de l'équipement existant
            mockRepository.Setup(x => x.UpdateAsync(repriseToUpdate, repriseToUpdate)).Returns(Task.CompletedTask);

            var controller = new RepriseMotosController(mockRepository.Object);

            // Act
            var actionResult = controller.PutRepriseMoto(fakeId, repriseToUpdate).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult)); // On s'attend à ce qu'aucun contenu ne soit retourné pour une mise à jour réussie
            mockRepository.Verify(); // Vérifie que toutes les configurations vérifiables sur le mock ont bien été appelées
        }


        /// <summary>
        /// Test PostUtilisateurTest 
        /// </summary>
        [TestMethod()]
        public void PostRepriseMotoTestAvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<RepriseMoto>>();
            var controller = new RepriseMotosController(mockRepository.Object);
            var fakeId = 100;
            // Arrange
            RepriseMoto reprise = new RepriseMoto
            {
                IdEstimationMoto = 100,
                IdDateLivraison = 1,
                IdCompteClient = 1,
                MarqueEstimationMoto = "test",
                ModeleEstimationMoto = "test",
                MoisImmatriculation = 10,
                AnneImmatriculation = 2010,
                PrixEstimationMoto = 100,
                KilometrageEstimationMoto = 1000,
                VersionEstimationMoto = "test"
            };

            // Act
            var actionResult = controller.PostRepriseMoto(reprise).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<RepriseMoto>), "Pas un ActionResult<Commande>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var okResult = actionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(okResult);
            var resultValue = okResult.Value as RepriseMoto;
            Assert.IsNotNull(resultValue);
        }

        /// <summary>
        /// Test PostUtilisateurTest 
        /// </summary>
        [TestMethod]
        public void PostRepriseMotoTest()
        {
            //// Arrange

            RepriseMoto reprise = new RepriseMoto
            {
                IdDateLivraison = 1,
                IdCompteClient = 1,
                MarqueEstimationMoto = "test",
                ModeleEstimationMoto = "test",
                MoisImmatriculation = 10,
                AnneImmatriculation = 2010,
                PrixEstimationMoto = 100,
                KilometrageEstimationMoto = 1000,
                VersionEstimationMoto = "test"
            };
            // Act
            var actionResult = controller.PostRepriseMoto(reprise).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un OkObjectResult");
            var okResult = actionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(RepriseMoto), "Pas un CompteClient");
            var repriseResult = okResult.Value as RepriseMoto;
            Assert.IsNotNull(repriseResult);

            controller.DeleteRepriseMoto(1000);
            context.SaveChangesAsync();

        }

        /// <summary>
        /// Test DeleteUtilisateurTest 
        /// </summary>
        [TestMethod()]
        public void DeleteRepriseMotoTest()
        {
            // Arrange

            RepriseMoto reprise = new RepriseMoto
            {
                IdDateLivraison=1,
                IdCompteClient = 1,
                MarqueEstimationMoto = "test",
                ModeleEstimationMoto = "test",
                MoisImmatriculation = 10,
                AnneImmatriculation = 2010,
                PrixEstimationMoto = 100,
                KilometrageEstimationMoto = 1000,
                VersionEstimationMoto = "test"
            };

            context.RepriseMotos.Add(reprise);
            context.SaveChanges();

            // Act
            RepriseMoto deletedReprise = context.RepriseMotos.FirstOrDefault(u => u.IdEstimationMoto == reprise.IdEstimationMoto);
            _ = controller.DeleteRepriseMoto(deletedReprise.IdEstimationMoto).Result;

            // Arrange
            RepriseMoto res = context.RepriseMotos.FirstOrDefault(u => u.IdEstimationMoto == deletedReprise.IdEstimationMoto);
            Assert.IsNull(res, "equipement non supprimé");
        }

        [TestMethod()]
        public void DeleteRepriseMotoTest_MOq()
        {
            var mockRepository = new Mock<IDataRepository<RepriseMoto>>();
            var controller = new RepriseMotosController(mockRepository.Object);
            var fakeId = 100;
            // Arrange
            RepriseMoto reprise = new RepriseMoto
            {
                IdEstimationMoto =100,
                IdDateLivraison=1,
                IdCompteClient=1,
                MarqueEstimationMoto="test",
                ModeleEstimationMoto="test",
                MoisImmatriculation=10,
                AnneImmatriculation=2010,
                PrixEstimationMoto=100,
                KilometrageEstimationMoto=1000,
                VersionEstimationMoto="test"
            };

            // Act
            mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(reprise);
            var actionResult = controller.DeleteRepriseMoto(reprise.IdEstimationMoto).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}