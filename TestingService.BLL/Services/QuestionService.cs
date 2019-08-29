using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.DTO;
using TestingService.BLL.Interfaces;
using TestingService.DAL.Entities;
using TestingService.DAL.Interfaces;

namespace TestingService.BLL.Services
{
    public class QuestionService : IQuestionService , IDisposable
    {
        private IUnitOfWork Database { get; set; }

        public QuestionService(IUnitOfWork database)
        {
            Database = database;
        }

        public void Create(QuestionDTO item)
        {                     
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<QuestionDTO, Question>()).CreateMapper();
            Database.Questions.Create(mapper.Map<QuestionDTO, Question>(item));
            Database.Save();
        }

        public void Delete(int id)
        {
            Database.Questions.Delete(id);
            Database.Save();
        }

        public IEnumerable<QuestionDTO> GetAll()
        {           
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Question, QuestionDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Question>, List<QuestionDTO>>(Database.Questions.GetAll());
        }

        public QuestionDTO GetById(int? id)
        {
            Question question = Database.Questions.GetById(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Question, QuestionDTO>()).CreateMapper();
            QuestionDTO questDTO = mapper.Map<Question, QuestionDTO>(question);
            return questDTO;
        }

        public QuestionDTO GetByName(string name)
        {
            Question question = Database.Questions.findByName(name);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Question, QuestionDTO>()).CreateMapper();
            QuestionDTO questDTO = mapper.Map<Question, QuestionDTO>(question);
            return questDTO;
        }

        public void Update(QuestionDTO item)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<QuestionDTO, Question>()).CreateMapper();
            Database.Questions.Update(mapper.Map<QuestionDTO, Question>(item));
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public IEnumerable<QuestionDTO> GetAllByQuestId(int id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Question, QuestionDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Question>, List<QuestionDTO>>(Database.Questions.GetAllByQuestId(id));
        }
    }
}
