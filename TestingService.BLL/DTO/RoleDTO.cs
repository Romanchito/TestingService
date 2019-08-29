using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingService.BLL.DTO
{
    public class RoleDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<UserDTO> Users { get; set; }
    }
}
