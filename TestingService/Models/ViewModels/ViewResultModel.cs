using System.Collections.Generic;
using TestingService.DAL.Source;

namespace TestingService.Models.ViewModels
{
    public class ViewResultModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestId { get; set; }
        public int Mark { get; set; }
        public string Date { get; set; }
        public double Percente { get; set; }
        public List<AnswerInfo> Answers { get; set; }

        public int GetCountOfTrueAnswers()
        {
            int count = 0;
            foreach (var item in Answers)
            {
                if (item.isTrue) count++;
            }
            return count;
        }
    }
}