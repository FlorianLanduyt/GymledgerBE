using System;
using GymLedgerAPI.Models;
using Xunit;

namespace GymLedgerAPI.Tests.Models {
    public class GymnastTest {

        [Fact]
        public void Gymnast_ValidContructor_CreatesObject() {
            Gymnast testGymnast = new Gymnast("Florian", "Landuyt", new DateTime(1996, 05, 10), "florian.landuyt@hotmail.com");


            Assert.Equal("Florian", testGymnast.Firstname);
            Assert.Equal("Landuyt", testGymnast.Lastname);
            Assert.Equal("florian.landuyt@hotmail.com", testGymnast.Email);
        }


    }
}
