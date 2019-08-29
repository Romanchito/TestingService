using System.Collections.Generic;
using TestingService.DAL.Source;

namespace TestingService.DAL.Entities
{
    public class Result
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestId { get; set; }
        public int Mark { get; set; }
        public string Date { get; set; }
        public double Percente { get; set; }
        public virtual ICollection<AnswerInfo> Answers { get; set; }        
    }
}