﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP4_1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TP4_1.Models.EntityFramework;
using System.Security.Cryptography;
using TP4_1_Models_EntityFramework;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using TP4_1.Models.DataManager;
using TP4_1.Models.Repository;
using Moq;

namespace TP4_1.Controllers.Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {

        private UtilisateursController controller;

        private FilmRatingDBContext context;
        private IDataRepository<Utilisateur> dataRepository;

        public UtilisateursControllerTests()
        {
            var builder = new DbContextOptionsBuilder<FilmRatingDBContext>()
                  .UseNpgsql("Server = localhost; port = 5432; Database = FilmsDB; uid = postgres; password = postgres;"); // Chaine de connexion à mettre dans les ( )
            this.Context = new FilmRatingDBContext(builder.Options);
            this.dataRepository = new UtilisateurManager(context);
            this.Controller = new UtilisateursController(this.dataRepository);

        }

        public UtilisateursController Controller
        {
            get
            {
                return controller;
            }

            set
            {
                controller = value;
            }
        }

        public FilmRatingDBContext Context
        {
            get
            {
                return context;
            }

            set
            {
                context = value;
            }
        }

        //A finir
        [TestMethod()]
        public void GetUtilisateursTest()
        {
            //Arrange
            List<Utilisateur> utilisateurAttendu = this.Context.Utilisateurs.ToList();

            //Assert
            List<Utilisateur> utilisateurObtenu = this.Controller.GetUtilisateurs().Result.Value.ToList();


            CollectionAssert.AreEqual(utilisateurAttendu, utilisateurObtenu);

        }


        [TestMethod()]
        public void UtilisateurGetByIdTest_OK()
        {
            Utilisateur utilisateurAttendu = this.Context.Utilisateurs.Find(1);

            Utilisateur utilisateurPresent = this.Controller.GetUtilisateurById(1).Result.Value;
            Assert.AreEqual(utilisateurAttendu, utilisateurPresent);
        }

        [TestMethod()]
        public void UtilisateurGetByIdTest_NonOK()
        {

            var result = this.Controller.GetUtilisateurById(0).Result;

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.IsNull(result.Value, "test");
        }


        [TestMethod()]
        public void UtilisateurGetByEmailTest_OK()
        {
            string email = "rrichings1@naver.com";
            Utilisateur utilisateurAttendu = this.Context.Utilisateurs.First(c=>c.Mail == email );

            Utilisateur utilisateurPresent = this.Controller.GetUtilisateurByEmail(email).Result.Value;
            Assert.AreEqual(utilisateurAttendu, utilisateurPresent);
        }

        [TestMethod()]
        public void UtilisateurGetByEmaimTest_NonOK()
        {

            var result = this.Controller.GetUtilisateurByEmail("rrichin1@naver.com").Result;

            Assert.IsNotNull(result.Result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
            Assert.AreEqual(((NotFoundResult)result.Result).StatusCode, 404, "test"); 
            Assert.IsNull(result.Value, "test");
        }

        [TestMethod]
        public void Postutilisateur_ModelValidated_CreationOK()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE du WS ou remove du DbSet.
            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Laititude = null,
                Longitude = null
            };
            // Act
            var result = controller.PostUtilisateur(userAtester).Result; 
            // .Result pour appeler la méthode async de manière synchrone, afin d'obtenir le résultat
            var result2 = controller.GetUtilisateurByEmail(userAtester.Mail);
            var actionResult = result2.Result as ActionResult<Utilisateur>;
            // Assert
            Assert.IsInstanceOfType(actionResult.Value, typeof(Utilisateur), "Pas un utilisateur");

            Utilisateur? userRecupere = context.Utilisateurs.Where(u => u.Mail.ToUpper() ==
            userAtester.Mail.ToUpper()).FirstOrDefault();

            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            userAtester.UtilisateurId = userRecupere.UtilisateurId;

            Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");
        }
        [TestMethod]
        public void Postutilisateur_ModelValidated_CreationOK_V2()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE de l’API ou remove du DbSet.
            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Laititude = null,
                Longitude = null
            };
            // Act
            var result = controller.PostUtilisateur(userAtester).Result; // .Result pour appeler la méthode async de  synchrone, afin d'attendre l’ajout
            // Assert
            Utilisateur? userRecupere = context.Utilisateurs.Where(u => u.Mail.ToUpper() ==
            userAtester.Mail.ToUpper()).FirstOrDefault(); // On récupère l'utilisateur créé directement dans la BD grace à son mail unique
            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            userAtester.UtilisateurId = userRecupere.UtilisateurId;
                        Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");
        }


        [TestMethod]
        public void Pututilisateur_ModelValidated_CreationOK()
        {

            int id = 10;

            //Alea tester changement
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            //On recupere queqlu'un 
            var result = controller.GetUtilisateurById(id);
            var actionResult = result.Result as ActionResult<Utilisateur>;
            // Test bien récupérée
            Assert.IsInstanceOfType(actionResult.Value, typeof(Utilisateur), "Pas un utilisateur");
            


            Utilisateur actionUser = actionResult.Value as Utilisateur;

            actionUser.Nom = $"test{chiffre}";

            var result2 = this.Controller.PutUtilisateur(actionUser.UtilisateurId, actionUser).Result;


            Assert.IsInstanceOfType(result2, typeof(NoContentResult), "Pas un non content");
            Assert.AreEqual(((NoContentResult)result2).StatusCode, 204, "Pas une 204");


            Utilisateur utilisateurTest = this.Context.Utilisateurs.Find(actionUser.UtilisateurId);

            Assert.AreEqual(utilisateurTest, actionUser, "La modification n'a pas été faite");
            
        }


        [TestMethod]
        public void Pututilisateur_ModelNotSameID()
        {

            int id = 10;

            Utilisateur userAtester = new Utilisateur()
            {
                UtilisateurId= 12,
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Laititude = null,
                Longitude = null
            };

            var result2 = this.Controller.PutUtilisateur(id, userAtester).Result;

            Assert.IsNotNull(result2, "test");
            Assert.IsInstanceOfType(result2, typeof(BadRequestResult), "test");
            Assert.AreEqual(((BadRequestResult)result2).StatusCode, 400, "test");
       
        }


        [TestMethod]
        public void Pututilisateur_ModelNotExist()
        {

            int id = 10000;

            Utilisateur userAtester = new Utilisateur()
            {
                UtilisateurId = 10000,
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Laititude = null,
                Longitude = null
            };

            var result2 = this.Controller.PutUtilisateur(id, userAtester).Result;

            Assert.IsNotNull(result2, "test");
            Assert.IsInstanceOfType(result2, typeof(NotFoundResult), "test");
            Assert.AreEqual(((NotFoundResult)result2).StatusCode, 404, "test");

        }






        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Postutilisateur_ModelNotValidated_SameMail()
        {

            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "abramford2@businesswire.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Laititude = null,
                Longitude = null
            };
            // Act
            var result = controller.PostUtilisateur(userAtester).Result;
        }


        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Postutilisateur_ModelNotValidated_NoPassword()
        {

            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "dfsdfds@businesswire.com",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Laititude = null,
                Longitude = null
            };
            // Act
            var result = controller.PostUtilisateur(userAtester).Result;
        }

        //Tout tester les manques
        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Postutilisateur_ModelNotValidated_ProblemMail()
        {
            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "f",
                Mail = "abramford2@businesswire.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Laititude = null,
                Longitude = null
            };

            // Act
            var result = controller.PostUtilisateur(userAtester).Result;
        }


        [TestMethod()]
        public void DeleteUtilisateur()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE du WS ou remove du DbSet.
            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Laititude = null,
                Longitude = null
            };

            this.Context.Utilisateurs.Add(userAtester);

            this.Context.SaveChanges();


            var result = this.Controller.GetUtilisateurByEmail(userAtester.Mail).Result;

            Assert.IsInstanceOfType(result.Value, typeof(Utilisateur));

            Utilisateur resultUser = result.Value;

            var result2 = this.Controller.DeleteUtilisateur(resultUser.UtilisateurId).Result;

           Utilisateur? utiliseur =  this.Context.Utilisateurs.Find(resultUser.UtilisateurId);

            Assert.IsNull(utiliseur, "l'utilisateur n'a pas été supprimé");


        }


        [TestMethod()]
        public void DeleteUtilisateur_NotOK()
        {
            int id = 10000;

            var result2 = this.Controller.DeleteUtilisateur(id).Result;

            Assert.IsNotNull(result2, "test");
            Assert.AreEqual(404, ((NotFoundResult)result2).StatusCode, "test");
            Assert.IsInstanceOfType(result2, typeof(NotFoundResult));


        }




        [TestMethod]
        public void Postutilisateur_ModelValidated_CreationOK_AvecMoq() // Fonctionne même si pas base ne fait pas d'insert
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Utilisateur>>(); // Instance un simulacre
            var userController = new UtilisateursController(mockRepository.Object); // On lui passe me simulacre
            Utilisateur user = new Utilisateur
            {
                Nom = "POISSON",
                Prenom = "Pascal",
                Mobile = "1",
                Mail = "poisson@gmail.com",
                Pwd = "Toto12345678!",
                Rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Laititude = null,
                Longitude = null
            };
            // Act
            var actionResult = userController.PostUtilisateur(user).Result; // on utilise le truc du simulacre
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Utilisateur>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Utilisateur), "Pas un Utilisateur");
            user.UtilisateurId = ((Utilisateur)result.Value).UtilisateurId;
            Assert.AreEqual(user, (Utilisateur)result.Value, "Utilisateurs pas identiques");
        }

        [TestMethod]
        public void GetUtilisateurById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
        // Arrange
        Utilisateur user = new Utilisateur
        {
            UtilisateurId = 1,
            Nom = "Calida",
            Prenom = "Lilley",
            Mobile = "0653930778",
            Mail = "clilleymd@last.fm",
            Pwd = "Toto12345678!",
            Rue = "Impasse des bergeronnettes",
            CodePostal = "74200",
            Ville = "Allinges",
            Pays = "France",
            Laititude = 46.344795F,
            Longitude = 6.4885845F
        };
        var mockRepository = new Mock<IDataRepository<Utilisateur>>();
        mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(user);
        var userController = new UtilisateursController(mockRepository.Object);
        // Act
        var actionResult = userController.GetUtilisateurById(1).Result;
        // Assert
        Assert.IsNotNull(actionResult);
        Assert.IsNotNull(actionResult.Value);
        Assert.AreEqual(user, actionResult.Value as Utilisateur);
        }

        [TestMethod]
        public void GetUtilisateurById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.GetUtilisateurById(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }




        [TestMethod]
        public void GetUtilisateurByEmail_ExistingEmailPassed_ReturnsRightIttem_AvecMoq()
        {
            // Arrange
            Utilisateur user = new Utilisateur
            {
                UtilisateurId = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                Rue = "Impasse des bergeronnettes",
                CodePostal = "74200",
                Ville = "Allinges",
                Pays = "France",
                Laititude = 46.344795F,
                Longitude = 6.4885845F
            };
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByStringAsync("clilleymd@last.fm").Result).Returns(user);

            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.GetUtilisateurByEmail("clilleymd@last.fm").Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(user, actionResult.Value as Utilisateur);
        }



        [TestMethod]
        public void GetUtilisateurByEmail_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.GetUtilisateurByEmail("clilleymd@last.fm").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }


        /*
        [TestMethod]
        public void DeleteUtilisateurTest_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteUtilisateur(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }*/



















    }
}