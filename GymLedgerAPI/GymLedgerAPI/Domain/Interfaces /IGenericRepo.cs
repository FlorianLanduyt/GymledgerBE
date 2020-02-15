using System;
using System.Collections.Generic;

namespace GymLedgerAPI.Domain.Interfaces
{
    public interface IGenericRepo<T> where T: class
    {
        ICollection<T> GetAll();
        T GetbyId(long id);
        void Add(T obj);
        void Remove(T obj);
        void SaveChanges();
    }
}
