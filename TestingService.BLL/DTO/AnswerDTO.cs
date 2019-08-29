namespace TestingService.BLL.DTO
{
    public class AnswerDTO
    {
        public int Id { get; set; }
        public bool isTrue { get; set; }
        public string Text_of_answer { get; set; }
        public int QuestionId { get; set; }
        public QuestionDTO Question { get; set; }

        public AnswerDTO()
        {
        }

        public AnswerDTO(int questionId)
        {
            QuestionId = questionId;
        }
    }
}