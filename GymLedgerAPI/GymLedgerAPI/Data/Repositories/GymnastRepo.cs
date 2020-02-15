using System;
using System.Collections.Generic;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.Data.Repositories
{
    public class GymnastRepo : IGymnastRepo
    {
        public GymnastRepo()
        {
        }

        public void Add(Gymnast obj)
        {
            throw new NotImplementedException();
        }

        public ICollection<Gymnast> GetAll()
        {
            throw new NotImplementedException();
        }

        public Gymnast GetbyId(long id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Gymnast obj)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
