using System;
using GymLedgerAPI.Domain.Models;

namespace GymLedgerAPI.Domain.Interfaces
{
    public interface IExerciseEvaluationRepo : IGenericRepo<ExerciseEvaluation>
    {

        ExerciseEvaluation GetByTwoIds(long trainingId, long exerciseId);
    }
}
