using System;
using System.Collections.Generic;
using System.Linq;
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

        public void RemoveGymnast(Gymnast g)
        {
            GymnastCoaches.Remove(GymnastCoaches.FirstOrDefault(gc => gc.Gymnast == g));
        }
    }
}
