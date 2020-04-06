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

        private string GetToken(User user)
        {      // Create the token
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