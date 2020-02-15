using System;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.Domain.Models
{
    public class GymnastCoach
    {
        public Gymnast Gymnast { get; set; }
        public long GymnastId { get; set; }

        public Coach Coach { get; set; }
        public long CoachId { get; set; }

        public GymnastCoach(Gymnast g, long gId, Coach c, long cId)
        {
            Gymnast = g;
            GymnastId = gId;
            Coach = c;
            CoachId = cId;
        }
    }
}
