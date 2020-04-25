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

        public ICollection<ExerciseEvaluation> GetAllFromTraining(int trainingId) {
            return _exerciseEvaluations.Where(ee => ee.Training.Id == trainingId).ToList();
        }

        public ExerciseEvaluation GetbyId(int id)
        {
            return _exerciseEvaluations
                .Include(e => e.Training)
                .Include(e => e.Exercise)
                .SingleOrDefault(e => e.Id == id);
        }

        public ExerciseEvaluation GetEvaluationFromExerciseInTraining(int trainingId, int exerciseId) {
            return _exerciseEvaluations
                .SingleOrDefault(ee => ee.Training.Id == trainingId && ee.Exercise.Id == exerciseId);
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
