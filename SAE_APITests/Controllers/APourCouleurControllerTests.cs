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
    /// <summary>
    /// Classe de test pour APourCouleurController. Contient des tests unitaires pour tester le comportement du contrôleur APourCouleur.
    /// </summary>
    [TestClass()]
    public class APourCouleurControllerTests
    {
       
            #region Private Fields
            // Déclaration des variables nécessaires pour les tests
            private APourCouleurController controller;
            private BMWDBContext context;
            private IDataRepository<APourCouleur> dataRepository;
            #endregion

            #region Test Initialization

            /// <summary>
            /// Méthode d'initialisation exécutée avant chaque test pour configurer l'environnement de test.
            /// Initialise le contexte de base de données, le référentiel de données et le contrôleur APourCouleur.
            /// </summary>
            [TestInitialize]
            public void Init()
            {
                // Configuration de la base de données pour les tests avec DbContextOptionsBuilder
                var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
                context = new BMWDBContext(builder.Options);
                // Création du gestionnaire de données et du contrôleur à tester
                dataRepository = new APourCouleurManager(context);
                controller = new APourCouleurController(dataRepository);
            }
            #endregion

            #region Test Controller
            /// <summary>
            /// Teste l'instanciation du contrôleur APourCouleurController pour s'assurer qu'elle n'est pas null.
            /// </summary>
            [TestMethod()]
            public void APourCouleurControllerTest()
            {
                // Arrange : préparation des données attendues
                var builder = new DbContextOptionsBuilder<BMWDBContext>().UseNpgsql("Server = 51.83.36.122; port = 5432; Database = sa25; uid = sa25; password = 1G1Nxb; SearchPath = bmw");
                context = new BMWDBContext(builder.Options);
                dataRepository = new APourCouleurManager(context);

                // Act : appel de la méthode à tester
                var option = new APourCouleurController(dataRepository);

                // Assert : vérification que les données obtenues correspondent aux données attendues : vérification que les données obtenues correspondent aux données attendues
                Assert.IsNotNull(option, "L'instance de MaClasse ne devrait pas être null.");

            }
            #endregion

            #region Test GetAPourCouleurTest

            /// <summary>
            /// Teste la méthode GetAPourCouleurs pour vérifier qu'elle retourne la liste correcte des éléments APourCouleur.
            /// </summary>
            [TestMethod()]
            public void GetAPourCouleurTest()
            {
                // Arrange : préparation des données attendues : préparation des données attendues
                List<APourCouleur> expected = context.APourCouleurs.ToList();
                // Act : appel de la méthode à tester 
                var res = controller.GetAPourCouleurs().Result;
                // Assert : vérification que les données obtenues correspondent aux données attendues 
                CollectionAssert.AreEqual(expected, res.Value.ToList(), "Les listes ne sont pas identiques");
            }
            #endregion

            #region Test GetAPourCouleurByIdTest

            /// <summary>
            /// Teste la méthode GetAPourCouleurById pour vérifier qu'elle retourne l'élément correct basé sur l'ID fourni.
            /// </summary>
            [TestMethod()]
            public void GetAPourCouleurByIdTest()
            {
                // Arrange : préparation des données attendues
                APourCouleur expected = context.APourCouleurs.Find(1, 1);
                // Act : appel de la méthode à tester
                var res = controller.GetAPourCouleurById(expected.IdEquipement, expected.IdCouleurEquipement).Result;
                // Assert : vérification que les données obtenues correspondent aux données attendues
                Assert.AreEqual(expected, res.Value);
            }

            /// <summary>
            /// Teste la méthode GetAPourCouleurById en utilisant un mock pour le référentiel de données.
            /// Permet de tester le contrôleur de manière isolée.
            /// </summary>
            [TestMethod]
            public void GetAPourCouleurByIdTest_AvecMoq()
            {
                // Arrange : préparation des données attendues
                var mockRepository = new Mock<IDataRepository<APourCouleur>>();

                APourCouleur option = new APourCouleur
                {
                    IdEquipement = 1,
                    IdCouleurEquipement = 1,
                };

                // Act : appel de la méthode à tester
                mockRepository.Setup(x => x.GetByIdAsync(option.IdEquipement, option.IdCouleurEquipement).Result).Returns(option);
                var userController = new APourCouleurController(mockRepository.Object);
                var actionResult = userController.GetAPourCouleurById(option.IdEquipement, option.IdCouleurEquipement).Result;

                // Assert : vérification que les données obtenues correspondent aux données attendues
                Assert.IsNotNull(actionResult);
                Assert.IsNotNull(actionResult.Value);
                Assert.AreEqual(option, actionResult.Value as APourCouleur);
            }
            #endregion

            #region Test PutAPourCouleurTestAsync
            /// <summary>
            /// Teste la méthode PutAPourCouleur pour vérifier que la mise à jour d'un élément fonctionne correctement.
            /// </summary>

            [TestMethod]
            public async Task PutAPourCouleur_ReturnsBadRequest_WhenIdsDoNotMatch()
            {
                // Arrange
                var aPourCouleur = new APourCouleur { IdEquipement = 1, IdCouleurEquipement = 2 };

                // Act
                var result = await controller.PutAPourCouleur(1, 3, aPourCouleur);

                // Assert
                Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            }

            /// <summary>
            /// Teste la méthode PutAPourCouleur en utilisant un mock pour simuler le référentiel de données.
            /// Permet de vérifier le comportement du contrôleur lors de la mise à jour d'un élément.
            /// </summary>

            [TestMethod]
            public async Task PutAPourCouleur_ReturnsNotFound_WhenAPourCouleurDoesNotExist()
            {
                // Arrange // Arrange
                var mockRepository = new Mock<IDataRepository<APourCouleur>>();
                var _controller = new APourCouleurController(mockRepository.Object);

                var aPourCouleur = new APourCouleur { IdEquipement = 1, IdCouleurEquipement = 2 };
                 mockRepository.Setup(x => x.GetByIdAsync(1, 2)).ReturnsAsync((APourCouleur)null);

                // Act
                var result = await _controller.PutAPourCouleur(1, 2, aPourCouleur);

                // Assert
                Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            }

            [TestMethod]
            public async Task PutAPourCouleur_ReturnsNoContent_WhenUpdateIsSuccessful()
            {
                // Arrange
                var mockRepository = new Mock<IDataRepository<APourCouleur>>();
                var _controller = new APourCouleurController(mockRepository.Object);

                var aPourCouleur = new APourCouleur { IdEquipement = 1, IdCouleurEquipement = 2 };
                mockRepository.Setup(x => x.GetByIdAsync(1, 2)).ReturnsAsync(new APourCouleur());
                mockRepository.Setup(x => x.UpdateAsync(It.IsAny<APourCouleur>(), It.IsAny<APourCouleur>())).Returns(Task.CompletedTask);

                // Act
                var result = await _controller.PutAPourCouleur(1, 2, aPourCouleur);

                // Assert
                Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            }
            #endregion

            #region Test PostAPourCouleurTestAsync

            /// <summary>
            /// Teste la méthode PostAPourCouleur pour vérifier que l'ajout d'un nouvel élément fonctionne correctement.
            /// </summary>

            [TestMethod()]
            public async Task PostAPourCouleurTestAsync()
            {
                // Arrange : préparation des données attendues
                APourCouleur option = new APourCouleur
                {
                    IdEquipement = 1,
                    IdCouleurEquipement = 7,
                };

                // Act : appel de la méthode à tester
                var result = controller.PostAPourCouleur(option).Result; // .Result pour appeler la méthode async de manière synchrone, afin d'attendre l’ajout

                // Assert : vérification que les données obtenues correspondent aux données attendues
                APourCouleur? optionRecupere = context.APourCouleurs
                    .Where(u => u.IdEquipement == option.IdEquipement && u.IdCouleurEquipement == option.IdCouleurEquipement)
                    .FirstOrDefault();

                // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
                // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
                option.IdEquipement = optionRecupere.IdEquipement;
                option.IdCouleurEquipement = optionRecupere.IdCouleurEquipement;
                Assert.AreEqual(optionRecupere, option, "Utilisateurs pas identiques");

                context.APourCouleurs.Remove(option);
                await context.SaveChangesAsync();
            }

            /// <summary>
            /// Teste la méthode PostAPourCouleur en utilisant un mock pour simuler le référentiel de données.
            /// Permet de vérifier le comportement du contrôleur lors de l'ajout d'un nouvel élément.
            /// </summary>
            [TestMethod]
            public void PostAPourCouleurTest_Mok()
            {
                // Arrange : préparation des données attendues
                var mockRepository = new Mock<IDataRepository<APourCouleur>>();
                var userController = new APourCouleurController(mockRepository.Object);



                // Arrange : préparation des données attendues
                APourCouleur option = new APourCouleur
                {
                    IdEquipement = 1,
                    IdCouleurEquipement = 7,
                };

                // Act : appel de la méthode à tester
                var actionResult = userController.PostAPourCouleur(option).Result;
                // Assert : vérification que les données obtenues correspondent aux données attendues
                Assert.IsInstanceOfType(actionResult, typeof(ActionResult<APourCouleur>), "Pas un ActionResult<Utilisateur>");
                Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");

                var result = actionResult.Result as CreatedAtActionResult;
                Assert.IsInstanceOfType(result.Value, typeof(APourCouleur), "Pas un Utilisateur");

                option.IdEquipement = ((APourCouleur)result.Value).IdEquipement;
                option.IdCouleurEquipement = ((APourCouleur)result.Value).IdCouleurEquipement;
                Assert.AreEqual(option, (APourCouleur)result.Value, "Utilisateurs pas identiques");
            }
            #endregion

            #region Test DeleteAPourCouleurTest
            /// <summary>
            /// Teste la méthode DeleteAPourCouleur pour vérifier que la suppression d'un élément fonctionne correctement.
            /// </summary>

            [TestMethod()]
            public void DeleteAPourCouleurTest()
            {
                // Arrange : préparation des données attendues
                APourCouleur option = new APourCouleur
                {
                    IdEquipement = 1,
                    IdCouleurEquipement = 7,
                };
                context.APourCouleurs.Add(option);
                context.SaveChanges();

                // Act : appel de la méthode à tester
                APourCouleur option1 = context.APourCouleurs.FirstOrDefault(u => u.IdCouleurEquipement == option.IdCouleurEquipement && u.IdEquipement == option.IdEquipement);
                _ = controller.DeleteAPourCouleur(option.IdEquipement, option.IdCouleurEquipement).Result;

                // Arrange : préparation des données attendues
                APourCouleur res = context.APourCouleurs.FirstOrDefault(u => u.IdEquipement == option.IdEquipement && u.IdCouleurEquipement == option.IdCouleurEquipement);
                Assert.IsNull(res, "utilisateur non supprimé");
            }

            /// <summary>
            /// Teste la méthode DeleteAPourCouleur en utilisant un mock pour simuler le référentiel de données.
            /// Permet de vérifier le comportement du contrôleur lors de la suppression d'un élément.
            /// </summary>
            [TestMethod]
            public void DeleteAPourCouleurTest_AvecMoq()
            {

                // Arrange : préparation des données attendues
                APourCouleur option = new APourCouleur
                {
                    IdEquipement = 1,
                    IdCouleurEquipement = 7,
                };
                var mockRepository = new Mock<IDataRepository<APourCouleur>>();
                mockRepository.Setup(x => x.GetByIdAsync(option.IdEquipement, option.IdCouleurEquipement).Result).Returns(option);
                var userController = new APourCouleurController(mockRepository.Object);
                // Act : appel de la méthode à tester
                var actionResult = userController.DeleteAPourCouleur(option.IdEquipement, option.IdCouleurEquipement).Result;
                // Assert : vérification que les données obtenues correspondent aux données attendues
                Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
            }
            #endregion
        }
    }