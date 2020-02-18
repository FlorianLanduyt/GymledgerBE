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
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                #region Users
                // ----------- Creating the coaches and gymnasts ---------------

                var password = "root";

                Coach stefan = new Coach("Stefan", "Deckx", new DateTime(1978, 02, 10), "stefan.deckx@hotmail.com");
                Coach koen = new Coach("Koen", "Van Damme", new DateTime(1986, 03, 10), "koen.vandamme@hotmail.com");

                Gymnast florian = new Gymnast("Florian", "Landuyt", new DateTime(1996, 05, 10), "florian.landuyt@hotmail.com");
                Gymnast jonathan = new Gymnast("Jonathan", "Vrolix", new DateTime(1996, 11, 09), "jonathan.vrolix@hotmail.com");


                // ---------- Adding gymnasts to their coaches -----------------

                stefan.AddGymnast(florian);
                stefan.AddGymnast(jonathan);

                //koen.AddGymnast(florian);


                // ---------- Adding users to the database ---------------------

                _dbContext.AppUsers.Add(koen);
                _dbContext.AppUsers.Add(stefan);
                _dbContext.AppUsers.Add(florian);
                _dbContext.AppUsers.Add(jonathan);

                _dbContext.Gymnasts.Add(florian);
                _dbContext.Gymnasts.Add(jonathan);
                _dbContext.Coaches.Add(stefan);
                _dbContext.Coaches.Add(koen);


                // ---------- Creating the users -------------------------------
                await CreateUser(stefan, password);
                await CreateUser(koen, password);
                await CreateUser(jonathan, password);
                await CreateUser(florian, password);

                #endregion

                #region Trainings

                Training t1 = new Training(KindOfTraining.GENERAL, new DateTime());
                Training t2 = new Training(KindOfTraining.CORESTABILITY, new DateTime(DateTime.Now.AddDays(3).Year, DateTime.Now.AddDays(3).Month, DateTime.Now.AddDays(3).Day));
                Training t3 = new Training(KindOfTraining.LEGPOWER, new DateTime(DateTime.Now.AddDays(4).Year, DateTime.Now.AddDays(4).Month, DateTime.Now.AddDays(4).Day));

                Exercise e1 = new Exercise("Squat", 10, "www.image.be", 50);
                Exercise e2 = new Exercise("Squat", 10, "www.image.be", 50);

                #endregion

                _dbContext.SaveChanges();
            }

        }

        private async Task CreateUser(User user, string password)
        {
            await _userManager.CreateAsync(user, password);
        }
    }
}
