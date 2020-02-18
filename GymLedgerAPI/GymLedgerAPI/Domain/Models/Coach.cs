﻿using System;
using System.Collections.Generic;
using GymLedgerAPI.Domain.Models;

namespace GymLedgerAPI.Models
{
    public class Coach : User
    {
        //public int CoachId { get; set; }
        public IList<GymnastCoach> GymnastCoaches { get; set; }  

        public Coach(string firstname, string lastname, DateTime birthday, string email): base(firstname, lastname, birthday, email)
        {
            GymnastCoaches = new List<GymnastCoach>();
            isCoach = true;
        }

        protected Coach()
        {
        }

        public void AddGymnast(Gymnast g)
        {
            GymnastCoaches.Add(new GymnastCoach(g, this));
        }
    }
}
