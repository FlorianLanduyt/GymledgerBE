using System.Collections.Generic;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.Domain.Interfaces
{
    public interface IExerciseRepo : IGenericRepo<Exercise>
    {
        ICollection<Exercise> GetExercisesFromGymnast(string email);
        IEnumerable<Exercise> GetByName(string name);
    }
}
