﻿using System;
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
            return _gymnasts.ToList();
        }


        public Gymnast GetGymnastWithTrainings(string gymnastId) {
            return _gymnasts
                .Include(g => g.Trainings)
                //.Include(g => g.Trainings).ThenInclude(t => t.TrainingExercises)
                .Include(g=> g.Trainings).ThenInclude(t => t.Category)
                .SingleOrDefault(g => g.Id == gymnastId);
        }

        public ICollection<Gymnast> GetGymnastsFromCoach(string coachId)
        {
            return _gymnasts
                .Include(g => g.GymnastCoaches).ThenInclude(gc => gc.Gymnast)
                .Where(g => g.GymnastCoaches.All(gc => gc.CoachId == coachId))
                .ToList();
        }

        

        public void Remove(Gymnast obj)
        {
            _gymnasts.Remove(obj);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Gymnast GetbyIdString(string id) {
            return _gymnasts.SingleOrDefault(g => g.Id == id);

        }

        public Gymnast GetbyId(int id) {
            throw new NotImplementedException();
        }

        public Gymnast GetByEmail(string email) {
            return _gymnasts
                .Include(g => g.Trainings)
                .Include(g=>g.Trainings).ThenInclude(t=>t.Category)
                .SingleOrDefault(g => g.Email == email);
        }
    }
}
