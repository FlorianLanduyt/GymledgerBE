using System;
using System.Collections.Generic;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.Data.Repositories
{
    public class ExerciseRepo : IExerciseRepo
    {
        public ExerciseRepo()
        {
        }

        public void Add(Exercise obj)
        {
            throw new NotImplementedException();
        }

        public ICollection<Exercise> GetAll()
        {
            throw new NotImplementedException();
        }

        public Exercise GetbyId(long id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Exercise obj)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
