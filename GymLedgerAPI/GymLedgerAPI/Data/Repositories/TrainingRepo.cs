using System;
using System.Collections.Generic;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.Data.Repositories
{
    public class TrainingRepo : ITrainingRepo
    {
        public TrainingRepo()
        {
        }

        public void Add(Training obj)
        {
            throw new NotImplementedException();
        }

        public ICollection<Training> GetAll()
        {
            throw new NotImplementedException();
        }

        public Training GetbyId(long id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Training obj)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
