using System;
namespace GymLedgerAPI.Models
{
    public class Category
    {
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
