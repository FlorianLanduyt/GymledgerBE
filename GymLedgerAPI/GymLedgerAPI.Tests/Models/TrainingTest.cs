using System;
using GymLedgerAPI.Models;
using GymLedgerAPI.Tests.Data;
using Xunit;

namespace GymLedgerAPI.Tests.Models {
    public class TrainingTest {
        private readonly DummyDbContext _context;

        public TrainingTest(DummyDbContext context) {
            _context = context;
        }

        [Fact]
        public void Gymnast_ValidContructor_CreatesObject() {
            Training testTraining = _context.T1;


            Assert.Equal(7, testTraining.FeelingAfterTraining);
            
        }

    }
}
