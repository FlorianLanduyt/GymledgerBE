using System;
using GymLedgerAPI.Data.Mapping;
using GymLedgerAPI.Domain.Models;
using GymLedgerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymLedgerAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        public DbSet<Gymnast> Gymnasts { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        //public DbSet<User> AppUsers { get; set; }
        public DbSet<Exercise> Excercises { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<ExerciseEvaluation> Evaluations { get; set; }
        public DbSet<Category> Categories { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.ApplyConfiguration(new CoachMapper());
            builder.ApplyConfiguration(new ExerciseMapper());
            builder.ApplyConfiguration(new GymnastCoachMapper());
            //builder.ApplyConfiguration(new GymnastMapper());
            builder.ApplyConfiguration(new TrainingMapper());
            builder.ApplyConfiguration(new TrainingExerciseMapper());
            builder.ApplyConfiguration(new ExerciseEvaluationMapper());

        }
    }
}
