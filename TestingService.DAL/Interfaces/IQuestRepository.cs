using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.DAL.Entities;

namespace TestingService.DAL.Interfaces
{
    public interface IQuestRepository : IRepository<Quest>
    {
        Quest FindByName(string name);
        IEnumerable<Quest> FindQuestsByTeacher(string name);
        IEnumerable<Quest> FindQuestsByGroup(string name);
    }
}
