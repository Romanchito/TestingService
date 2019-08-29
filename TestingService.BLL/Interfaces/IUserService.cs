using System.Collections.Generic;
using TestingService.BLL.DTO;

namespace TestingService.BLL.Interfaces
{
    public interface IUserService : IService<UserDTO>
    {
        UserDTO GetUserByEmail(string email);
        IEnumerable<UserDTO> FindUsersByRole(string roleName);
    }
}