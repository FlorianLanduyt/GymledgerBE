using System;
using System.ComponentModel.DataAnnotations;

namespace GymLedgerAPI.Domain.DTOs {
    public class ExerciseDTO {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }


    }
}
