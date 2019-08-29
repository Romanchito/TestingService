using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.DAL.Entities;

namespace TestingService.DAL.Interfaces
{
    public interface IUserRepository : IRepository<User> 
    {
        User FindByEmail(string email);
        IEnumerable<User> FindUsersByRole(string roleName);
    }
}
