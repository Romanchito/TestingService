using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using TestingService.DAL.EFContext;
using TestingService.DAL.Entities;
using TestingService.DAL.Interfaces;

namespace TestingService.DAL.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private Context db;

        public AnswerRepository(Context db)
        {
            this.db = db;
        }

        public void Create(Answer item)
        {
            db.Answers.Add(item);
        }

        public void Delete(int id)
        {
            Answer answer = db.Answers.Find(id);
            db.Answers.Remove(answer);
        }

        public Answer FindAnswerByName(string name)
        {
            Answer answer = db.Answers.FirstOrDefault(x => x.Text_of_answer == name);
            db.Entry(answer).State = EntityState.Detached;
            return answer;
        }

        public IEnumerable<Answer> GetAll()
        {
            IEnumerable <Answer> answers = db.Answers.Include(a => a.Question);
            foreach (var item in answers)
            {
                db.Entry(item).State = EntityState.Detached;
            }
            return answers;
        }

        public IEnumerable<Answer> GetAllByQuestionId(int questionId)
        {
            IEnumerable<Answer> answers = db.Answers.Where(x => x.QuestionId == questionId);
            foreach (var item in answers)
            {
                db.Entry(item).State = EntityState.Detached;
            }
            return answers;            
        }

        public Answer GetById(int? id)
        {
            Answer answer = db.Answers.Find(id);
            db.Entry(answer).State = EntityState.Detached;
            return answer;
        }

        public void Update(Answer item)
        {
            Debug.WriteLine("Answer DEBUG " + item.Text_of_answer + " and " + item.isTrue);
            Answer answer = db.Answers.Find(item.Id);
            answer.isTrue = item.isTrue;
            answer.Text_of_answer = item.Text_of_answer;
        }
    }
}