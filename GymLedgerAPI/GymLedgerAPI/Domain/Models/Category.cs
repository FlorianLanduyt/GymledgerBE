using System;
namespace GymLedgerAPI.Models
{
    public class Category
    {
        //GENERAL = 0,
        //LEGPOWER = 1,
        //SCHOULDERPOWER = 2,
        //CORESTABILITY = 3,
        //BACKPOWER = 4

        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public Category(string name, string description) {
            Description = description;
            Name = name;
        }

        protected Category() {

        }
    }
}
