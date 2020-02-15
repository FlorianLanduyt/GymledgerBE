using System;
using System.ComponentModel.DataAnnotations;

namespace GymLedgerAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Email { get; set; }



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
