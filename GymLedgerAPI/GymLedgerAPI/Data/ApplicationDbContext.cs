using System;
using GymLedgerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymLedgerAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Gymnast> Gymnasts { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Exercise> Excercises { get; set; }
        public DbSet<Training> Trainings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
