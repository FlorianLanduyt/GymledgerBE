using System;
using System.Collections.Generic;
using System.Linq;
using GymLedgerAPI.Domain.Interfaces;
using GymLedgerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GymLedgerAPI.Data.Repositories {
    public class CategoryRepo : ICategoryRepo {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Category> _categories;

        public CategoryRepo(ApplicationDbContext context) {
            _context = context;
            _categories = context.Categories;
        }

        public void Add(Category obj) {
            _categories.Add(obj);
        }

        public ICollection<Category> GetAll() {
            return _categories.ToList();
        }

        public Category GetbyId(int id) {
            return _categories.FirstOrDefault(c => c.Id == id);
        }

        public void Remove(Category obj) {
            _categories.Remove(obj);
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
}
