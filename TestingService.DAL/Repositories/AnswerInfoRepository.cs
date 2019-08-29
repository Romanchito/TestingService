using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.DAL.EFContext;
using TestingService.DAL.Interfaces;
using TestingService.DAL.Source;

namespace TestingService.DAL.Repositories
{
    public class AnswerInfoRepository : IAnswerInfoRepository
    {
        private Context db;

        public AnswerInfoRepository(Context db)
        {
            this.db = db;
        }
        public void Create(AnswerInfo item)
        {
            db.AnswersInfo.Add(item);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AnswerInfo> GetAll()
        {
            throw new NotImplementedException();
        }

        public AnswerInfo GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public void Update(AnswerInfo item)
        {
            throw new NotImplementedException();
        }
    }
}
