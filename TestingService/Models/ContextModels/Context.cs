using System.Data.Entity;
using TestingService.Models.EntityModels;

namespace TestingService.Models.ContextModels
{
    public class Context : DbContext
    {
        public Context() : base("ConnectionString")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }      
    }
}