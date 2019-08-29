using System.Collections.Generic;

namespace TestingService.BLL.DTO
{
    public class QuestDTO
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public int? TeacherId { get; set; }
        public bool LogicDelete { get; set; }
        public int? GroupId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Percent_Of_Exelent { get; set; }
        public int Percent_Of_Good { get; set; }
        public int Percent_Of_Satisfactory { get; set; }
        public UserDTO Teacher { get; set; }
        public ICollection<QuestionDTO> Questions { get; set; }
        public GroupDTO Group { get; set; }
    }
}
