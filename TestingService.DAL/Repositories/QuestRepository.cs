using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using TestingService.DAL.EFContext;
using TestingService.DAL.Entities;
using TestingService.DAL.Interfaces;

namespace TestingService.DAL.Repositories
{
    public class QuestRepository : IQuestRepository
    {
        private Context db;

        public QuestRepository(Context db)
        {
            this.db = db;
        }

        public void Create(Quest item)
        {
            db.Quests.Add(item);
        }

        public void Delete(int id)
        {
            Quest quest = db.Quests.Find(id);
            quest.LogicDelete = true;
        }

        public Quest GetById(int? id)
        {
            Quest quest = db.Quests.Find(id);           
            return quest;
        }

        public IEnumerable<Quest> GetAll()
        {
            IEnumerable<Quest> quests = db.Quests.Include(t => t.Teacher);
            foreach (var item in quests)
            {
                db.Entry(item).State = EntityState.Detached;
            }
            return quests;
        }

        public void Update(Quest item)
        {
            Debug.WriteLine("DELETEEE " + item.Type + " " + item.Questions.Count());
            if (item.Type == "Опрос")
            {
                HashSet<Answer> answers = new HashSet<Answer>();

                foreach (var q in db.Quests.Find(item.Id).Questions)
                {
                    foreach (var a in q.Answers)
                    {
                        Debug.WriteLine("CHECK " + a.isTrue);
                        if (!a.isTrue)
                        {
                            Debug.WriteLine("HEHE " + a.isTrue);
                            answers.Add(a);
                        }
                    }
                }

                ClearFlseAnswers(answers);
            }

            db.Quests.AddOrUpdate(item);
        }

        private void ClearFlseAnswers(HashSet<Answer> answer)
        {
            foreach (var item in answer)
            {
                Debug.WriteLine("DELETEEE " + item.Text_of_answer);
                db.Answers.Remove(item);
            }
        }

        public Quest FindByName(string name)
        {
            Quest quest = db.Quests.FirstOrDefault(q => q.Name == name);
            db.Entry(quest).State = EntityState.Detached;
            return quest;
        }

        public IEnumerable<Quest> FindQuestsByTeacher(string name)
        {            
            IEnumerable<Quest> quests = db.Quests.Where(q => q.Teacher.Email.Equals(name)).Include(t => t.Teacher);
            foreach (var item in quests)
            {
                db.Entry(item).State = EntityState.Detached;
            }
            return quests;
        }

        public IEnumerable<Quest> FindQuestsByGroup(string name)
        {
            IEnumerable<Quest> quests = db.Quests.Include(g => g.Group);
            List<Quest> result = new List<Quest>();

            foreach (var item in quests)
            {
                db.Entry(item).State = EntityState.Detached;
            }

            foreach (var q in quests)
            {                
                if (q.Group!= null && q.Group.Name.Equals(name))
                {
                    result.Add(q);
                }
            }
            Debug.WriteLine("REPO RESULT " + result.Count());
            return result;            
        }
    }
}