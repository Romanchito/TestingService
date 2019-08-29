using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.DAL.Entities;

namespace TestingService.DAL.Interfaces
{
    public interface IResultRepository : IRepository<Result>
    {
        IEnumerable<Result> FindResultsByUserId(int id);
        IEnumerable<Result> FindResultsByGroupId(int id);
        IEnumerable<Result> FindResultsByQuestId(int id);
        IEnumerable<Result> MultyFindResultsByQuestId(int id, string group, string dateFirst, string dateSecond, string mark);
    }
}
