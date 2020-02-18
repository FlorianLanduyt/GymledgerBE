using System;
using System.Collections.Generic;
using System.Linq;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GymLedgerAPI.Data.Repositories
{
    public class CoachRepo : ICoachRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Coach> _coaches;

        public CoachRepo(ApplicationDbContext context)
        {
            _context = context;
            _coaches = context.Coaches;
        }

        public void Add(Coach obj)
        {
            _coaches.Add(obj);
        }

        public ICollection<Coach> GetAll()
        {
            return _coaches.Include(g => g.GymnastCoaches).ToList();
        }

        public Coach GetbyId(int id)
        {
            return _coaches
                .Include(c => c.GymnastCoaches)
                .SingleOrDefault(c => c.Id == id);
        }

        public void Remove(Coach obj)
        {
            _coaches.Remove(obj);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
