using System;
using System.ComponentModel.DataAnnotations;

namespace GymLedgerAPI.Domain.DTOs {
    public class RegisterDTO {
        [Required]
        public string Email { get; set; }

        [Required]
        public bool isCoach { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Password { get; set; }


        [Required]
        public DateTime BirthDay { get; set; }
    }
}
