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

        [HttpGet("gymnasts/{CoachId}")]
        public ActionResult<IEnumerable<Gymnast>> GetGymnastFromCoach(int coachId)
        {
            return _gymnasts.GetGymnastsFromCoach(coachId).ToList();
        }
    }
}
