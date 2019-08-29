using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestingService.Models.ViewModels
{
    public class ViewImageModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Наименование")]
        public string Name { get; set; }
        public byte[] Data { get; set; }        
    }
}