using System.Data.Entity;
using TestingService.DAL.Entities;
using TestingService.DAL.Source;

namespace TestingService.DAL.EFContext
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Quest> Quests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<AnswerInfo> AnswersInfo { get; set; }
        public DbSet<Image> Images { get; set; }

        static Context()
        {
            Database.SetInitializer(new DbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Group>().HasMany(p => p.Users).WithOptional(p => p.Group);
           

            base.OnModelCreating(modelBuilder);
        }

        public Context(string connectionString) : base(connectionString)
        {
        }
    }
}