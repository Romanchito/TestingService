using AutoMapper;
using System.Collections.Generic;
using TestingService.BLL.DTO;
using TestingService.BLL.Interfaces;
using TestingService.DAL.Entities;
using TestingService.DAL.Interfaces;
using System;

namespace TestingService.BLL.Services
{
    public class AnswerService : IAnswerService
    {
        private IUnitOfWork Database { get; set; }

        public AnswerService(IUnitOfWork database)
        {
            Database = database;
        }

        public void Create(AnswerDTO item)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AnswerDTO, Answer>()).CreateMapper();
            Database.Answers.Create(mapper.Map<AnswerDTO, Answer>(item));
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.Answers.Delete(id);
            Database.Save();
        }

        public AnswerDTO FindAnswerByName(string name)
        {
            Answer answer = Database.Answers.FindAnswerByName(name);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Answer, AnswerDTO>()).CreateMapper();
            AnswerDTO answerDTO = (mapper.Map<Answer, AnswerDTO>(answer));
            return answerDTO;
        }

        public IEnumerable<AnswerDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Answer, AnswerDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Answer>, List<AnswerDTO>>(Database.Answers.GetAll());
        }

        public AnswerDTO GetById(int? id)
        {
            Answer answer = Database.Answers.GetById(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Answer, AnswerDTO>()).CreateMapper();
            AnswerDTO answerDTO = (mapper.Map<Answer, AnswerDTO>(answer));
            return answerDTO;
        }

        public void Update(AnswerDTO item)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AnswerDTO, Answer>()).CreateMapper();
            Database.Answers.Update(mapper.Map<AnswerDTO, Answer>(item));
            Database.Save();
        }

        public IEnumerable<AnswerDTO> GetAllByQuestionId(int questionId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Answer, AnswerDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Answer>, List<AnswerDTO>>(Database.Answers.GetAllByQuestionId(questionId));
        }
    }
}