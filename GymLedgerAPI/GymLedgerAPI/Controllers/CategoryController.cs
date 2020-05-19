using System;
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

namespace GymLedgerAPI.Controllers {
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class CategoryController : Controller {
        private readonly ICategoryRepo _categoryRepo;

        public CategoryController(ICategoryRepo categoryRepo) {
            _categoryRepo = categoryRepo;
        }


        /// <summary>
        /// Get all the categories available
        /// </summary>
        /// <returns>The list of categories</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories() {
            try {
                var categories = _categoryRepo.GetAll().ToList();

                if(categories == null) {
                    return Ok(); // Bij lege lijst geen error maar de lege lijst terug geven 
                }
                return Ok(categories);

            } catch(Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
