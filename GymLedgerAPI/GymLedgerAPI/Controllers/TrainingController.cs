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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [AllowAnonymous]
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
        /// Get the training with the given ID
        /// </summary>
        /// <param name="trainingId">The ID of a training</param>
        /// <returns>The training</returns>
        [HttpGet("{trainingId}")]
        public ActionResult<Training> GetTraining(int trainingId) {
            try {
                return _trainings.GetbyId(trainingId);
            } catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Get all the trainings of a gymnast with the given ID
        /// </summary>
        /// <param name="gymnastId">The ID of a gymnast</param>
        /// <returns>A list of trainings of a particularly gymnast</returns>
        [HttpGet("{email}/trainings")]
        public ActionResult<IEnumerable<Training>> GetAllTrainingsFromGymnast(string email) {
            return _trainings.GetAllTrainingsFromGymnast(email).ToList();
        }

        /// <summary>
        /// Create a new training
        /// </summary>
        /// <param name="trainingDTO">The training to add</param>
        /// <param name="email">The email of a the gymnast where to add the training </param>
        /// <returns>The training</returns>
        [HttpPost("{email}")]
        public ActionResult<Training> CreateNewTraining([FromBody]TrainingDTO trainingDTO, string email) {
            Gymnast gymnast = _gymnasts.GetByEmail(email);
            Category category = _categories.GetbyId(trainingDTO.CategoryId);

            if(gymnast == null) {
                return NotFound("Geen gymnast met dit ID.");
            }

            if(category == null) {
                return NotFound("Geen category met dit ID.");
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
        /// <returns>The training</returns>
        [HttpDelete("{id}")]
        public ActionResult<Training> Remove(int id) {
            Training trainingToDelete = this._trainings.GetbyId(id);

            if (trainingToDelete == null) {
                return NotFound();
            }

            try {
                _trainings.Remove(trainingToDelete);
                _trainings.SaveChanges();

                return NoContent();
            } catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        /// <summary>
        /// Edit the training
        /// </summary>
        /// <param name="trainingDTO">A model of the changed training</param>
        /// <returns>The changed training</returns>
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
