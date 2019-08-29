using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.DTO;

namespace TestingService.BLL.Interfaces
{
    public interface IQuestService : IService<QuestDTO>
    {
        QuestDTO GetQuestByName(string name);
        IEnumerable<QuestDTO> FindQuestsByTeacher(string name);
        IEnumerable<QuestDTO> FindQuestsByGroup(string name);
    }
}
