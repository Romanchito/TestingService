using System;
using TestingService.DAL.EFContext;
using TestingService.DAL.Entities;
using TestingService.DAL.Interfaces;

namespace TestingService.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private Context db;
        private UserRepository userRepository;
        private RoleRepository roleRepository;
        private QuestRepository questRepository;
        private QuestionRepository questionRepository;
        private GroupRepository groupRepository;
        private AnswerRepository answerRepository;
        private ResultRepository resultRepository;
        private ImageRepository imageRepository;

        private bool disposed = false;

        public EFUnitOfWork(string connectionString)
        {
            db = new Context(connectionString);
        }

        public IRepository<Role> Roles
        {
            get
            {
                if (roleRepository == null)
                {
                    roleRepository = new RoleRepository(db);
                }

                return roleRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(db);
                }

                return userRepository;
            }
        }

        public IQuestRepository Quests
        {
            get
            {
                if (questRepository == null)
                {
                    questRepository = new QuestRepository(db);
                }
                return questRepository;
            }
        }

        public IGroupRepository Groups
        {
            get
            {
                if (groupRepository == null)
                {
                    groupRepository = new GroupRepository(db);
                }
                return groupRepository;
            }
        }

        public IQuestionRepositiry Questions
        {
            get
            {
                if (questionRepository == null)
                {
                    questionRepository = new QuestionRepository(db);
                }
                return questionRepository;
            }
        }

        public IAnswerRepository Answers
        {
            get
            {
                if (answerRepository == null)
                {
                    answerRepository = new AnswerRepository(db);
                }
                return answerRepository;
            }
        }

        public IResultRepository Results
        {
            get
            {
                if (resultRepository == null)
                {
                    resultRepository = new ResultRepository(db);
                }
                return resultRepository;
            }
        }
        public IImageRepository Images
        {
            get
            {
                if (imageRepository == null)
                {
                    imageRepository = new ImageRepository(db);
                }
                return imageRepository;
            }
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}