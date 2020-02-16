using System;
using System.Collections.Generic;
using System.Linq;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace GymLedgerAPI.Data.Repositories
{
    public class ExerciseEvaluationRepo : IExerciseEvaluationRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ExerciseEvaluation> _exerciseEvaluations;

        public ExerciseEvaluationRepo(ApplicationDbContext context)
        {
            _context = context;
            _exerciseEvaluations = context.Evaluations;
        }

        public void Add(ExerciseEvaluation obj)
        {
            _exerciseEvaluations.Add(obj);
        }

        public ICollection<ExerciseEvaluation> GetAll()
        {
            return _exerciseEvaluations.ToList();
        }

        public ExerciseEvaluation GetbyId(long id)
        {
            return _exerciseEvaluations.SingleOrDefault(e => e.Id == id);
        }


        public void Remove(ExerciseEvaluation obj)
        {
            _exerciseEvaluations.Remove(obj);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
