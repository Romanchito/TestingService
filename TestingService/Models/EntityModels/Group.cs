using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestingService.Models.EntityModels
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Quest> Quests { get; set; }          
        
    }
}