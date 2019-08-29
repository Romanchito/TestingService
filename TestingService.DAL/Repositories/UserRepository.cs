using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using TestingService.DAL.EFContext;
using TestingService.DAL.Entities;
using TestingService.DAL.Interfaces;

namespace TestingService.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private Context db;

        public UserRepository(Context db)
        {
            this.db = db;
        }

        public void Create(User item)
        {
            Debug.WriteLine("GROUPID " + item.GroupId);
            db.Users.Add(item);
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
            }
        }

        public User GetById(int? id)
        {
            User user = db.Users.Where(x=>x.Id == id).Include(r=>r.Role).Include(g=>g.Group).SingleOrDefault();
            //db.Entry(user).State = EntityState.Detached;
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            IEnumerable<User> users = db.Users.Include(r => r.Role);
            foreach (var item in users)
            {
                db.Entry(item).State = EntityState.Detached;
            }
            return users;
        }

        public void Update(User item)
        {
            Debug.WriteLine("GROUPID " + item.GroupId);
            User user = db.Users.Find(item.Id);
            user.Id = item.Id;
            user.Name = item.Name;
            user.Surname = item.Surname;
            user.Patronomic = item.Patronomic;
            user.Email = item.Email;
            user.Password = item.Password;
            user.RoleId = item.RoleId;
            user.GroupId = item.GroupId;
        }

        public User FindByEmail(string email)
        {
            User user = db.Users.FirstOrDefault(x => x.Email == email);
            //db.Entry(user).State = EntityState.Detached;
            return user;
        }

        public IEnumerable<User> FindUsersByRole(string roleName)
        {            
            IEnumerable<User> users = db.Users.Where(u => u.Role.Name.Equals(roleName)).Include(r => r.Role).Include(g => g.Group);                      
            foreach (var item in users)
            {
                db.Entry(item).State = EntityState.Detached;
            }
            return users;
        }
    }
}