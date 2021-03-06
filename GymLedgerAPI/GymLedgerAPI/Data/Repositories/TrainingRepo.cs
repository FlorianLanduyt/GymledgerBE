﻿using System;
using System.Collections.Generic;
using System.Linq;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GymLedgerAPI.Data.Repositories
{
    public class TrainingRepo : ITrainingRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Training> _trainings;

        public TrainingRepo(ApplicationDbContext context)
        {
            _context = context;
            _trainings = context.Trainings;
        }

        public void Add(Training obj)
        {
            _trainings.Add(obj);
        }

        public ICollection<Training> GetAll()
        {
            return _trainings.ToList();
        }

        public ICollection<Training> GetAllTrainingsFromGymnast(string email) {
            return _trainings
                .Include(t => t.Category)
                .Where(t => t.Gymnast.Email == email).ToList();
        }

        public Training GetbyId(int id)
        {
            return _trainings
                .Include(t => t.Category)
                .Include(t => t.TrainingExercises).ThenInclude(t => t.Exercise)
                .SingleOrDefault(t => t.Id == id);
        }

        

        public void Remove(Training obj)
        {
            _trainings.Remove(obj);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
