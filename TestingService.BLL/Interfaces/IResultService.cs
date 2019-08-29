using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.DTO;

namespace TestingService.BLL.Interfaces
{
    public interface IResultService : IService<ResultDTO>
    {
        IEnumerable<ResultDTO> FindResultsByUserId(int id);
        IEnumerable<ResultDTO> FindResultsByGroupId(int id);
        IEnumerable<ResultDTO> FindResultsByQuestId(int id);
        IEnumerable<ResultDTO> MultyFindResultsByQuestId(int id, string group, string dateFirst, string dateSecond, string mark);
    }
}
