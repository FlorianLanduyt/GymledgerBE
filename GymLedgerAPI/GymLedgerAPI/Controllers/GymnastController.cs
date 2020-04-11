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
    public class GymnastController : Controller
    {
        private readonly IGymnastRepo _gymnasts;

        public GymnastController(IGymnastRepo gymnasts)
        {
            _gymnasts = gymnasts;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Gymnast>> GetGymnasts()
        {
            return _gymnasts.GetAll().ToList();
        }


        [HttpGet("{gymnastId}")]
        public ActionResult<Gymnast> GetGymnast(string gymnastId)
        {
            Gymnast g = _gymnasts.GetbyIdString(gymnastId);

            if (g == null)
            {
                return NotFound();
            }
            return g;
        }

        [HttpGet("gymnastsWithTraining/{gymnastId}")]
        public ActionResult<Gymnast> GetGymnastWithTraining(string gymnastId) {
            try {
                return _gymnasts.GetGymnastWithTrainings(gymnastId);
            } catch (ArgumentNullException) {
                return NotFound("Geen gymnast met dit id");
            }
        }

        [HttpGet("gymnast/{email}")]
        public ActionResult<Gymnast> GetGymnastByEmail(string email) {
            try {
                return _gymnasts.GetByEmail(email);
            } catch (ArgumentNullException) {
                return NotFound("Geen gymnast met deze email");
            }
        }



        [HttpGet("gymnasts/{CoachId}")]
        public ActionResult<IEnumerable<Gymnast>> GetGymnastFromCoach(string coachId)
        {
            try {
                return _gymnasts.GetGymnastsFromCoach(coachId).ToList();
            } catch (ArgumentNullException) {
                return NotFound("Geen gymnasten met deze coach");
            }
        }





        







    }
}
