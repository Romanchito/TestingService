using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.BLL.DTO;

namespace TestingService.BLL.Interfaces
{
    public interface IQuestionService : IService<QuestionDTO>
    {
        QuestionDTO GetByName(string name);
        IEnumerable<QuestionDTO> GetAllByQuestId(int id);
    }
}
