using AutoMapper;
using TestingService.BLL.DTO;
using TestingService.DAL.Entities;
using TestingService.DAL.Source;
using TestingService.Models.ViewModels;

namespace TestingService.App_Start
{
    public static class AutoMapperConfig
    {
        public static void MapperRegister()
        {
            Mapper.Initialize(m =>
            {
                //m.CreateMap<AnswerDTO, AnswerInfo>().ReverseMap();
                m.CreateMap<QuestDTO, ViewQuestModel>().ReverseMap();
                m.CreateMap<UserDTO, ViewUserModel>().ReverseMap();
                m.CreateMap<Quest, QuestDTO>().ReverseMap();
                m.CreateMap<User, UserDTO>().ReverseMap();
                m.CreateMap<Group, GroupDTO>().ReverseMap();
                m.CreateMap<Question, QuestionDTO>().ReverseMap();
                m.CreateMap<Answer, AnswerDTO>().ReverseMap();
                m.CreateMap<Result, ResultDTO>().ReverseMap();
                m.CreateMap<ResultDTO, ViewResultModel>().ReverseMap();
                m.CreateMap<Role, RoleDTO>().ReverseMap();
                m.CreateMap<ImageDTO, ViewImageModel>().ReverseMap();
                m.CreateMap<Image, ImageDTO>().ReverseMap();
            });

            Mapper.AssertConfigurationIsValid();
        }
    }
}