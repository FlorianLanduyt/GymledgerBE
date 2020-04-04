using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.DTOs;
using GymLedgerAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymLedgerAPI.Controllers
{
    [Route("api/[controller]")]
    public class TrainingController : Controller {
        private readonly IGymnastRepo _gymnasts;
        private readonly ITrainingRepo _trainings;
        private readonly ICategoryRepo _categories;

        public TrainingController(IGymnastRepo gymnasts, ITrainingRepo trainings, ICategoryRepo categories) {
            _gymnasts = gymnasts;
            _trainings = trainings;
            _categories = categories;
        }

        [HttpGet("{trainingId}")]
        public ActionResult<Training> GetTraining(int trainingId) {
            try {
                return _trainings.GetbyId(trainingId);
            } catch (ArgumentException) {
                return NotFound("Geen gymnast met dit ID");
            }
        }

        [HttpGet("{gymnastId}/trainings")]
        public ActionResult<IEnumerable<Training>> GetAllTrainingsFromGymnast(int gymnastId) {
            return _trainings.GetAllTrainingsFromGymnast(gymnastId).ToList();
        }

        [HttpPost("{gymnastId}")]
        public ActionResult<Training> CreateNewTraining([FromBody]TrainingDTO trainingDTO, int gymnastId) {
            Gymnast gymnast = _gymnasts.GetbyId(gymnastId);
            Category category = _categories.GetbyId(trainingDTO.CategoryId);

            Training training = new Training(category, trainingDTO.Date, trainingDTO.FeelingBeforeTraining, trainingDTO.FeelingAfterTraining);


            // Andere post-it
            //trainingDTO.Exercises.ToList().ForEach(e => {
            //    trainine = new Exercise(e.Description, e.Number, e.Image)
            //});

            gymnast.AddTraining(training);
            _gymnasts.SaveChanges();

            return CreatedAtAction(nameof(GetTraining), new { trainingId = training.Id }, training);
        }


        [HttpDelete("{id}")]
        public ActionResult<Training> Remove(int id) {
            Training trainingToDelete = this._trainings.GetbyId(id);

            if (trainingToDelete == null) {
                return NotFound();
            }

            try {
                _trainings.Remove(trainingToDelete);
                _trainings.SaveChanges();

                return Ok(trainingToDelete);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("edit")]
        public ActionResult<Training> Edit([FromBody]TrainingDTO trainingDTO) {
            Training trainingToEdit = _trainings.GetbyId(trainingDTO.trainingId);
            Category category = _categories.GetbyId(trainingDTO.CategoryId);

            if (trainingToEdit == null) {
                return NotFound();
            }

            if(category == null) {
                return NotFound();
            }

            try {
                trainingToEdit.Category = category;
                trainingToEdit.Date = trainingDTO.Date;
                trainingToEdit.FeelingBeforeTraining = trainingDTO.FeelingBeforeTraining;
                trainingToEdit.FeelingAfterTraining = trainingDTO.FeelingAfterTraining;
                _trainings.SaveChanges();
                return Ok(trainingToEdit);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        } 


    }
}
