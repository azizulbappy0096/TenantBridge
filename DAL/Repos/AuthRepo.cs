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

        public User? Login(string email, string password)
        {
            return db.Users.Where(u => u.Email.Equals(email) && u.Password.Equals(password)).FirstOrDefault();
        }

        public bool Register(User user)
        {
            db.Users.Add(user);
            return db.SaveChanges() > 0;
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
