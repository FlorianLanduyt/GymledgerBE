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
        private readonly DbSet<Training> _trainingen;

        public ExerciseRepo(ApplicationDbContext context)
        {
            _context = context;
            _exercises = context.Excercises;
            _trainingen = context.Trainings;
        }

        public void Add(Exercise obj)
        {
            _context.Add(obj);
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
    }
}
