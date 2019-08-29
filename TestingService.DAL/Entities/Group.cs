using System.Collections.Generic;

namespace TestingService.DAL.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Quest> Quests { get; set; }
        
    }
}