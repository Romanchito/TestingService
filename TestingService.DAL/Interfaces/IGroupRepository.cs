using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.DAL.Entities;

namespace TestingService.DAL.Interfaces
{
    public interface IGroupRepository : IRepository<Group>
    {
        IEnumerable<Group> GetGroupsByQuestId(int questId);
    }
}
