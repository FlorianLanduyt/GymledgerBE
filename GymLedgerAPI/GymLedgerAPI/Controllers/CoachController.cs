using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace GymLedgerAPI.Controllers
{
    [Route("api/[controller]")]
    public class CoachController : Controller
    {
        private readonly ICoachRepo _coachRepo;

        public CoachController(ICoachRepo coachRepo)
        {
            _coachRepo = coachRepo;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Coach>> GetCoaches()
        {
            return _coachRepo.GetAll().ToList();
        }

        [HttpGet("{coachId}")]
        public ActionResult<Coach> GetGymnast(int coachId) {
            Coach coach = _coachRepo.GetbyId(coachId);

            if (coach == null) {
                return NotFound();
            }
            return coach;
        }


    }
}
