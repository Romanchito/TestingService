using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TestingService.BLL.DTO;

namespace TestingService.Models.ViewModels
{
    public class ViewQuestModel
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Длина имени должна быть от 5 до 30 символов")]
        [Remote("CheckQuestName", "Test" ,"Conrollers")]
        [Display(Name = "Наименование")]
        [Required]
        public string Name { get; set; }
        public string Type { get; set; }
        public int? GroupId { get; set; }
        public bool LogicDelete { get; set; }
        public int? TeacherId { get; set; }        
        [Display(Name = "Процент на 'отлично'")]
        [Required]
        public int Percent_Of_Exelent { get; set; }
        [Display(Name = "Процент на 'отлично'")]
        [Required]
        public int Percent_Of_Good { get; set; }
        [Display(Name = "Процент на 'отлично'")]
        [Required]
        public int Percent_Of_Satisfactory { get; set; }
        public UserDTO Teacher { get; set; }        
        public GroupDTO Group { get; set; }
    }
}