using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.DAL.Entities;

namespace TestingService.DAL.EFContext
{
    class DbInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context db)
        {
            db.Groups.Add(new Group { Id = 1, Name = "test" });
            db.Roles.Add(new Role { Id = 1, Name = "admin" });
            db.Roles.Add(new Role { Id = 2, Name = "teacher" });
            db.Roles.Add(new Role { Id = 3, Name = "student" });
            db.Users.Add(new User
            {
                Email = "rstsar@yandex.ru",
                Password = "123456",
                Name = "Roman",
                Surname = "Tretyakov",
                Patronomic = "Sergeevich",
                RoleId = 1
            });

            db.Users.Add(new User
            {
                Email = "prepod@yandex.ru",
                Password = "123456",
                Name = "Andrey",
                Surname = "Molotov",
                Patronomic = "Igorevich",
                RoleId = 2
            });

            db.Users.Add(new User
            {
                Email = "student@yandex.ru",
                Password = "123456",
                Name = "Erzan",
                Surname = "Pupkin",
                Patronomic = "Sergeevich",
                RoleId = 3
            });

            base.Seed(db);
        }

    }
}
