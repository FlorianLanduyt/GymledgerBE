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


        public TrainingController(IGymnastRepo gymnasts, ITrainingRepo trainings) {
            _gymnasts = gymnasts;
            _trainings = trainings;
        }

        [HttpGet("{trainingId}")]
        public ActionResult<Training> GetTraining(int trainingId) {
            try {
                return _trainings.GetbyId(trainingId);
            } catch (ArgumentException) {
                return NotFound("Geen gymnast met dit ID");
            }
        }

        [HttpPost("{gymnastId}")]
        public ActionResult<Training> CreateNewTraining([FromBody]TrainingDTO trainingDTO, int gymnastId) {
            Gymnast gymnast = _gymnasts.GetbyId(gymnastId);
            Training training = new Training(trainingDTO.Category, trainingDTO.Day, trainingDTO.BeforeFeeling, trainingDTO.AfterFeeling);


            // Andere post-it
            //trainingDTO.Exercises.ToList().ForEach(e => {
            //    trainine = new Exercise(e.Description, e.Number, e.Image)
            //});

            gymnast.AddTraining(training);
            _gymnasts.SaveChanges();

            return CreatedAtAction(nameof(GetTraining), new { trainingId = training.Id }, training);
        }


    }
}
