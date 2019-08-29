using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestingService.Models.CreatorsModels
{
    public class QuestionnaireStatisticModel
    {
        public QuestionnaireStatisticModel(int[] status, List<string> answers , string question)
        {
            this.answers = answers;
            this.question = question;
            this.status = status;
        }

        public int[] status { get; set; }
        public List<string> answers { get; set; }
        public string question { get; set; }
    }
}