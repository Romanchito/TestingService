using AutoMapper;
using System;
using System.Collections.Generic;
using TestingService.BLL.DTO;
using TestingService.BLL.Infrastructure;
using TestingService.BLL.Interfaces;
using TestingService.DAL.Entities;
using TestingService.DAL.Interfaces;

namespace TestingService.BLL.Services
{
    public class QuestService : IQuestService, IDisposable
    {
        private IUnitOfWork Database { get; set; }

        public QuestService(IUnitOfWork database)
        {
            Database = database;
        }

        public void Create(QuestDTO questDTO)
        {
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<QuestDTO, Quest>()).CreateMapper();            
            Database.Quests.Create(Mapper.Map<QuestDTO, Quest>(questDTO));
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public QuestDTO GetById(int? id)
        {
            Quest quest = Database.Quests.GetById(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Quest, QuestDTO>()).CreateMapper();
            QuestDTO questDTO = mapper.Map<Quest, QuestDTO>(quest);
            return questDTO;
        }

        public QuestDTO GetQuestByName(string name)
        {
            Quest quest = Database.Quests.FindByName(name);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Quest, QuestDTO>()).CreateMapper();
            QuestDTO questDTO = mapper.Map<Quest, QuestDTO>(quest);
            return questDTO;
        }

        public IEnumerable<QuestDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Quest, QuestDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Quest>, List<QuestDTO>>(Database.Quests.GetAll());
        }

        public void Update(QuestDTO item)
        {
            
            Database.Quests.Update(Mapper.Map<QuestDTO, Quest>(item));
            Database.Save();
            Database.Dispose();
        }

        public void Delete(int id)
        {
            Database.Quests.Delete(id);
            Database.Save();
        }

        public IEnumerable<QuestDTO> FindQuestsByTeacher(string name)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Quest, QuestDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Quest>, List<QuestDTO>>(Database.Quests.FindQuestsByTeacher(name));
        }

        public IEnumerable<QuestDTO> FindQuestsByGroup(string name)
        {

            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Quest, QuestDTO>()).CreateMapper();
            return Mapper.Map<IEnumerable<Quest>, List<QuestDTO>>(Database.Quests.FindQuestsByGroup(name));
        }
    }
}