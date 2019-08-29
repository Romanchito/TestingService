using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestingService.DAL.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public int? RoleId { get; set; }        
        public int? GroupId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronomic { get; set; }
        public virtual Role Role { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }
        public virtual ICollection<Quest> Quests { get; set; }      
    }
}