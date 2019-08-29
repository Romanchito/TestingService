using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TestingService.BLL.DTO;
using TestingService.BLL.Interfaces;
using TestingService.DAL.Entities;
using TestingService.DAL.Interfaces;

namespace TestingService.BLL.Services
{
    public class GroupService : IGroupService, IDisposable
    {
        private IUnitOfWork Database { get; set; }

        public GroupService(IUnitOfWork database)
        {
            Database = database;
        }

        public void Create(GroupDTO questDTO)
        {
            Group group = new Group();
            group.Name = questDTO.Name;
            Database.Groups.Create(group);
            Database.Save();
        }

        public IEnumerable<GroupDTO> GetAll() 
        {            
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Group, GroupDTO>()).CreateMapper();
                return mapper.Map<IEnumerable<Group>, List<GroupDTO>>(Database.Groups.GetAll());       
           
        }

        public GroupDTO GetById(int? id)
        {
            if (id == null) return null;
            var quest = Database.Groups.GetById(id);
            if (quest == null) throw new Exception("Задание не найдено");
            return new GroupDTO { Id = quest.Id, Name = quest.Name };
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Update(GroupDTO item)
        {
            Group group = new Group { Id = item.Id, Name = item.Name };

            Database.Groups.Update(group);
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.Groups.Delete(id);
            Database.Save();
        }

        public IEnumerable<GroupDTO> GetGroupsByQuestId(int questId)
        {
            return Mapper.Map<IEnumerable<Group>, List<GroupDTO>>(Database.Groups.GetGroupsByQuestId(questId));
        }
    }
}