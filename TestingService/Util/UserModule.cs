using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestingService.BLL.Interfaces;
using TestingService.BLL.Services;

namespace TestingService.Util
{
    public class CreateModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IResultService>().To<ResultService>();
            Bind<IAnswerService>().To<AnswerService>();
            Bind<IQuestionService>().To<QuestionService>();
            Bind<IQuestService>().To<QuestService>();
            Bind<IGroupService>().To<GroupService>();
            Bind<IUserService>().To<UserService>();
            Bind<IImageSrvice>().To<ImageService>();
        }
    }
}