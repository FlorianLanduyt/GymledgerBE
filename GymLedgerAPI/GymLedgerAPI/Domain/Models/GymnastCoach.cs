﻿using System;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.Domain.Models
{
    public class GymnastCoach
    {
        public Gymnast Gymnast { get; set; }
        public long GymnastId { get; set; }

        public Coach Coach { get; set; }
        public long CoachId { get; set; }

        public GymnastCoach(Gymnast g, Coach c)
        {
            Gymnast = g;
            GymnastId = g.Id;
            Coach = c;
            CoachId = c.Id;
        }

        protected GymnastCoach()
        {

        }
    }
}
