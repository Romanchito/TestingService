using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestingService.Models.EntityModels
{
    public class Quest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int TeacherId { get; set; }
        public int Percent_Of_Exelent { get; set; }
        public int Percent_Of_Good { get; set; }
        public int Percent_Of_Satisfactory { get; set; }       
        public virtual User Teacher { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Group> Groups { get; set; }

        
    }
}