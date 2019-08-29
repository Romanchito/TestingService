using System.Collections.Generic;
using TestingService.BLL.DTO;

namespace TestingService.Models.CreatorsModels
{
    public class AnswerModel
    {
        public int idQuest { get; set; }
        public string Type { get; set; }
        public string result { get; set; }
        public List<QuestionDTO> questions { get; set; }
        public List<AnswerDTO> results { get; set; }       
    }
}