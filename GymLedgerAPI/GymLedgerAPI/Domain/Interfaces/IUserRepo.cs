using System;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.Domain.Interfaces
{
    public interface IUserRepo : IGenericRepo<User>
    {
        User GetbyIdString(string id);
    }
}
