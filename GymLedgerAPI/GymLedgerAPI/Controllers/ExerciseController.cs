using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymLedgerAPI.Domain.DTOs;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymLedgerAPI.Controllers {
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ExerciseController : Controller {
        private readonly IExerciseRepo _exercises;
        private readonly ITrainingRepo _trainingen;
        private readonly IExerciseEvaluationRepo _evaluations;
        private readonly IGymnastRepo _gymnasts;

        public ExerciseController(IExerciseRepo exercises, ITrainingRepo trainingen, IExerciseEvaluationRepo evaluations, IGymnastRepo gymnasts) {
            _exercises = exercises;
            _trainingen = trainingen;
            _evaluations = evaluations;
            _gymnasts = gymnasts;
        }

        /// <summary>
        /// Get all the exercises from a gymnast
        /// </summary>
        /// <param name="gymnastEmail">The mail from the gymnast</param>
        /// <returns>A list with the exercises of the gymnast</returns>
        [HttpGet("{gymnastEmail}/list")]
        public ActionResult<IEnumerable<Exercise>> GetExercises(string gymnastEmail) {
            var gymnast = _gymnasts.GetByEmail(gymnastEmail);

            if (gymnast == null) {
                return NotFound("Geen gymnast met dit ID");
            }

            try {
                var oefeningen = _exercises.GetExercisesFromGymnast(gymnastEmail);

                if (oefeningen == null) {
                    return Ok(); //Een lege lijst terug sturen maar geen error
                }

                return Ok(oefeningen);
            } catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        /// <summary>
        /// Get the exercises from a certain training
        /// </summary>
        /// <param name="trainingId">The id of the asked training</param>
        /// <returns>A list of the exercises in the asked training</returns>
        [HttpGet("training/{trainingId}")]
        public ActionResult<IEnumerable<Exercise>> GetExercisesInTraining(int trainingId) {
            Training t = _trainingen.GetbyId(trainingId);

            if (t == null) {
                return NotFound("Geen training met dit Id");
            }

            try {
                var exercises = t.TrainingExercises.ToList().Select(te => te.Exercise);

                return Ok(exercises);

            } catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        /// <summary>
        /// Get the exercises that are not in a certain training
        /// </summary>
        /// <param name="email">The email of the gymnast</param>
        /// <param name="trainingId">The asked training</param>
        /// <returns>A list of the exercises</returns>
        [HttpGet("oefeningNietInTraining/{trainingId}/{email}")]
        public ActionResult<IEnumerable<Exercise>> GetExercisesNotInTraining(string email, int trainingId) {

            var gymnast = _gymnasts.GetByEmail(email);
            Training t = _trainingen.GetbyId(trainingId);

            if (gymnast == null) {
                return NotFound("Geen gymnast met dit ID");
            }

            if (t == null) {
                return NotFound();
            }

            try {
                var exercisesInTraining = t.TrainingExercises.ToList().Select(te => te.Exercise).ToList();
                var allExercises = _exercises.GetExercisesFromGymnast(email).ToList();

                var exerciseNotInTraining = allExercises.Except(exercisesInTraining);

                return Ok(exerciseNotInTraining);
            } catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }

        /// <summary>
        /// Creates a new exercise for the gymnast
        /// </summary>
        /// <param name="model">The exercise</param>
        /// <param name="gymnastEmail">The email of the gymnast</param>
        /// <returns>The created exercise</returns>
        [HttpPost("{gymnastEmail}")]
        public ActionResult<Exercise> CreateExercise([FromBody] ExerciseDTO model, string gymnastEmail) {
            var gymnast = _gymnasts.GetByEmail(gymnastEmail);

            if (gymnast == null) {
                return NotFound("Geen gymnast met deze email");
            }

            try {
                Exercise newExercise = new Exercise(model.Description, "", gymnast);
                _exercises.Add(newExercise);
                _exercises.SaveChanges();

                return Ok(newExercise);


            } catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }



        /// <summary>
        /// Add an existing exercise to a training
        /// </summary>
        /// <param name="trainingId">The id of the training</param>
        /// <param name="exerciseId">The id of the exercise</param>
        /// <returns>The added exercise to a training</returns>
        [HttpPost("{trainingId}/{exerciseId}")]
        public ActionResult<Exercise> AddExerciseToTraining(int trainingId, int exerciseId) {

            var training = _trainingen.GetbyId(trainingId);
            var exercise = _exercises.GetbyId(exerciseId);


            if (training == null) {
                return NotFound();
            }

            if (exercise == null) {
                return NotFound();
            }

            try {
                training.AddExerciseToTraining(exercise);
                _trainingen.SaveChanges();

                return Ok(exercise);

            } catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        /// <summary>
        /// Removes an existing exercise from a training with its evaluation
        /// </summary>
        /// <param name="trainingId">The id of the training</param>
        /// <param name="exerciseId">The id of the exercise</param>
        /// <returns>The removed exercise</returns>
        [HttpDelete("{trainingId}/{exerciseId}")]
        public ActionResult<Exercise> RemoveExerciseFromTraining(int trainingId, int exerciseId) {
            var training = _trainingen.GetbyId(trainingId);
            var exercise = _exercises.GetbyId(exerciseId);

            if (training == null) {
                return NotFound("Geen training met dit Id gevonden.");
            }

            if (exercise == null) {
                return NotFound("Geen oefening met dit Id gevonden.");
            }


            try {
                var evaluation = _evaluations.GetEvaluationFromExerciseInTraining(trainingId, exerciseId);
                if (evaluation != null) {
                    _evaluations.Remove(evaluation);
                    _evaluations.SaveChanges();
                }

                training.DeleteExerciseFromTraining(exercise);
                _trainingen.SaveChanges();

                return Ok(exercise);
            } catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }



        // ---------- tijdelijk niet nodig ------------

        ///// <summary>
        ///// Get all the exercises 
        ///// </summary>
        ///// <returns>A list with all the exercises</returns>
        //[HttpGet]
        //public ActionResult<IEnumerable<Exercise>> GetAll() {
        //    try {
        //        var exercises = _exercises.GetAll().ToList();

        //        if (exercises == null) {
        //            return Ok(); // Bij nog geen oefeningen, lege lijst teruggeven
        //        }

        //        return Ok(exercises);
        //    } catch (Exception e) {
        //        return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

        //    }
        //}


        /// <summary>
        /// Get all trainings not in training to choose from
        /// </summary>
        /// <param name="trainingId">Id of the training</param>
        /// <returns>The list of exercises</returns>
        //[HttpGet("oefeningNietInTraining/{trainingId}")]
        //public ActionResult<IEnumerable<Exercise>> GetExercisesNotInTraining(int trainingId) {
        //    Training t = _trainingen.GetbyId(trainingId);

        //    if (t == null) {
        //        return NotFound();
        //    }

        //    try {
        //        var exercisesInTraining = t.TrainingExercises.ToList().Select(te => te.Exercise).ToList();
        //        var allExercises = _exercises.GetAll().ToList();

        //        var exerciseNotInTraining = allExercises.Except(exercisesInTraining);

        //        return Ok(exerciseNotInTraining);
        //    } catch (Exception e) {
        //        return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

        //    }
        //}
    }
}
