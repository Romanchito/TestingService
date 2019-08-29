using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingService.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? GroupId  { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronomic { get; set; }
        public GroupDTO Group { get; set; }
        public RoleDTO Role { get; set; }
        
    }
}
