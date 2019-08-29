using System.Collections.Generic;

namespace TestingService.DAL.Entities
{
    public class Quest
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public int? TeacherId { get; set; }
        public int? GroupId { get; set; }
        public bool LogicDelete { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Percent_Of_Exelent { get; set; }
        public int Percent_Of_Good { get; set; }
        public int Percent_Of_Satisfactory { get; set; }
        public virtual User Teacher { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
             
    }
}