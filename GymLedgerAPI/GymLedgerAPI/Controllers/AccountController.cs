using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GymLedgerAPI.Domain.DTOs;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GymLedgerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ICoachRepo _coachRepo;
        private readonly IGymnastRepo _gymnastRepo;
        private readonly IConfiguration _config;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, ICoachRepo coachRepo, IGymnastRepo gymnastRepo, IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _coachRepo = coachRepo;
            _gymnastRepo = gymnastRepo;
            _config = config;
        }


        /// <summary>
        /// Logs in the user by creating a token
        /// </summary>
        /// <param name="model">The login model</param>
        /// <returns>The created token</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> CreateToken(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded)
                {
                    string token = GetToken(user);
                    return Created("", token); //returns only the token 
                }
            }
            return BadRequest();
        }

        /// <summary>
        /// Registers a user by creating a token
        /// </summary>
        /// <param name="model">The register model</param>
        /// <returns>The created token</returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult<String> Register(RegisterDTO model) {
            User user = CreateUser(model);
            if (user != null) {
                if (model.isCoach) {
                    _coachRepo.Add((Coach)user);
                    _coachRepo.SaveChanges();
                } else {
                    _gymnastRepo.Add((Gymnast)user);
                    _gymnastRepo.SaveChanges();
                }
                string token = GetToken(user);
                return Created("", token);
            }
            return BadRequest(); 
        }

        

        /// <summary>
        /// Checks if the username is available
        /// </summary>
        /// <param name="email">The email that needs a check</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("checkusername")]
        public async Task<ActionResult<Boolean>> CheckAvailableUserName(string email) {
            var user = await _userManager.FindByNameAsync(email);
            return user == null;
        }

        /// <summary>
        /// Checks if the username exists and if it exists, checks for correct password
        /// </summary>
        /// <param name="email">The email</param>
        /// <param name="password">The password</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("userNameExists")]
        public async Task<ActionResult<Boolean>> UserNameExists(string email, string password) {
            var user = await _userManager.FindByNameAsync(email);

            if (user != null) {
                var passwordCorrect = await _signInManager.CheckPasswordSignInAsync(user, password, false);
                return (user != null) && passwordCorrect.Succeeded;
            }

            return false;
        }

        // Creates the user
        private User CreateUser(RegisterDTO model) {
            User user = null;
            if (model.isCoach) {
                user = new Coach(model.FirstName, model.LastName, model.BirthDay, model.Email);
            } else {
                user = new Gymnast(model.FirstName, model.LastName, model.BirthDay, model.Email);
            }

            user.NormalizedEmail = user.Email.ToUpper();
            user.NormalizedUserName = user.Email.ToUpper().Trim();
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);

            return user;
        }

        // Creates the token 
        private string GetToken(User user) {      // Create the token
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Lastname)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

            //test
            Console.WriteLine(key);

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                null,
                null,
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}