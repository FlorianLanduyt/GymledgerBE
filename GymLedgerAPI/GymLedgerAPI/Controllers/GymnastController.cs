﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymLedgerAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GymnastController : Controller
    {
        private readonly IGymnastRepo _gymnasts;

        public GymnastController(IGymnastRepo gymnasts)
        {
            _gymnasts = gymnasts;
        }


        /// <summary>
        /// Get a certain gymnast
        /// </summary>
        /// <param name="email">The email of the gymnast</param>
        /// <returns>The certain gymnast</returns>
        [HttpGet("gymnast/{email}")]
        public ActionResult<Gymnast> GetGymnast(string email) {
            var g = _gymnasts.GetByEmail(email);
            if (g == null) {
                return NotFound("Geen gebruiker met deze email.");
            }

            try {
                return Ok(g);
            } catch (ArgumentNullException e) {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }




        // ---------- Tijdelijk niet nodig ---------

        //[HttpGet]
        //public ActionResult<IEnumerable<Gymnast>> GetGymnasts()
        //{
        //    return _gymnasts.GetAll().ToList();
        //}


        //[HttpGet("{gymnastId}")]
        //public ActionResult<Gymnast> GetGymnast(string gymnastId)
        //{
        //    Gymnast g = _gymnasts.GetbyIdString(gymnastId);

        //    if (g == null)
        //    {
        //        return NotFound();
        //    }
        //    return g;
        //}

        //[HttpGet("gymnastsWithTraining/{gymnastId}")]
        //public ActionResult<Gymnast> GetGymnastWithTraining(string gymnastId) {
        //    try {
        //        return _gymnasts.GetGymnastWithTrainings(gymnastId);
        //    } catch (ArgumentNullException) {
        //        return NotFound("Geen gymnast met dit id");
        //    }
        //}


        //[HttpGet("gymnasts/{CoachId}")]
        //public ActionResult<IEnumerable<Gymnast>> GetGymnastFromCoach(string coachId)
        //{
        //    try {
        //        return _gymnasts.GetGymnastsFromCoach(coachId).ToList();
        //    } catch (ArgumentNullException) {
        //        return NotFound("Geen gymnasten met deze coach");
        //    }
        //}





    }
}
