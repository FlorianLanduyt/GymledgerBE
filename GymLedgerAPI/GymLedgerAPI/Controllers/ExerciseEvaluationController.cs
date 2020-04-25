using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymLedgerAPI.Domain.DTOs;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymLedgerAPI.Controllers
{
    [Route("api/[controller]")]
    public class ExerciseEvaluationController : Controller {
        private readonly IExerciseEvaluationRepo _evaluations;
        private readonly ITrainingRepo _trainings;
        private readonly IExerciseRepo _exercises;

        public ExerciseEvaluationController(IExerciseEvaluationRepo evaluations, ITrainingRepo trainings, IExerciseRepo exercises) {
            _evaluations = evaluations;
            _trainings = trainings;
            _exercises = exercises;
        }

        /// <summary>
        /// Get all the evaluations of an exercise in any training
        /// </summary>
        /// <returns>A list with exercise evaluations</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ExerciseEvaluation>> GetAll() {
            return Ok(_evaluations.GetAll().ToList());
        }

        /// <summary>
        /// Get the evaluations of an exercise in a given training 
        /// </summary>
        /// <param name="trainingId">The id of the training</param>
        /// <returns>A list with exercise evaluations</returns>
        [HttpGet("{trainingId}")]
        public ActionResult<IEnumerable<ExerciseEvaluation>> GetFromTraining(int trainingId) {
            var training = _trainings.GetbyId(trainingId);

            if(training == null) {
                return NotFound("Geen training met dit Id gevonden");
            }
            try {
                return Ok(_evaluations.GetAllFromTraining(trainingId));
            } catch(Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }


        /// <summary>
        /// Get the evaluation from an exercise in a training
        /// </summary>
        /// <param name="trainingId">The id of the training</param>
        /// <param name="exerciseId">The id of the exercise</param>
        /// <returns>The evaluation</returns>
        [HttpGet("{trainingId}/{exerciseId}")]
        public ActionResult<ExerciseEvaluation> GetEvaluationFromExerciseInTraining (int trainingId, int exerciseId) {
            var training = _trainings.GetbyId(trainingId);
            var exercise = _exercises.GetbyId(exerciseId);

            if (training == null) {
                return NotFound("Geen training met dit Id gevonden");
            }

            if (exercise == null) {
                return NotFound("Geen oefening met dit Id gevonden");
            }

            try {
                var evaluations = _evaluations.GetEvaluationFromExerciseInTraining(trainingId, exerciseId);
                if(evaluations == null) {
                    return Ok(); //Teruggave van een lege lijst
                }
                return Ok(evaluations);

            } catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }


        [HttpPost("{trainingId}/{exerciseId}")]
        public ActionResult<ExerciseEvaluation> CreateEvaluation([FromBody] EvaluationDTO model,  int trainingId, int exerciseId) {
            

            var training = _trainings.GetbyId(trainingId);
            var exercise = _exercises.GetbyId(exerciseId);

            if (training == null) {
                return NotFound("Geen training met dit Id gevonden");
            }

            if (exercise == null) {
                return NotFound("Geen oefening met dit Id gevonden");
            }


            try {
                ExerciseEvaluation ee = new ExerciseEvaluation(model.Note, model.FeelingOfExercise, model.Weight, model.Repetitions, model.Sets, exercise);

                training.AddExerciseEvaluationToTraining(ee);

                _evaluations.SaveChanges();
                _trainings.SaveChanges();

                return Ok(ee);

            } catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [HttpPut]
        public ActionResult<ExerciseEvaluation> EditEvaluation([FromBody] EvaluationDTO model) {
            var evaluation = _evaluations.GetbyId(model.Id);
           

            if (evaluation == null) {
                return NotFound("Geen evaluation met dit Id gevonden");
            }

            try {

                evaluation.Note = model.Note;
                evaluation.DifficultyScore = model.FeelingOfExercise;
                evaluation.Weight = model.Weight;
                evaluation.Repetitions = model.Repetitions;
                evaluation.Series = model.Sets;


                _evaluations.SaveChanges();

                return Ok(evaluation);

            } catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }




    }
}
