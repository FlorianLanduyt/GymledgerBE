using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.DTOs {
    public class TrainingDTO {

        [Required]
        public DateTime Day { get; set; }

        [Required]
        public Category Category { get; set; }

        
        public int BeforeFeeling { get; set; }
        public int AfterFeeling { get; set; }

        public ICollection<Exercise> Exercises { get; set; }

    }
}
