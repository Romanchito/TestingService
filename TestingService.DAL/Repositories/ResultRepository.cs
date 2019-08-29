using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using TestingService.DAL.EFContext;
using TestingService.DAL.Entities;
using TestingService.DAL.Interfaces;

namespace TestingService.DAL.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private Context db;

        public ResultRepository(Context db)
        {
            this.db = db;
        }

        public void Create(Result item)
        {           
            db.Results.Add(item);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Result> FindResultsByGroupId(int id)
        {
            IEnumerable<Result> results = db.Results;
            List<Result> finalResults = new List<Result>();
            foreach (var item in results)
            {
                if (db.Quests.Find(item.QuestId).GroupId.Equals(id))
                {                   
                    finalResults.Add(item);
                }
            }
            return finalResults;
        }

        public IEnumerable<Result> FindResultsByQuestId(int id)
        {
            IEnumerable<Result> results = db.Results;
            List<Result> finalResults = new List<Result>();
            foreach (var item in results)
            {
                if (item.QuestId.Equals(id))
                {                    
                    finalResults.Add(item);
                }
            }
            return finalResults;
        }

        public IEnumerable<Result> FindResultsByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Result> GetAll()
        {
            return db.Results;
        }

        public Result GetById(int? id)
        {
            Result res = db.Results.Find(id);
            return res;
        }

        public IEnumerable<Result> MultyFindResultsByQuestId(int id, string group, string dateFirst, string dateSecond, string mark)
        {
            List<Result> results = db.Results.Where(x => x.QuestId.Equals(id)).ToList();
            if (group != null && group != "")
            {
                foreach (var item in results.ToArray())
                {
                    if (!db.Users.Find(item.UserId).Group.Name.Equals(group))
                    {
                        results.Remove(item);
                    }
                }
            }            
            if (dateFirst != null && dateFirst != "")
            {
                CultureInfo culture = new CultureInfo("de-DE");
                string pattern = "dd.MM.yyyy";
                string[] parts = null;
                foreach (var item in results.ToArray())
                {
                    parts = item.Date.Split(' ');
                    if (DateTime.ParseExact(dateFirst, pattern, culture) > DateTime.ParseExact(parts[0], pattern, culture))
                    {
                        results.Remove(item);
                    }
                }
            }

            if (dateSecond != null && dateSecond != "")
            {
                CultureInfo culture = new CultureInfo("de-DE");
                string pattern = "dd.MM.yyyy";
                string[] parts = null;
                foreach (var item in results.ToArray())
                {
                    parts = item.Date.Split(' ');
                    if (DateTime.ParseExact(dateSecond, pattern, culture) < DateTime.ParseExact(parts[0], pattern, culture))
                    {
                        results.Remove(item);
                    }
                }
            }

            if (mark != null && mark != "")
            {
                foreach (var item in results.ToArray())
                {
                    if (!item.Mark.ToString().Equals(mark))
                    {
                        results.Remove(item);
                    }
                }
            }

            return results;
        }

        public void Update(Result item)
        {
            throw new NotImplementedException();
        }
    }
}