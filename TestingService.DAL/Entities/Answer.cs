using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingService.DAL.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public bool isTrue { get; set; }
        public string Text_of_answer { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public Answer()
        {
        }

        public Answer(int questionId)
        {
            QuestionId = questionId;
        }
    }
}
