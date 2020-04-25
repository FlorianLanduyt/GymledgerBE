//using System;
//using System.Collections.Generic;
//using System.Linq;
//using GymLedgerAPI.Domain.Interfaces;
//using GymLedgerAPI.Models;
//using Microsoft.EntityFrameworkCore;

//namespace GymLedgerAPI.Data.Repositories
//{
//    public class UserRepo : IUserRepo
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly DbSet<User> _users;

//        public UserRepo(ApplicationDbContext context)
//        {
//            _context = context;
//            _users = context.AppUsers;
//        }

//        public void Add(User obj) {
//            _users.Add(obj);
//        }

//        public ICollection<User> GetAll() {
//            return _users.ToList();
//        }

//        public User GetbyIdString(string id) {
//            return _users
//                .SingleOrDefault(u => u.Id == id);
//        }


//        public void Remove(User obj) {
//            _users.Remove(obj);
//        }

//        public void SaveChanges() {
//            _context.SaveChanges();
//        }

//        public User GetbyId(int id) {
//            throw new NotImplementedException();
//        }
//    }
//}
