using System.Collections.Generic;
using TestingService.DAL.Source;

namespace TestingService.BLL.DTO
{
    public class ResultDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestId { get; set; }
        public int Mark { get; set; }
        public string Date { get; set; }
        public double Percente { get; set; }
        public List<AnswerInfo> Answers { get; set; }

        
    }
}