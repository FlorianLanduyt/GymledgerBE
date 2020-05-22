using System;
using System.Collections.Generic;
using System.Linq;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GymLedgerAPI.Data.Repositories
{
    public class ExerciseRepo : IExerciseRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Exercise> _exercises;

        public ExerciseRepo(ApplicationDbContext context)
        {
            _context = context;
            _exercises = context.Excercises;
        }

        public void Add(Exercise obj)
        {
            _context.Add(obj);
        }

        public ICollection<Exercise> GetExercisesFromGymnast(string email) {
             return _exercises
                .Where(t => t.Gymnast.Email == email)
                .ToList();
        }

        public ICollection<Exercise> GetAll()
        {
            return _exercises.ToList();
        }

        public Exercise GetbyId(int id)
        {
            return _exercises.SingleOrDefault(e => e.Id == id);
        }

        public void Remove(Exercise obj)
        {
            _exercises.Remove(obj);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Exercise> GetByName(string email, string name = null) {
            return _exercises.Where(e => e.Gymnast.Email == email && e.Description.ToLower().StartsWith(name.ToLower()));
        }
    }
}
