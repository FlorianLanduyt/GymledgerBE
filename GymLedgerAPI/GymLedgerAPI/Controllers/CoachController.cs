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
    }
}
