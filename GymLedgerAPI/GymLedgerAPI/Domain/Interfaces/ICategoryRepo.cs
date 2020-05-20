using System;
using System.Threading.Tasks;
using GymLedgerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymLedgerAPI.Domain.Interfaces {
    public interface ICategoryRepo : IGenericRepo<Category>{
        Category GetByName(string name);
    }
}
