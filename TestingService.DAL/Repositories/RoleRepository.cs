using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.DAL.EFContext;
using TestingService.DAL.Entities;
using TestingService.DAL.Interfaces;

namespace TestingService.DAL.Repositories
{
    public class RoleRepository : IRepository<Role>
    {
        private Context db;

        public RoleRepository(Context db)
        {
            this.db = db;
        }

        public void Create(Role item)
        {
            db.Roles.Add(item);
        }

        public void Delete(int id)
        {
            Role role = db.Roles.Find(id);
            if (role != null)
            {
                db.Roles.Remove(role);
            }
        }

        public Role GetById(int? id)
        {
            Role role = db.Roles.Find(id);
            db.Entry(role).State = EntityState.Detached;
            return role;
        }

        public IEnumerable<Role> GetAll()
        {
            IEnumerable<Role> roles = db.Roles;
            foreach (var item in roles)
            {
                db.Entry(item).State = EntityState.Detached;
            }
            return roles;
        }

        public void Update(Role item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
