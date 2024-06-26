﻿using Microsoft.AspNetCore.Mvc;
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
        public class TransationsControllerTest
        {
            private TransationsController controller;
            private BMWDBContext context;
            private IDataRepository<Transation> dataRepository;


            [TestInitialize]
            public void Init()
            {
                var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
                context = new BMWDBContext(builder.Options);
                dataRepository = new TransationManager(context);
                controller = new TransationsController(dataRepository);
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
                dataRepository = new TransationManager(context);

                // Act
                var option = new TransationsController(dataRepository);

                // Assert
                Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");
            }

            /// <summary>
            /// Test GetUtilisateursTest 
            /// </summary>
            [TestMethod()]
            public void GetTransationsTest()
            {
                // Arrange
                List<Transation> type = context.Transations.ToList();
                // Act
                var res = controller.GetTransations().Result;
                // Assert
                CollectionAssert.AreEqual(type, res.Value.ToList(), "Les listes ne sont pas identiques");
            }

            /// <summary>
            /// Test GetUtilisateurByIdTest 
            /// </summary>
            [TestMethod()]
            public void GetTransationByIdTest()
            {
                // Arrange
                Transation type = context.Transations.Find(1);
                // Act
                var res = controller.GetTransationById(1).Result;
                // Assert
                Assert.AreEqual(type, res.Value);
            }

            [TestMethod()]
            public void GetTransationByIdTest_AvecMoq()
            {
                // Arrange
                var fakeId = 100;
                var mockRepository = new Mock<IDataRepository<Transation>>();
                var userController = new TransationsController(mockRepository.Object);

                Transation type = new Transation
                {
                    IdTransaction = 100,
                    IdCompteClient = 1,
                    Montant = 100,
                    TypeDePayment = "carte",
                    TypeDeTransaction = "achat"
                };
                // Act

                mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(type);
                var actionResult = userController.GetTransationById(100).Result;
                // Assert
                Assert.IsNotNull(actionResult);
                Assert.IsNotNull(actionResult.Value);
                Assert.AreEqual(type, actionResult.Value as Transation);
            }

            /// <summary>
            /// Test PutUtilisateurTest 
            /// </summary>
            [TestMethod()]
            public void PutTransationTest()
            {
                // Arrange
                Transation type = context.Transations.Find(1);

                // Act
                var res = controller.PutTransation(1, type);

                // Arrange
                Transation type_nouveau = context.Transations.Find(1);
                Assert.AreEqual(type, type_nouveau);
            }

            [TestMethod]
            public void PutTransationTest_AvecMoq()
            {
                // Arrange
                var fakeId = 100;
                var typeToUpdate = new Transation
                {
                    IdTransaction = 100,
                    IdCompteClient = 1,
                    Montant = 100,
                    TypeDePayment = "carte",
                    TypeDeTransaction = "achat"
                };

                var mockRepository = new Mock<IDataRepository<Transation>>();
                mockRepository.Setup(x => x.GetByIdAsync(fakeId))
                    .ReturnsAsync(typeToUpdate); // Simule la récupération de l'équipement existant
                mockRepository.Setup(x => x.UpdateAsync(typeToUpdate, typeToUpdate)).Returns(Task.CompletedTask);

                var controller = new TransationsController(mockRepository.Object);

                // Act
                var actionResult = controller.PutTransation(fakeId, typeToUpdate).Result;

                // Assert
                Assert.IsInstanceOfType(actionResult, typeof(NoContentResult)); // On s'attend à ce qu'aucun contenu ne soit retourné pour une mise à jour réussie
                mockRepository.Verify(); // Vérifie que toutes les configurations vérifiables sur le mock ont bien été appelées
            }


            /// <summary>
            /// Test PostUtilisateurTest 
            /// </summary>
            [TestMethod()]
            public void PostTransationTestAvecMoq()
            {
                // Arrange
                var mockRepository = new Mock<IDataRepository<Transation>>();
                var userController = new TransationsController(mockRepository.Object);
                var fakeId = 100;
                // Arrange
                Transation type = new Transation
                {
                    IdTransaction = 100,
                    IdCompteClient = 1,
                    Montant = 100,
                    TypeDePayment = "carte",
                    TypeDeTransaction = "achat"
                };

                // Act
                var actionResult = userController.PostTransation(type).Result;
                // Assert
                Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Transation>), "Pas un ActionResult<Commande>");
                Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
                var okResult = actionResult.Result as CreatedAtActionResult;
                Assert.IsNotNull(okResult);
                var resultValue = okResult.Value as Transation;
                Assert.IsNotNull(resultValue);
            }

            /// <summary>
            /// Test PostUtilisateurTest 
            /// </summary>
            [TestMethod]
            public void PostTransationTest()
            {
                //// Arrange

                Transation type = new Transation
                {
                    IdCompteClient = 1,
                    Montant = 100,
                    TypeDePayment = "carte",
                    TypeDeTransaction = "achat"
                };
                // Act
                var actionResult = controller.PostTransation(type).Result;
                // Assert
                Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un OkObjectResult");
                var okResult = actionResult.Result as CreatedAtActionResult;
                Assert.IsNotNull(okResult);
                Assert.IsInstanceOfType(okResult.Value, typeof(Transation), "Pas un CompteClient");
                var typeResult = okResult.Value as Transation;
                Assert.IsNotNull(typeResult);

                context.Transations.Remove(type);
                context.SaveChangesAsync();

            }

            /// <summary>
            /// Test DeleteUtilisateurTest 
            /// </summary>
            [TestMethod()]
            public void DeleteTransationTest()
            {
                // Arrange

                Transation type = new Transation
                {
                    IdCompteClient = 1,
                    Montant = 100,
                    TypeDePayment = "carte",
                    TypeDeTransaction = "achat"
                };

                context.Transations.Add(type);
                context.SaveChanges();

                // Act
                Transation deletedType = context.Transations.FirstOrDefault(u => u.IdTransaction == type.IdTransaction);
                _ = controller.DeleteTransation(deletedType.IdTransaction).Result;

                // Arrange
                Transation res = context.Transations.FirstOrDefault(u => u.IdTransaction == deletedType.IdTransaction);
                Assert.IsNull(res, "equipement non supprimé");
            }

            [TestMethod()]
            public void DeleteTransationTest_MOq()
            {
                var mockRepository = new Mock<IDataRepository<Transation>>();
                var userController = new TransationsController(mockRepository.Object);
                var fakeId = 100;
                // Arrange
                Transation type = new Transation
                {
                    IdTransaction=100,
                    IdCompteClient = 1,
                    Montant=100,
                    TypeDePayment = "carte",
                    TypeDeTransaction = "achat"
                };

                // Act
                mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(type);
                var actionResult = userController.DeleteTransation(type.IdTransaction).Result;
                // Assert
                Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
            }

        }
    
}