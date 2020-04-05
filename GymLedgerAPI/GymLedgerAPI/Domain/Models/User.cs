﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using GymLedgerAPI.Domain.Models;

using Microsoft.AspNetCore.Identity;

namespace GymLedgerAPI.Models
{
    public class User : IdentityUser<int>
    {
        //public int UserId { get; set; }
        [Required]
        public bool isCoach { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        //[Required]
        //public string Email { get; set; }



        public User(string firstname, string lastname, DateTime birthDate, string email)
        {
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