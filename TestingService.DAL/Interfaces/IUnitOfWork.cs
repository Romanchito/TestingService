using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingService.DAL.Entities;

namespace TestingService.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRepository<Role> Roles { get; }
        IQuestRepository Quests { get; }
        IGroupRepository Groups { get;}
        IQuestionRepositiry Questions { get; }
        IAnswerRepository Answers { get; }
        IResultRepository Results { get; }
        IImageRepository Images { get; } 
        void Save();
    }
}
