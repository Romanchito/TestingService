using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.DAL.Entities;

namespace TestingService.DAL.Interfaces
{
    public interface IQuestionRepositiry : IRepository<Question>
    {
        Question findByName(string name);
        IEnumerable<Question> GetAllByQuestId(int id);
    }
}
