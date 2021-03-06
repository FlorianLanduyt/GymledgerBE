﻿using System;
using System.Threading.Tasks;
using GymLedgerAPI.Domain.Models;
using GymLedgerAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace GymLedgerAPI.Data {
    public class DataInit {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public DataInit(ApplicationDbContext dbContext, UserManager<User> userManager) {
            _dbContext = dbContext;
            _userManager = userManager;
        }



        public async Task InitializeData() {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated()) {
                #region Users
                // ----------- Creating the coaches and gymnasts ---------------

                var password = "P@ssword111";

                //Coach stefan = new Coach("Stefan", "Deckx", new DateTime(1978, 02, 10), "stefan.deckx@hotmail.com");
                //Coach koen = new Coach("Koen", "Van Damme", new DateTime(1986, 03, 10), "koen.vandamme@hotmail.com");

                //Gymnast florian = new Gymnast("Florian", "Landuyt", new DateTime(1996, 05, 10), "florian.landuyt@hotmail.com");
                //Gymnast jonathan = new Gymnast("Jonathan", "Vrolix", new DateTime(1996, 11, 09), "jonathan.vrolix@hotmail.com");

                Gymnast gymnast = new Gymnast("sport", "gymnast", new DateTime(1996, 05, 10), "sport.gymnast@hotmail.com");

                CreateUser(gymnast, password);
                //CreateUser(jonathan, password);
                //CreateUser(koen, password);
                //CreateUser(stefan, password);



                // ---------- Adding gymnasts to their coaches -----------------

                // stefan.AddGymnast(florian);
                // stefan.AddGymnast(jonathan);

                // koen.AddGymnast(florian);


                // // ---------- Adding users to the database ---------------------




                await _userManager.AddPasswordAsync(gymnast, password);
                // //await _userManager.AddPasswordAsync(jonathan, password);
                // //await _userManager.AddPasswordAsync(koen, password);
                // //await _userManager.AddPasswordAsync(stefan, password);



                // // ---------- Creating the users -------------------------------
                // //await CreateUser(stefan, password);
                // //await CreateUser(koen, password);
                // //await CreateUser(jonathan, password);
                //await CreateUser(florian, password);

                 #endregion

                // #region Categories

                // Category general = new Category("General", "");
                // Category legpower = new Category("Legpower", "");
                // Category schoulderPower = new Category("Shoulders", "");
                // Category corestability = new Category("Corestability", "");
                // Category backpower = new Category("Backpower", "");

                // _dbContext.Categories.Add(general);
                // _dbContext.Categories.Add(corestability);
                // _dbContext.Categories.Add(schoulderPower);
                // _dbContext.Categories.Add(legpower);
                // _dbContext.Categories.Add(backpower);


                // #endregion


                // #region Trainings

                // Training t1 = new Training(general, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), 5,7);
                // Training t2 = new Training(corestability, new DateTime(DateTime.Now.AddDays(3).Year, DateTime.Now.AddDays(3).Month, DateTime.Now.AddDays(3).Day),4,8);
                // Training t3 = new Training(legpower, new DateTime(DateTime.Now.AddDays(4).Year, DateTime.Now.AddDays(4).Month, DateTime.Now.AddDays(4).Day));
                // Training t4 = new Training(backpower, new DateTime(DateTime.Now.AddDays(5).Year, DateTime.Now.AddDays(5).Month, DateTime.Now.AddDays(5).Day), 9,9);

                // Training t5 = new Training(backpower, new DateTime(DateTime.Now.AddDays(5).Year, DateTime.Now.AddDays(5).Month, DateTime.Now.AddDays(5).Day), 9, 9);




                // _dbContext.Trainings.Add(t1);
                // _dbContext.Trainings.Add(t2);
                // _dbContext.Trainings.Add(t3);
                // _dbContext.Trainings.Add(t4);
                // _dbContext.Trainings.Add(t5);

                // Exercise e1 = new Exercise("Squat", "www.image.be");
                // Exercise e2 = new Exercise("Biceps", "www.image.be");
                // Exercise e3 = new Exercise("Triceps", "www.image.be");
                // Exercise e4 = new Exercise("Flies", "www.image.be");
                // Exercise e5 = new Exercise("Kettle bell swing", "www.image.be");
                // Exercise e6 = new Exercise("Quadriceps", "www.image.be");
                // Exercise e7 = new Exercise("Polsen op en neer", "www.image.be");
                // Exercise e8 = new Exercise("Xco met knieen op bal", "www.image.be");
                // Exercise e9 = new Exercise("Flex bars", "www.image.be");
                // Exercise e10 = new Exercise("Gyrospinn", "www.image.be");


                // ExerciseEvaluation ee1 = new ExerciseEvaluation("Gaat beter dan vorige week", 8, 12.0, 20, 3, e1);
                // //ExerciseEvaluation ee2 = new ExerciseEvaluation("Pijn aan linkerschouder", 4, 0, 20, 3, e2);
                // ExerciseEvaluation ee3 = new ExerciseEvaluation("Kan volgende week hoger gewicht", 9, 12.0, 20, 3, e3);
                // ExerciseEvaluation ee4 = new ExerciseEvaluation("toch weer iets meer pijn, iets rustiger doen de volgende keer", 5, 12.0, 20, 3, e1);


                // t1.AddExerciseEvaluationToTraining(ee1);
                //// t1.AddExerciseEvaluationToTraining(ee2);
                // t1.AddExerciseEvaluationToTraining(ee3);
                // t3.AddExerciseEvaluationToTraining(ee4);


                // t1.AddExerciseToTraining(e1);
                // t1.AddExerciseToTraining(e2);
                // t1.AddExerciseToTraining(e3);

                // t2.AddExerciseToTraining(e2);


                // florian.AddTraining(t1);
                // florian.AddTraining(t2);
                // florian.AddTraining(t3);
                // florian.AddTraining(t4);

                // jonathan.AddTraining(t5);

                 _dbContext.Gymnasts.Add(gymnast);
                // _dbContext.Gymnasts.Add(jonathan);
                // _dbContext.Coaches.Add(stefan);
                // _dbContext.Coaches.Add(koen);


                // _dbContext.Excercises.Add(e4);
                // _dbContext.Excercises.Add(e5);
                // _dbContext.Excercises.Add(e6);
                // _dbContext.Excercises.Add(e7);
                // _dbContext.Excercises.Add(e8);
                // _dbContext.Excercises.Add(e9);
                // _dbContext.Excercises.Add(e10);

                // #endregion

                _dbContext.SaveChanges();
            }

        }

        //private async Task CreateUser(User user, string password) {
        //    user.NormalizedEmail = user.Email.ToUpper();
        //    user.NormalizedUserName = user.Email.ToUpper().Trim();
        //    await _userManager.CreateAsync(user, password);
        //}

        private void CreateUser(User user, string password) {
            user.NormalizedEmail = user.Email.ToUpper();
            user.NormalizedUserName = user.Email.ToUpper().Trim();
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);

        }
    }
}