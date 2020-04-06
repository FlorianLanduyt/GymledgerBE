using System;
using System.Collections.Generic;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.Domain.Interfaces
{
    public interface ITrainingRepo : IGenericRepo<Training>
    {
        ICollection<Training> GetAllTrainingsFromGymnast(string gymnastId);
    }
}
