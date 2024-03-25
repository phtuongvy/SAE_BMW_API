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
    public class CarteBancaireControllerTests
    {
        private CarteBancaireController controller;
        private BMWDBContext context;
        private IDataRepository<CarteBancaire> dataRepository;


        [TestInitialize]
        public void Init()
        {
            var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server=localhost;port=5432;Database=RatingFilmsDB; uid=postgres;password=postgres;");
            context = new BMWDBContext(builder.Options);
            dataRepository = new CarteBancaireManager(context);
            controller = new CarteBancaireController(dataRepository);
        }
        /// <summary>
        /// Test Contrôleur 
        /// </summary>
        [TestMethod()]
        public void CarteBancaireControllerTest()
        {

        }


        /// <summary>
        /// Test GetCarteBancaireTest 
        /// </summary>
        [TestMethod()]
        public void GetCarteBancaireTest()
        {
            // Arrange
            List<CarteBancaire> expected = context.CartesBancaires.ToList();
            // Act
            var res = controller.GetCarteBancaires().Result;
            // Assert
            CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
        }

        /// <summary>
        /// Test GetCarteBancaireById 
        /// </summary>

        [TestMethod()]
        public void GetCarteBancaireByIdTest()
        {
            // Arrange
            CarteBancaire expected = context.CartesBancaires.Find(1);
            // Act
            var res = controller.GetCarteBancaireById(1).Result;
            // Assert
            Assert.AreEqual(expected, res.Value);
        }

        [TestMethod]
        public void GetCarteBancaireByIdTest_AvecMoq()
        {
            // Arrange

            var mockRepository = new Mock<IDataRepository<CarteBancaire>>();

            ICollection<Acquerir> AcquisC = new List<Acquerir>
            {
                new Acquerir { /* initialisez les propriétés de l'objet ici */ },
                new Acquerir { /* un autre objet Acquerir */ }
            };


            CarteBancaire catre = new CarteBancaire
            {
                IdCb = 1,
                NomCarte = "NUNES EMILIO Ricardo ",
                NumeroCb = "12345678901234",
                MoisExpiration = 02,
                AnneeExpiration = 25,
                CryptoCb = "093",
                AcquisCB = AcquisC
            };
            // Act
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(catre);
            var userController = new CarteBancaireController(mockRepository.Object);

            var actionResult = userController.GetCarteBancaireById(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(catre, actionResult.Value as CarteBancaire);
        }

        /// <summary>
        /// Test PutCarteBancaireTest 
        /// </summary>
        [TestMethod()]
        public void PutCarteBancaireTest()
        {
            // Arrange
            CarteBancaire userAtester = context.CartesBancaires.Find(1);

            // Act
            var res = controller.PutCarteBancaire(1, userAtester);

            // Arrange
            CarteBancaire nouvellecarte = context.CartesBancaires.Find(1);
            Assert.AreEqual(userAtester, nouvellecarte);
        }

        [TestMethod]
        public void PutCarteBancaireTestAvecMoq()
        {
            ICollection<Acquerir> AcquisC = new List<Acquerir>
            {
                new Acquerir { /* initialisez les propriétés de l'objet ici */ },
                new Acquerir { /* un autre objet Acquerir */ }
            };

            // Arrange
            CarteBancaire catre = new CarteBancaire
            {
                IdCb = 1,
                NomCarte = "NUNES EMILIO Ricardo ",
                NumeroCb = "12345678901234",
                MoisExpiration = 02,
                AnneeExpiration = 25,
                CryptoCb = "093",
                AcquisCB = AcquisC
            };
            CarteBancaire catre2 = new CarteBancaire
            {
                IdCb = 2,
                NomCarte = "NUNES  ",
                NumeroCb = "1234567890123fzeoizjf",
                MoisExpiration = 03,
                AnneeExpiration = 25,
                CryptoCb = "093",
                AcquisCB = AcquisC
            };
            var mockRepository = new Mock<IDataRepository<CarteBancaire>>();
            mockRepository.Setup(x => x.GetByIdAsync(2).Result).Returns(catre2);
            var userController = new CarteBancaireController(mockRepository.Object);

            // Act
            var actionResult = userController.PutCarteBancaire(2, catre).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult");
        }

        /// <summary>
        /// Test PostUtilisateur 
        /// </summary>
        [TestMethod]
        public void PostCarteBancaireTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<CarteBancaire>>();
            var userController = new CarteBancaireController(mockRepository.Object);

            ICollection<Acquerir> AcquisC = new List<Acquerir>
            {
                new Acquerir { /* initialisez les propriétés de l'objet ici */ },
                new Acquerir { /* un autre objet Acquerir */ }
            };

            // Arrange
            CarteBancaire catre = new CarteBancaire
            {
                IdCb = 1,
                NomCarte = "NUNES EMILIO Ricardo ",
                NumeroCb = "12345678901234",
                MoisExpiration = 02,
                AnneeExpiration = 25,
                CryptoCb = "093",
                AcquisCB = AcquisC
            };
            // Act
            var actionResult = userController.PostCarteBancaire(catre).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<CarteBancaire>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(CarteBancaire), "Pas un Utilisateur");
            catre.IdCb = ((CarteBancaire)result.Value).IdCb;
            Assert.AreEqual(catre, (CarteBancaire)result.Value, "Utilisateurs pas identiques");
        }

        /// <summary>
        /// Test DeleteUtilisateu 
        /// </summary>

        [TestMethod()]
        public void DeleteUtilisateurTest()
        {
            // 
            ICollection<Acquerir> AcquisC = new List<Acquerir>
            {
                new Acquerir { /* initialisez les propriétés de l'objet ici */ },
                new Acquerir { /* un autre objet Acquerir */ }
            };

            // Arrange
            CarteBancaire catre = new CarteBancaire
            {
                IdCb = 1,
                NomCarte = "NUNES EMILIO Ricardo ",
                NumeroCb = "12345678901234",
                MoisExpiration = 02,
                AnneeExpiration = 25,
                CryptoCb = "093",
                AcquisCB = AcquisC
            };
            context.CartesBancaires.Add(catre);
            context.SaveChanges();

            // Act
            CarteBancaire deletedCarte = context.CartesBancaires.FirstOrDefault(u => u.IdCb == catre.IdCb);
            _ = controller.DeleteCarteBancaire(deletedCarte.IdCb).Result;

            // Arrange
            CarteBancaire res = context.CartesBancaires.FirstOrDefault(u => u.IdCb == catre.IdCb);
            Assert.IsNull(res, "utilisateur non supprimé");
        }


        [TestMethod]
        public void DeleteUtilisateurTest_AvecMoq()
        {
            ICollection<Acquerir> AcquisC = new List<Acquerir>
            {
                new Acquerir { /* initialisez les propriétés de l'objet ici */ },
                new Acquerir { /* un autre objet Acquerir */ }
            };

            // Arrange
            CarteBancaire catre = new CarteBancaire
            {
                IdCb = 1,
                NomCarte = "NUNES EMILIO Ricardo ",
                NumeroCb = "12345678901234",
                MoisExpiration = 02,
                AnneeExpiration = 25,
                CryptoCb = "093",
                AcquisCB = AcquisC
            };
            var mockRepository = new Mock<IDataRepository<CarteBancaire>>();
            mockRepository.Setup(x => x.GetByIdAsync(2).Result).Returns(catre);
            var userController = new CarteBancaireController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteCarteBancaire(2).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }


    }
}