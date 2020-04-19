using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymLedgerAPI.Controllers
{
    [Route("api/[controller]")]
    public class ExerciseController : Controller {
        private readonly IExerciseRepo _exercises;
        private readonly ITrainingRepo _trainingen;

        public ExerciseController(IExerciseRepo exercises, ITrainingRepo trainingen) {
            _exercises = exercises;
            _trainingen = trainingen;
        }

        /// <summary>
        /// Get all the exercises 
        /// </summary>
        /// <returns>A list with all the exercises</returns>
        [HttpGet]
        public IEnumerable<Exercise> GetAll() {
            return _exercises.GetAll().ToList();
        }


        /// <summary>
        /// Add a existing exercise to a training
        /// </summary>
        /// <param name="trainingId"></param>
        /// <param name="exerciseId"></param>
        /// <returns></returns>
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

            } catch(Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
