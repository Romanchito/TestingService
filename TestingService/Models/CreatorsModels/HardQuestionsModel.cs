using System.Collections.Generic;

namespace TestingService.Models.CreatorsModels
{
    public class HardQuestionsModel
    {
        public HardQuestionsModel(List<string> TrueQuestions, List<string> FalseQuestions, double[] TrueStatus, double[] FalseStatus)
        {
            this.TrueQuestions = TrueQuestions;
            this.FalseQuestions = FalseQuestions;
            this.TrueStatus = TrueStatus;
            this.FalseStatus = FalseStatus;
        }

        public List<string> TrueQuestions { get; set; }
        public List<string> FalseQuestions { get; set; }
        public double[] TrueStatus { get; set; }
        public double[] FalseStatus { get; set; }
    }
}