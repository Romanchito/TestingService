using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestingService.Models.EntityModels
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronomic { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Quest> Quests { get; set; }
    }
}