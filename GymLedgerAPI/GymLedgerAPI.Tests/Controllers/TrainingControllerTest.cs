using System;
using GymLedgerAPI.Controllers;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.DTOs;
using GymLedgerAPI.Models;
using GymLedgerAPI.Tests.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GymLedgerAPI.Tests.Controllers {
    public class TrainingControllerTest {

        private Mock<IGymnastRepo> _gymnastsRepo;
        private readonly Mock<ITrainingRepo> _trainingRepo;
        private readonly Mock<ICategoryRepo> _categoryRepo;

        private readonly TrainingController _controller;
        private readonly DummyDbContext _context;

        public TrainingControllerTest() {
            _gymnastsRepo = new Mock<IGymnastRepo>();
            _trainingRepo = new Mock<ITrainingRepo>();
            _categoryRepo = new Mock<ICategoryRepo>();

            _controller = new TrainingController(_gymnastsRepo.Object, _trainingRepo.Object, _categoryRepo.Object);
            _context = new DummyDbContext();
        }

        #region get
        [Fact]
        public void GetById_ExistingId_GivesTraining() {
            int trainingId = 1;
            _trainingRepo.Setup(t => t.GetbyId(trainingId)).Returns(_context.T1);

            ActionResult<Training> actionResult = _controller.GetTraining(trainingId);
            var response = actionResult?.Result as OkObjectResult;

            Training trainingFromResponse = response?.Value as Training;

            Assert.Equal(_context.C1, trainingFromResponse.Category);

        }

        [Fact]
        public void GetById_NotExistingId_ReturnsNotFound() {
            int trainingId = 8;
            _trainingRepo.Setup(t => t.GetbyId(trainingId)).Returns((Training)null);

            ActionResult<Training> actionResult = _controller.GetTraining(trainingId);

            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
        #endregion

        #region Add Training

        //[Fact]
        //public void AddActivity_Succeeds() {
        //    string email = "florian.landuyt@hotmail.com";

        //    TrainingDTO trainingDTO = new TrainingDTO() {
        //        CategoryId = 1,
        //        Date = new DateTime(),
        //        FeelingBeforeTraining = 5
        //    };



        //    ActionResult<Training> actionResult = _controller.CreateNewTraining(trainingDTO, email);
        //    var response = actionResult.Result as OkObjectResult;
        //    Training newTraining = response?.Value as Training;

        //    Assert.Equal(200, response?.StatusCode);
        //    Assert.Equal(5, newTraining.FeelingBeforeTraining);

        //    _trainingRepo.Verify(t => t.Add(It.IsAny<Training>()), Times.Once());
        //    _trainingRepo.Verify(t => t.SaveChanges(), Times.Once());
        //}

        #endregion



    }
}
