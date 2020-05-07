using System;
using GymLedgerAPI.Models;

namespace GymLedgerAPI.Tests.Data {
    public class DummyDbContext {

        #region Gymnast
        public Gymnast G1 { get; }
        public Gymnast G2 { get; }
        public Gymnast NewGymnast { get; }

        public Gymnast[] Gymnasts { get; }
        #endregion

        #region Trainings
        public Training T1 { get; }
        public Training T2 { get; }
        public Training T3 { get; }
        public Training NewTraining { get; set; }

        public Training[] Trainings { get; }
        #endregion

        #region Categories
        public Category C1 { get; }
        public Category C2 { get; }
        public Category C3 { get; }
        public Category[] Categories { get; }
        #endregion


        public DummyDbContext() {

            #region init Gymnasts
            G1 = new Gymnast("Florian", "Landuyt", new DateTime(1996, 05, 10), "florian.landuyt@hotmail.com");
            G2 = new Gymnast("Jonathan", "Vrolix", new DateTime(1996, 11, 09), "jonathan.vrolix@hotmail.com");

            Gymnasts = new[] { G1, G2 };

            NewGymnast = new Gymnast("Alex", "Pieters", new DateTime(1996, 12, 20), "alex.pieters@hotmail.com");
            #endregion


            #region init Categories
            C1 = new Category("General", "");
            C2 = new Category("Legpower", "");
            C3 = new Category("Corestability", "");

            Categories = new[] { C1, C2, C3 };
            #endregion


            #region init Trainings
            int trainingId = 1;

            T1 = new Training(C1, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), 5, 7) { Id = trainingId };

            T2 = new Training(C3, new DateTime(DateTime.Now.AddDays(3).Year, DateTime.Now.AddDays(3).Month, DateTime.Now.AddDays(3).Day), 4, 8) { Id = trainingId++};
            T3 = new Training(C2, new DateTime(DateTime.Now.AddDays(4).Year, DateTime.Now.AddDays(4).Month, DateTime.Now.AddDays(4).Day)) { Id = trainingId + 2};

            Trainings = new[] { T1, T2, T3 };

            NewTraining = new Training(C2, new DateTime(DateTime.Now.AddDays(5).Year, DateTime.Now.AddDays(5).Month, DateTime.Now.AddDays(5).Day), 9, 9) { Id = trainingId + 3};


            #endregion


        }
    }
}
