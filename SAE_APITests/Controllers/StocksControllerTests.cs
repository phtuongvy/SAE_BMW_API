using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SAE_API.Controllers;
using SAE_API.Models.DataManager;
using SAE_API.Models.EntityFramework;
using SAE_API.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_API.Controllers.Tests
{
    [TestClass()]
    public class StocksControllerTests
    {
        private StocksController controller;
        private BMWDBContext context;
        private IDataRepository<Stock> dataRepository;


        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
            context = new BMWDBContext(builder.Options);
            dataRepository = new StockManager(context);
            controller = new StocksController(dataRepository);
        }


        /// <summary>
        /// Test Contrôleur 
        /// </summary>

        [TestMethod()]
        public void TypeEquipementsControllersTest()
        {

        }

        /// <summary>
        /// Test GetUtilisateursTest 
        /// </summary>
        [TestMethod()]
        public void GetStocksTest()
        {
            // Arrange
            List<Stock> stock = context.Stocks.ToList();
            // Act
            var res = controller.GetStocks().Result;
            // Assert
            CollectionAssert.AreEqual(stock, res.Value.ToList(), "Les listes ne sont pas identiques");
        }

        /// <summary>
        /// Test GetUtilisateurByIdTest 
        /// </summary>
        [TestMethod()]
        public void GetStockByIdTest()
        {
            // Arrange
            Stock stock = context.Stocks.Find(1);
            // Act
            var res = controller.GetStockById(1).Result;
            // Assert
            Assert.AreEqual(stock, res.Value);
        }

        [TestMethod()]
        public void GetStockByIdTest_AvecMoq()
        {
            // Arrange
            var fakeId = 100;
            var mockRepository = new Mock<IDataRepository<Stock>>();
            var controller = new StocksController(mockRepository.Object);

            Stock stock = new Stock
            {
                IdStock = 100
            };
            // Act

            mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(stock);
            var actionResult = controller.GetStockById(100).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(stock, actionResult.Value as Stock);
        }

        /// <summary>
        /// Test PutUtilisateurTest 
        /// </summary>
        [TestMethod()]
        public void PutStockTest()
        {
            // Arrange
            Stock stock = context.Stocks.Find(1);

            // Act
            var res = controller.PutStock(1, stock);

            // Arrange
            Stock stock_nouveau = context.Stocks.Find(1);
            Assert.AreEqual(stock, stock_nouveau);
        }

        [TestMethod]
        public void PutStockTest_AvecMoq()
        {
            // Arrange
            var fakeId = 100;
            var stockToUpdate = new Stock
            {
                IdStock = 100
            };

            var mockRepository = new Mock<IDataRepository<Stock>>();
            mockRepository.Setup(x => x.GetByIdAsync(fakeId))
                .ReturnsAsync(stockToUpdate); // Simule la récupération de l'équipement existant
            mockRepository.Setup(x => x.UpdateAsync(stockToUpdate, stockToUpdate)).Returns(Task.CompletedTask);

            var controller = new StocksController(mockRepository.Object);

            // Act
            var actionResult = controller.PutStock(fakeId, stockToUpdate).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult)); // On s'attend à ce qu'aucun contenu ne soit retourné pour une mise à jour réussie
            mockRepository.Verify(); // Vérifie que toutes les configurations vérifiables sur le mock ont bien été appelées
        }


        /// <summary>
        /// Test PostUtilisateurTest 
        /// </summary>
        [TestMethod()]
        public void PostStockTestAvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Stock>>();
            var controller = new StocksController(mockRepository.Object);
            var fakeId = 100;
            // Arrange
            Stock stock = new Stock
            {
                IdStock = 100
            };

            // Act
            var actionResult = controller.PostStock(stock).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Stock>), "Pas un ActionResult<Commande>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var okResult = actionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(okResult);
            var resultValue = okResult.Value as Stock;
            Assert.IsNotNull(resultValue);
        }

        /// <summary>
        /// Test PostUtilisateurTest 
        /// </summary>
        [TestMethod]
        public void PostStockTest()
        {
            //// Arrange

            Stock stock = new Stock
            {
                IdStock = 10000
            };
            // Act
            var actionResult = controller.PostStock(stock).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un OkObjectResult");
            var okResult = actionResult.Result as CreatedAtActionResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(Stock), "Pas un CompteClient");
            var stockResult = okResult.Value as Stock;
            Assert.IsNotNull(stockResult);

            context.Stocks.Remove(stock);
            context.SaveChangesAsync();

        }

        /// <summary>
        /// Test DeleteUtilisateurTest 
        /// </summary>
        [TestMethod()]
        public void DeleteStockTest()
        {
            // Arrange

            Stock stock = new Stock
            {
                IdStock = 1000
            };


            context.Stocks.Add(stock);
            context.SaveChanges();

            // Act
            Stock deletedStock = context.Stocks.FirstOrDefault(u => u.IdStock == stock.IdStock);
            _ = controller.DeleteStock(deletedStock.IdStock).Result;

            // Arrange
            Stock res = context.Stocks.FirstOrDefault(u => u.IdStock == deletedStock.IdStock);
            Assert.IsNull(res, "equipement non supprimé");
        }

        [TestMethod()]
        public void DeleteStockTest_MOq()
        {
            var mockRepository = new Mock<IDataRepository<Stock>>();
            var controller = new StocksController(mockRepository.Object);
            var fakeId = 100;
            // Arrange
            Stock stock = new Stock
            {
                IdStock = 100
            };

            // Act
            mockRepository.Setup(x => x.GetByIdAsync(100).Result).Returns(stock);
            var actionResult = controller.DeleteStock(stock.IdStock).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}