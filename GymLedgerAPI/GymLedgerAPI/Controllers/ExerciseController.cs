using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymLedgerAPI.Controllers
{
    [Route("api/[controller]")]
    public class ExerciseController : Controller
    {
        private readonly IExerciseRepo _exercises;

        public ExerciseController(IExerciseRepo exercises) {
            _exercises = exercises;
        }

        /// <summary>
        /// Get all the exercises 
        /// </summary>
        /// <returns>A list with all the exercises</returns>
        [HttpGet]
        public IEnumerable<Exercise> GetAll() {
            return _exercises.GetAll().ToList();
        }
    }
}
