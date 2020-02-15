using System;
using System.Threading.Tasks;
using GymLedgerAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace GymLedgerAPI.Data
{
    public class DataInit
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public DataInit(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }



        public async Task InitializeData()
        {


        }
    }
}
