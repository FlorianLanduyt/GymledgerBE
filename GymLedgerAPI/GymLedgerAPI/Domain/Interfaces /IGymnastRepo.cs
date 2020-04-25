using System;
using System.Collections.Generic;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.Domain.Interfaces
{
    public interface IGymnastRepo : IGenericRepo<Gymnast>
    {
        ICollection<Gymnast> GetGymnastsFromCoach(string coachId);
        Gymnast GetGymnastWithTrainings(string gymnastId);
        Gymnast GetbyIdString(string id);
        Gymnast GetByEmail(string email);
    }
}
