using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using GymLedgerAPI.Domain.Models;

using Microsoft.AspNetCore.Identity;

namespace GymLedgerAPI.Models
{
    public class User : IdentityUser
    {
        [Required]
        public bool IsCoach { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public DateTime BirthDate { get; set; }

        //[Required]
        //public string Email { get; set; }



        public User(string firstname, string lastname, DateTime birthDate, string email)
        {
            UserName = firstname + lastname;
            Firstname = firstname;
            Lastname = lastname;
            BirthDate = birthDate;
            Email = email;
        }

        protected User()
        {
        }

    }
}
