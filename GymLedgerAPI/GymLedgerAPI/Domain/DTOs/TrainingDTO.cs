using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.DTOs {
    public class TrainingDTO {
        public int trainingId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category{ get; set; }  


        public int FeelingBeforeTraining { get; set; }
        public int FeelingAfterTraining { get; set; }

        public ICollection<Exercise> Exercises { get; set; }

    }
}
