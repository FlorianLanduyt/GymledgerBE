using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.DTOs {
    public class TrainingDTO {

        [Required]
        public DateTime Day { get; set; }

        [Required]
        public KindOfTraining Category { get; set; }

        
        public string BeforeFeeling { get; set; }
        public string AfterFeeling { get; set; }

        public ICollection<Exercise> Exercises { get; set; }

    }
}
