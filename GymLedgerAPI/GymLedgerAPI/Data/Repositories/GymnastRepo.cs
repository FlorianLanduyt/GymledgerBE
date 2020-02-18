using System;
using System.Collections.Generic;
using System.Linq;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GymLedgerAPI.Data.Repositories
{
    public class GymnastRepo : IGymnastRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Gymnast> _gymnasts;

        public GymnastRepo(ApplicationDbContext context)
        {
            _context = context;
            _gymnasts = context.Gymnasts;
        }

        public void Add(Gymnast obj)
        {
            _gymnasts.Add(obj);
        }

        public ICollection<Gymnast> GetAll()
        {
            return _gymnasts.Include(g => g.GymnastCoaches).ToList();
        }

        public Gymnast GetbyId(int id)
        {
            return _gymnasts.SingleOrDefault(g => g.Id == id);
        }

        public void Remove(Gymnast obj)
        {
            _gymnasts.Remove(obj);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
