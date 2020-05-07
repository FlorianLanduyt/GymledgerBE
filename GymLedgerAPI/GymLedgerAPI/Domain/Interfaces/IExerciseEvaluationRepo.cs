using System;
using System.Collections.Generic;
using GymLedgerAPI.Domain.Models;

namespace GymLedgerAPI.Domain.Interfaces
{
    public interface IExerciseEvaluationRepo : IGenericRepo<ExerciseEvaluation>
    {
        ICollection<ExerciseEvaluation> GetAllFromTraining(int trainingId);
        ExerciseEvaluation GetEvaluationFromExerciseInTraining(int trainingId, int exerciseId);
    }
}
