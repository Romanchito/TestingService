using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestingService.Models.EntityModels
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}