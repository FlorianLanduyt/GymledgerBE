using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.DTOs;
using GymLedgerAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymLedgerAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TrainingController : Controller {
        private readonly IGymnastRepo _gymnasts;
        private readonly ITrainingRepo _trainings;
        private readonly ICategoryRepo _categories;

        public TrainingController(IGymnastRepo gymnasts, ITrainingRepo trainings, ICategoryRepo categories) {
            _gymnasts = gymnasts;
            _trainings = trainings;
            _categories = categories;
        }

        /// <summary>
        /// Get all the trainings of a gymnast with the given ID
        /// </summary>
        /// <param name="gymnastId">The ID of a gymnast</param>
        /// <returns>A list of trainings of a particularly gymnast</returns>
        [HttpGet("{email}/list")]
        public ActionResult<IEnumerable<Training>> GetAllTrainingsFromGymnast(string email) {
            try {
                var trainings = _trainings.GetAllTrainingsFromGymnast(email).ToList();

                if (trainings == null) {
                    return Ok(); // returns empty list
                }

                return Ok(trainings);
            } catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Get a certain training
        /// </summary>
        /// <param name="trainingId">The id of the training</param>
        /// <returns>The training</returns>
        [HttpGet("{trainingId}")]
        public ActionResult<Training> GetTraining(int trainingId) {
            Training t = _trainings.GetbyId(trainingId);

            if(t == null) {
                return NotFound();
            }

            try {
                return Ok(_trainings.GetbyId(trainingId));
            } catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        

        /// <summary>
        /// Create a new training
        /// </summary>
        /// <param name="trainingDTO">The model of the training to add</param>
        /// <param name="email">The email of a the gymnast where to add the training</param>
        /// <returns>The created training</returns>
        [HttpPost("{email}")]
        public ActionResult<Training> CreateTraining([FromBody]TrainingDTO trainingDTO, string email) {
            Gymnast gymnast = _gymnasts.GetByEmail(email);
            Category category = _categories.GetbyId(trainingDTO.CategoryId);

            
            if(gymnast == null) {
                return NotFound("Geen gymnast met dit ID.");
            }

            
            if(category == null && trainingDTO.Category != null) {
                category = new Category(trainingDTO.Category, ""); // tijdelijke geen beschrijving
                _categories.SaveChanges();
            }

            if (category == null && trainingDTO.Category == null) {
                return NotFound("Geen category met dit ID");
            }

            try {
                Training training = new Training(category, trainingDTO.Date, trainingDTO.FeelingBeforeTraining, trainingDTO.FeelingAfterTraining);
                gymnast.AddTraining(training);
                _gymnasts.SaveChanges();

                return CreatedAtAction(nameof(GetTraining), new { trainingId = training.Id }, training);
            } catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            
        }

        /// <summary>
        /// Remove a training
        /// </summary>
        /// <param name="id">The id of the training you want to delete</param>
        /// <returns>The deleted training</returns>
        [HttpDelete("{id}")]
        public ActionResult<Training> RemoveTraining(int id) {
            Training trainingToDelete = _trainings.GetbyId(id);

            if (trainingToDelete == null) {
                return NotFound();
            }

            try {
                _trainings.Remove(trainingToDelete);
                _trainings.SaveChanges();

                return Ok(trainingToDelete);
            } catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        /// <summary>
        /// Edit a certain training
        /// </summary>
        /// <param name="trainingDTO">A model of the edited training</param>
        /// <returns>The edited training</returns>
        [HttpPut("edit")]
        public ActionResult<Training> EditTraining([FromBody]TrainingDTO trainingDTO) {
            Training trainingToEdit = _trainings.GetbyId(trainingDTO.trainingId);
            Category category = _categories.GetbyId(trainingDTO.CategoryId);


            if (category == null && trainingDTO.Category != null) {
                category = new Category(trainingDTO.Category, ""); // tijdelijke geen beschrijving
                _categories.SaveChanges();
            }

            if (category == null && trainingDTO.Category == null) {
                return NotFound("Geen category met dit ID");
            }


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
