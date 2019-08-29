using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.DAL.Entities;

namespace TestingService.DAL.Interfaces
{
    public interface IAnswerRepository : IRepository<Answer>
    {
        Answer FindAnswerByName(string name);
        IEnumerable<Answer> GetAllByQuestionId(int questionId);
    }
}
