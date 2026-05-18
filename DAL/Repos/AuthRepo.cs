using BLL.DTOs.Helpers;
using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class AuthRepo
    {
        TenantBridgeContext db;

        public AuthRepo(TenantBridgeContext db)
        {
            this.db = db;
        }

        public List<User> Get()
        {
            return db.Users.ToList();
        }

        public User? Get(int id)
        {
            return db.Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public User? GetByEmail(string email)
        {
            return db.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
        }

        public List<User> GetByRole(int role)
        {
            return db.Users.Where(u => u.Role == role).ToList();
        }

        public User? Login(string email, string password)
        {
            return db.Users.Where(u => u.Email.Equals(email) && u.Password.Equals(AuthHelper.GetMd5(password))).FirstOrDefault();
        }

        public bool Register(User user)
        {
            user.Password = AuthHelper.GetMd5(user.Password);
            db.Users.Add(user);
            return db.SaveChanges() > 0;
        }

        public bool ChangePassword(int id, string oldPassword, string newPassword)
        {
            var user = Get(id);
            if (user == null || !user.Password.Equals(AuthHelper.GetMd5(oldPassword))) return false;
            user.Password = AuthHelper.GetMd5(newPassword);
            return Update(user);
        }

        public bool Update(User user)
        {
            var data = Get(user.Id);

            if(data == null)
                return false;
            
            db.Entry(data).CurrentValues.SetValues(user);
            return db.SaveChanges() > 0;
        }
    }
}
