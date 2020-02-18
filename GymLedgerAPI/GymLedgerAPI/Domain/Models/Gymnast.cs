using System;
using System.Collections.Generic;
using GymLedgerAPI.Domain.Models;

namespace GymLedgerAPI.Models
{
    public class Gymnast : User
    {

        //public int GymnastId { get; set; }
        public IList<Training> Trainings { get; set; }
        public IList<GymnastCoach> GymnastCoaches { get; set; }

        public Gymnast(string firstname, string lastname, DateTime birthday, string email): base(firstname, lastname, birthday, email)
        {
            Trainings = new List<Training>();
            GymnastCoaches = new List<GymnastCoach>();
        }

        protected Gymnast()
        {

        }
    }
}
