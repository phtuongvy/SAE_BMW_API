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
    public class CategorieCaracteristiqueMotoControllerTests
    {
        private CategorieCaracteristiqueMotoController controller;
        private BMWDBContext context;
        private IDataRepository<CategorieCaracteristiqueMoto> dataRepository;


        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new CategorieCaracteristiqueMotoManager(context);
            controller = new CategorieCaracteristiqueMotoController(dataRepository);
        }

        /// <summary>   
        /// Test Contrôleur 
        /// </summary>
        [TestMethod()]
        public void CategorieCaracteristiqueMotoControllerTest()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new CategorieCaracteristiqueMotoManager(context);

            // Act
            var option = new CategorieCaracteristiqueMotoController(dataRepository);

            // Assert
            Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

        }


        /// <summary>
        /// Test GetCarteBancaireTest 
        /// </summary>
        [TestMethod()]
        public void GetCategorieCaracteristiqueMotoTest()
        {
            // Arrange
            List<CategorieCaracteristiqueMoto> expected = context.CategorieCaracteristiqueMotos.ToList();
            // Act
            var res = controller.GetCategorieCaracteristiqueMotos().Result;
            // Assert
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }

        /// <summary>
        /// Test GetCarteBancaireById 
        /// </summary>

        [TestMethod()]
        public void GetCategorieCaracteristiqueMotoByIdTest()
        {
            // Arrange
            CategorieCaracteristiqueMoto expected = context.CategorieCaracteristiqueMotos.Find(1);
            // Act
            var res = controller.GetCategorieCaracteristiqueMotoById(expected.IdCategorieCaracteristiqueMoto).Result;
            // Assert
            Assert.AreEqual(expected, res.Value);
        }

        [TestMethod]
        public void GetCategorieCaracteristiqueMotoByIdTest_AvecMoq()
        {
            // Arrange

            var mockRepository = new Mock<IDataRepository<CategorieCaracteristiqueMoto>>();




            CategorieCaracteristiqueMoto option = new CategorieCaracteristiqueMoto
            {
                IdCategorieCaracteristiqueMoto = 102,
                
            };
            // Act
            mockRepository.Setup(x => x.GetByIdAsync(option.IdCategorieCaracteristiqueMoto).Result).Returns(option);
            var userController = new CategorieCaracteristiqueMotoController(mockRepository.Object);

            var actionResult = userController.GetCategorieCaracteristiqueMotoById(option.IdCategorieCaracteristiqueMoto).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(option, actionResult.Value as CategorieCaracteristiqueMoto);
        }

        /// <summary>
        /// Teste la méthode PutCategorieCaracteristiqueMoto pour vérifier que la mise à jour d'un élément fonctionne correctement.
        /// </summary>

        [TestMethod]
        public async Task PutCategorieCaracteristiqueMotos_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            // Arrange
            var aPourTailles = new CategorieCaracteristiqueMoto { IdCategorieCaracteristiqueMoto = 1 };

            // Act
            var result = await controller.PutCategorieCaracteristiqueMoto(3,  aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        /// <summary>
        /// Teste la méthode PutCategorieCaracteristiqueMoto en utilisant un mock pour simuler le référentiel de données.
        /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
        /// </summary>

        [TestMethod]
        public async Task PutCategorieCaracteristiqueMotos_ReturnsBadRequestResult_WhenCategorieCaracteristiqueMotoDoesNotExistAsync()
        {

            // Arrange : préparation des données attendues
            var mockRepository = new Mock<IDataRepository<CategorieCaracteristiqueMoto>>();
            var _controller = new CategorieCaracteristiqueMotoController(mockRepository.Object);

            var aPourTailles = new CategorieCaracteristiqueMoto { IdCategorieCaracteristiqueMoto = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1, 2)).ReturnsAsync((CategorieCaracteristiqueMoto)null);

            // Act
            var result = await _controller.PutCategorieCaracteristiqueMoto(1,  aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PutCategorieCaracteristiqueMotos_ReturnsNotFoundResult_WhenUpdateIsSuccessful()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<CategorieCaracteristiqueMoto>>();
            var _controller = new CategorieCaracteristiqueMotoController(mockRepository.Object);

            var aPourTailles = new CategorieCaracteristiqueMoto { IdCategorieCaracteristiqueMoto = 1 };
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(aPourTailles);
            mockRepository.Setup(x => x.UpdateAsync(It.IsAny<CategorieCaracteristiqueMoto>(), It.IsAny<CategorieCaracteristiqueMoto>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutCategorieCaracteristiqueMoto(1 ,aPourTailles);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        /// <summary>
        /// Test PostUtilisateur 
        /// </summary>
        /// 
        [TestMethod()]
        public async Task PostCategorieCaracteristiqueMotoTestAsync()
        {
            // Arrange

            CategorieCaracteristiqueMoto option = new CategorieCaracteristiqueMoto
            {
                IdCategorieCaracteristiqueMoto = 103,
                NomCategorieCaracteristiqueMoto = "test"
            };

            // Act
            var result = controller.PostCategorieCaracteristiqueMoto(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

            // Assert
            // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            CategorieCaracteristiqueMoto? optionRecupere = context.CategorieCaracteristiqueMotos
                .Where(u => u.IdCategorieCaracteristiqueMoto == option.IdCategorieCaracteristiqueMoto)
                .FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            option.IdCategorieCaracteristiqueMoto = optionRecupere.IdCategorieCaracteristiqueMoto;
            Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

            context.CategorieCaracteristiqueMotos.Remove(option);
            await context.SaveChangesAsync();
        }

        [TestMethod]
        public void PostCategorieCaracteristiqueMotoTest_Mok()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<CategorieCaracteristiqueMoto>>();
            var userController = new CategorieCaracteristiqueMotoController(mockRepository.Object);



            // Arrange
            CategorieCaracteristiqueMoto option = new CategorieCaracteristiqueMoto
            {
                IdCategorieCaracteristiqueMoto = 102,
                NomCategorieCaracteristiqueMoto = "test"

            };

            // Act
            var actionResult = userController.PostCategorieCaracteristiqueMoto(option).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<CategorieCaracteristiqueMoto>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(CategorieCaracteristiqueMoto), "Pas un Utilisateur");

            option.IdCategorieCaracteristiqueMoto = ((CategorieCaracteristiqueMoto)result.Value).IdCategorieCaracteristiqueMoto;

            Assert.AreEqual(option, (CategorieCaracteristiqueMoto)result.Value, "Utilisateurs pas identiques");
        }



        /// <summary>
        /// Test Delete 
        /// </summary>

        [TestMethod()]
        public void DeleteCategorieCaracteristiqueMotoTest()
        {
            // Arrange
            CategorieCaracteristiqueMoto option = new CategorieCaracteristiqueMoto
            {
                IdCategorieCaracteristiqueMoto = 234,
                NomCategorieCaracteristiqueMoto = "test"

            };
            context.CategorieCaracteristiqueMotos.Add(option);
            context.SaveChanges();

            // Act
            CategorieCaracteristiqueMoto option1 = context.CategorieCaracteristiqueMotos.FirstOrDefault(u => u.IdCategorieCaracteristiqueMoto == option.IdCategorieCaracteristiqueMoto);
            _ = controller.DeleteCategorieCaracteristiqueMoto(option.IdCategorieCaracteristiqueMoto).Result;
            // Arrange
            CategorieCaracteristiqueMoto res = context.CategorieCaracteristiqueMotos.FirstOrDefault(u => u.IdCategorieCaracteristiqueMoto == option.IdCategorieCaracteristiqueMoto);
            Assert.IsNull(res, "utilisateur non supprimé");
        }


        [TestMethod]
        public void DeleteCategorieCaracteristiqueMotoTest_AvecMoq()
        {

            // Arrange
            CategorieCaracteristiqueMoto option = new CategorieCaracteristiqueMoto
            {
                IdCategorieCaracteristiqueMoto = 201,
                NomCategorieCaracteristiqueMoto = "test"

            };
            var mockRepository = new Mock<IDataRepository<CategorieCaracteristiqueMoto>>();
            mockRepository.Setup(x => x.GetByIdAsync(option.IdCategorieCaracteristiqueMoto).Result).Returns(option);
            var userController = new CategorieCaracteristiqueMotoController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteCategorieCaracteristiqueMoto(option.IdCategorieCaracteristiqueMoto).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}