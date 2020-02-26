using System;
using System.Collections.Generic;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.Domain.Interfaces
{
    public interface IGymnastRepo : IGenericRepo<Gymnast>
    {
        ICollection<Gymnast> GetGymnastsFromCoach(int coachId);
        Gymnast GetGymnastWithTrainings(int gymnastId);
    }
}
