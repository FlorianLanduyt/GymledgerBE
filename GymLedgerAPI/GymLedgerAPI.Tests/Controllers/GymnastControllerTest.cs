using System;
using GymLedgerAPI.Controllers;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.Tests.Data;
using Moq;

namespace GymLedgerAPI.Tests.Controllers {
    public class GymnastControllerTest {
        private Mock<IGymnastRepo> _gymnastsRepo;
        private readonly GymnastController _controller;
        private readonly DummyDbContext _context;

        public GymnastControllerTest() {
            _gymnastsRepo = new Mock<IGymnastRepo>();
            _controller = new GymnastController(_gymnastsRepo.Object);
            _context = new DummyDbContext();
        }

        #region get


        #endregion
    }
}
