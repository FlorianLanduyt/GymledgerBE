using System;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.Domain.Interfaces
{
    public interface ICoachRepo : IGenericRepo<Coach>
    {
        Coach GetbyId(string id);
    }
}
