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
    public class UserController : Controller
    {
        private readonly IUserRepo _userRepo;

        //private readonly IGymnastRepo _gymnastRepo;
        //private readonly ICoachRepo _coachRepo;


        public UserController(
            //IGymnastRepo gymnastRepo,
            //ICoachRepo coachRepo,
            IUserRepo userRepo)
        {
            //_gymnastRepo = gymnastRepo;
            //_coachRepo = coachRepo;
            _userRepo = userRepo;
        }

        // GET: api/values
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return _userRepo.GetAll().ToList();
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<Coach>> GetCoaches()
        //{
        //    return _coachRepo.GetAll().ToList();
        //}



        
    }
}
