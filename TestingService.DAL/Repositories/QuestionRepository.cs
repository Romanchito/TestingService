using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using TestingService.DAL.EFContext;
using TestingService.DAL.Entities;
using TestingService.DAL.Interfaces;

namespace TestingService.DAL.Repositories
{
    public class QuestionRepository : IQuestionRepositiry
    {
        private Context db;

        public QuestionRepository(Context db)
        {
            this.db = db;
        }

        public void Create(Question item)
        {
            db.Questions.Add(item);
        }

        public void Delete(int id)
        {
            Question question = db.Questions.Find(id);
            if(question != null)
            {
                db.Questions.Remove(question);
            }            
        }

        public Question findByName(string name)
        {
            Question question = db.Questions.FirstOrDefault(x=>x.Text_of_question.Equals(name));
            db.Entry(question).State = EntityState.Detached;
            return question;            
        }

        public IEnumerable<Question> GetAll()
        {
            IEnumerable<Question> questions = db.Questions.Include(t => t.Quest);
            foreach (var item in questions)
            {
                db.Entry(item).State = EntityState.Detached;
            }
            return questions;            
        }

        public IEnumerable<Question> GetAllByQuestId(int id)
        {
            IEnumerable <Question> questions = db.Questions.Where(x => x.QuestId == id);
            foreach (var item in questions)
            {
                db.Entry(item).State = EntityState.Detached;
            }
            return questions;
        }

        public Question GetById(int? id)
        {
            Question question = db.Questions.Find(id);
            db.Entry(question).State = EntityState.Detached;
            return question;
        }

        public void Update(Question item)
        {
            Question question = db.Questions.Find(item.Id);
            question.EntryType = item.EntryType;
            question.ImageId = item.ImageId;
            question.Text_of_question = item.Text_of_question;           
        }
    }
}
