using AutoMapper;
using System;
using System.Collections.Generic;
using TestingService.BLL.DTO;
using TestingService.BLL.Interfaces;
using TestingService.DAL.Entities;
using TestingService.DAL.Interfaces;

namespace TestingService.BLL.Services
{
    public class UserService : IUserService, IDisposable
    {
        private IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork database)
        {
            Database = database;
        }

        public void Create(UserDTO userDTO)
        {
            User user = Mapper.Map<UserDTO, User>(userDTO);           
            Database.Users.Create(user);
            Database.Save();
        }

        public UserDTO GetById(int? id)
        {
            if (id == null) return null;
            return Mapper.Map<User, UserDTO>(Database.Users.GetById(id));
        }

        public IEnumerable<UserDTO> GetAll()
        {
            IEnumerable<UserDTO> userDtoList = new List<UserDTO>();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(Database.Users.GetAll());
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public UserDTO GetUserByEmail(string email)
        {
            var user = Database.Users.FindByEmail(email);
            if (user == null) return null;
            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Name = user.Name,
                Patronomic = user.Patronomic,
                Surname = user.Surname,
                RoleId = user.RoleId,
                Role = Mapper.Map<Role, RoleDTO>(user.Role),
                Group = Mapper.Map<Group, GroupDTO>(user.Group)
            };
        }

        public void Update(UserDTO item)
        {
            User user = Mapper.Map<UserDTO, User>(item);
            Database.Users.Update(user);
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.Users.Delete(id);
            Database.Save();
        }

        public IEnumerable<UserDTO> FindUsersByRole(string roleName)
        {
            IEnumerable<UserDTO> userDtoList = new List<UserDTO>();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(Database.Users.FindUsersByRole(roleName));
        }
    }
}