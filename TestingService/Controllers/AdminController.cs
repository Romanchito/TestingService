using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingService.BLL.DTO;
using TestingService.BLL.Interfaces;
using TestingService.Models.ViewModels;

namespace TestingService.Controllers
{
    [Authorize(Roles = "teacher,admin")]
    public class AdminController : Controller
    {
        private IGroupService groupService;
        private IUserService userService;
        private IQuestService questService;
        private IQuestionService questionService;
        private IAnswerService answerService;
        private IImageSrvice imageService;

        public AdminController(IGroupService groupService, IUserService userService, IQuestService questService,
            IQuestionService questionService, IAnswerService answerService , IImageSrvice imageService)
        {
            this.userService = userService;
            this.groupService = groupService;
            this.questService = questService;
            this.questionService = questionService;
            this.answerService = answerService;
            this.imageService = imageService;
        }

        public ActionResult AdminPanel(string result, string questType, int? id)
        {
            ViewBag.Result = result;
            ViewBag.Quest_Type = questType;
            ViewBag.Id = id;
            return View();
        }

        public ActionResult Groups()
        {
            IEnumerable<GroupDTO> groups = groupService.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<GroupDTO, ViewGroupModel>()).CreateMapper();
            var groupsMap = mapper.Map<IEnumerable<GroupDTO>, List<ViewGroupModel>>(groups);
            ViewBag.Groups = groupsMap;
            return PartialView();
        }

        public ActionResult EditGroup(int groupId)
        {
            GroupDTO groupDTO = groupService.GetById(groupId);
            ViewGroupModel group = new ViewGroupModel();
            if (groupDTO != null)
            {
                group.Id = groupDTO.Id;
                group.Name = groupDTO.Name;
            }

            return View(group);
        }

        [HttpPost]
        public ActionResult EditGroup(ViewGroupModel group)
        {
            int flag = 0;
            foreach (var item in groupService.GetAll())
            {
                if (item.Name.Equals(group.Name))
                {
                    flag++;
                    break;
                }
            }
            if(flag>0 && group.Id == 0) ModelState.AddModelError("", "Такая группа уже существует");
            if (ModelState.IsValid)
            {
                if (group.Id == 0)
                {
                    groupService.Create(new GroupDTO { Id = group.Id, Name = group.Name });
                    TempData["message"] = string.Format("Добавление группы \"{0}\" выполнено успешно", group.Name);
                }
                else
                {
                    GroupDTO newGroup = groupService.GetById(group.Id);
                    if (newGroup != null)
                    {
                        newGroup.Name = group.Name;
                        groupService.Update(newGroup);
                        TempData["message"] = string.Format("Обновление группы \"{0}\" выполнено успешно", group.Name);
                    }
                }

                return RedirectToAction("AdminPanel", new { result = "groups" });
            }

            return View();
        }

        public ViewResult CreateGroup()
        {
            return View("EditGroup", new ViewGroupModel());
        }

        [HttpPost]
        public ActionResult DeleteGroup(int groupId)
        {
            GroupDTO deleteGroup = groupService.GetById(groupId);
            if (deleteGroup != null)
            {
                groupService.Delete(groupId);
                TempData["message"] = string.Format("Удаление \"{0}\" выполнено успешно", deleteGroup.Name);
            }

            return RedirectToAction("AdminPanel", new { result = "groups" });
        }

        public ActionResult Users()
        {
            IEnumerable<UserDTO> users = userService.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, ViewUserModel>()).CreateMapper();
            var usersMap = mapper.Map<IEnumerable<UserDTO>, List<ViewUserModel>>(users);
            ViewBag.Users = usersMap;
            return PartialView();
        }

        public ActionResult EditUser(int userId)
        {
            UserDTO userDTO = userService.GetById(userId);
            ViewBag.Groups = CreateSelectListOfGroups(userDTO.GroupId);
            ViewUserModel user = new ViewUserModel();
            if (userDTO != null)
            {
                user.Id = userDTO.Id;
                user.Name = userDTO.Name;
                user.Surname = userDTO.Surname;
                user.Patronomic = userDTO.Patronomic;
                user.Email = userDTO.Email;
                user.Password = userDTO.Password;
                user.RoleId = userDTO.RoleId;
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(ViewUserModel user)
        {
            int flag = 0;
            foreach (var item in userService.GetAll())
            {
                if (item.Email.Equals(user.Email))
                {
                    flag++;
                    break;
                }
            }
            if (flag > 0 && user.Id == 0) ModelState.AddModelError("", "Такая почта уже занята");
            if (ModelState.IsValid)
            {
                if (user.Id == 0)
                {
                    UserDTO newUser = Mapper.Map<ViewUserModel, UserDTO>(user);
                    userService.Create(newUser);
                    TempData["message"] = string.Format("Добавление пользователя \"{0}\" выполнено успешно", user.Email);
                }
                else
                {
                    UserDTO newUser = userService.GetById(user.Id);
                    if (newUser != null)
                    {
                        newUser = Mapper.Map<ViewUserModel, UserDTO>(user);
                        userService.Update(newUser);
                        TempData["message"] = string.Format("Обновление пользователя \"{0}\" выполнено успешно", user.Email);
                    }
                }

                return RedirectToAction("AdminPanel", new { result = "users" });
            }
            if (user.Id == 0) ViewBag.Groups = CreateSelectListOfGroups(0);
            else ViewBag.Groups = CreateSelectListOfGroups(user.Id);
            return View();
        }

        public ViewResult CreateUser()
        {
            ViewBag.Groups = CreateSelectListOfGroups(0);
            return View("EditUser", new ViewUserModel());
        }

        [HttpPost]
        public ActionResult DeleteUser(int userId)
        {
            UserDTO deleteUser = userService.GetById(userId);
            if (deleteUser != null)
            {
                userService.Delete(userId);
                TempData["message"] = string.Format("Удаление \"{0}\" выполнено успешно", deleteUser.Email);
            }

            return RedirectToAction("AdminPanel", new { result = "users" });
        }

        public ActionResult Quests()
        {
            IEnumerable<QuestDTO> quests = questService.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<QuestDTO, ViewQuestModel>()).CreateMapper();
            var questsMap = mapper.Map<IEnumerable<QuestDTO>, List<ViewQuestModel>>(quests);
            ViewBag.Quests = questsMap;
            return PartialView();
        }

        public ActionResult EditQuest(int questId)
        {
            ViewBag.Teachers = CreateSelectListOfTeachers();
            ViewBag.Groups = CreateSelectListOfGroups(0);
            ViewBag.User = userService.GetUserByEmail(User.Identity.Name);
            QuestDTO question = questService.GetById(questId);

            foreach (var item in groupService.GetGroupsByQuestId(questId))
            {
                Debug.WriteLine(item.Name);
            }

            foreach (var item in ViewBag.Groups)
            {
                if (groupService.GetGroupsByQuestId(questId).Any(g => string.Equals(g.Name, item.Text)))
                {
                    item.Selected = true;
                }
            }
            ViewQuestModel viewQuestModel = Mapper.Map<QuestDTO, ViewQuestModel>(question);
            return View(viewQuestModel);
        }

        [HttpPost]
        public ActionResult EditQuest(ViewQuestModel quest)
        {
            ViewBag.User = userService.GetUserByEmail(User.Identity.Name);
            int flag = 0;
            foreach (var item in questService.GetAll())
            {
                if (item.Name.Equals(quest.Name))
                {
                    flag++;
                    break;
                }
            }
            if (flag > 0 && quest.Id == 0) ModelState.AddModelError("", "Такой материал уже существует");
            if (quest.Type.Equals("Тест"))
            {
                if (quest.Percent_Of_Exelent <= quest.Percent_Of_Good || quest.Percent_Of_Exelent <= quest.Percent_Of_Satisfactory || quest.Percent_Of_Good <= quest.Percent_Of_Satisfactory)
                {
                    ModelState.AddModelError("", "Ошибка в соотношении процентов");
                }
                else if (quest.Percent_Of_Exelent > 100 || quest.Percent_Of_Exelent < 30 ||
                         quest.Percent_Of_Good > 100 || quest.Percent_Of_Good < 30 || quest.Percent_Of_Satisfactory > 100 || quest.Percent_Of_Satisfactory < 30)
                {
                    ModelState.AddModelError("", "Процент должен быть в промежутке от 30 до 100");
                }
            }
            if (ModelState.IsValid)
            {              

                if (quest.Id == 0)
                {
                    QuestDTO questDTO = Mapper.Map<ViewQuestModel, QuestDTO>(quest);
                    questService.Create(questDTO);
                    TempData["message"] = string.Format("Добавление задания выполнено успешно");
                }
                else
                {
                    QuestDTO newQuest = questService.GetById(quest.Id);
                    if (newQuest != null)
                    {
                        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ViewQuestModel, QuestDTO>()).CreateMapper();
                        newQuest = mapper.Map<ViewQuestModel, QuestDTO>(quest);
                        questService.Update(newQuest);
                        TempData["message"] = string.Format("Добавление задания выполнено успешно");
                    }
                }
                if (userService.GetUserByEmail(User.Identity.Name).RoleId == 2)
                {
                    return RedirectToAction("TeacherPanel", "Teacher" ,  new { result = "quests" });
                }
                return RedirectToAction("AdminPanel", new { result = "quests" });
            }
            ViewBag.Teachers = CreateSelectListOfTeachers();
            ViewBag.Groups = CreateSelectListOfGroups(0);
            ViewBag.User = userService.GetUserByEmail(User.Identity.Name);
            return View();
        }

        public ViewResult CreateQuest()
        {
            ViewBag.User = userService.GetUserByEmail(User.Identity.Name);
            ViewBag.Groups = CreateSelectListOfGroups(0);
            ViewBag.Teachers = CreateSelectListOfTeachers();
            return View("EditQuest", new ViewQuestModel());
        }

        [HttpPost]
        public ActionResult DeleteQuest(int questId)
        {
            QuestDTO deleteQuest = questService.GetById(questId);
            if (deleteQuest != null)
            {
                questService.Delete(questId);
                TempData["message"] = string.Format("Удаление \"{0}\" выполнено успешно", deleteQuest.Name);
            }

            if (userService.GetUserByEmail(User.Identity.Name).RoleId == 2)
            {
                return RedirectToAction("TeacherPanel", "Teacher",  new { result = "quests" });
            }
            return RedirectToAction("AdminPanel", new { result = "quests" });
        }

        private List<SelectListItem> CreateSelectListOfTeachers()
        {
            bool first = true;
            List<SelectListItem> items = new List<SelectListItem>();
            List<UserDTO> teachers = userService.FindUsersByRole("teacher").ToList();

            foreach (var item in teachers)
            {
                if (first)
                {
                    items.Add(new SelectListItem
                    {
                        Text = item.Surname + " " + item.Name + " " + item.Patronomic,
                        Value = item.Id.ToString(),
                        Selected = true
                    });
                    first = false;
                    continue;
                }
                items.Add(new SelectListItem
                {
                    Text = item.Surname + " " + item.Name + " " + item.Patronomic,
                    Value = item.Id.ToString()
                });
            }

            return items;
        }

        private List<SelectListItem> CreateSelectListOfGroups(int? id)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var groups = groupService.GetAll();
            if (id == null && id == 0)
            {
                Debug.WriteLine("NOOOOO");
                foreach (var item in groups)
                {
                    items.Add(new SelectListItem
                    {
                        Text = item.Name,
                        Value = item.Id.ToString()
                    });
                }
            }
            else
            {
                Debug.WriteLine("AAAAAAAAAA1 " + id);
                foreach (var item in groups)
                {
                    Debug.WriteLine(item.Id);
                    if (item.Id.Equals(id))
                    {
                        items.Add(new SelectListItem
                        {
                            Text = item.Name,
                            Value = item.Id.ToString(),
                            Selected = true
                        });
                        Debug.WriteLine("AAAAAAAAAA");
                    }
                    else
                    {
                        items.Add(new SelectListItem
                        {
                            Text = item.Name,
                            Value = item.Id.ToString()
                        });
                    }
                }
            }

            return items;
        }

        private List<GroupDTO> AddGroupList(List<string> groups)
        {
            List<GroupDTO> groupsList = new List<GroupDTO>();
            if (groups != null)
            {
                foreach (var item in groups)
                {
                    groupsList.Add(groupService.GetById(Convert.ToInt32(item)));
                }
            }

            return groupsList;
        }

        [HttpPost]
        public ActionResult ActivateQuest(int questId)
        {
            QuestDTO quest = questService.GetById(questId);
            if (quest.Active)
            {
                quest.Active = false;
            }
            else
            {
                quest.Active = true;
            }
            questService.Update(quest);
            ViewBag.Quest = quest;
            ViewBag.User = userService.GetUserByEmail(User.Identity.Name);
            return View();
        }

        public ActionResult Questions(int questId, string questType)
        {
            ViewBag.QuestType = questType;
            IEnumerable<QuestionDTO> quests = questionService.GetAllByQuestId(questId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<QuestDTO, ViewQuestModel>()).CreateMapper();
            var questsMap = mapper.Map<IEnumerable<QuestionDTO>, List<ViewQuestionModel>>(quests);
            ViewBag.IdQuest = questId;
            return PartialView(questsMap);
        }

        public ActionResult EditQuestion(int questionId)
        {
            ViewBag.Images = imageService.GetAll();
            QuestionDTO question = questionService.GetById(questionId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<QuestionDTO, ViewQuestionModel>()).CreateMapper();
            ViewQuestionModel viewQuestionModel = mapper.Map<QuestionDTO, ViewQuestionModel>(question);
            ViewBag.QuestType = questService.GetById(question.QuestId).Type;
            ViewBag.User = userService.GetUserByEmail(User.Identity.Name);
            ViewBag.Id = question.QuestId;
            return View(viewQuestionModel);
        }

        [HttpPost]
        public ActionResult EditQuestion(ViewQuestionModel question)
        {
            ViewBag.Images = imageService.GetAll();
            ViewBag.User = userService.GetUserByEmail(User.Identity.Name);
            int flag = 0;
            foreach (var item in questionService.GetAllByQuestId(question.QuestId))
            {
                if (item.Text_of_question.Equals(question.Text_of_question))
                {
                    flag++;
                    break;
                }
            }
            if (flag > 0 && question.Id == 0) ModelState.AddModelError("", "Такой вопрос уже существует");
            if (ModelState.IsValid)
            {
                if (question.Id == 0)
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ViewQuestionModel, QuestionDTO>()).CreateMapper();
                    QuestionDTO questionDTO = mapper.Map<ViewQuestionModel, QuestionDTO>(question);
                    questionService.Create(questionDTO);
                    TempData["message"] = string.Format("Добавление вопроса выполнено успешно");
                }
                else
                {
                    QuestionDTO newQuestion = questionService.GetById(question.Id);
                    if (newQuestion != null)
                    {
                        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ViewQuestionModel, QuestionDTO>()).CreateMapper();
                        newQuestion = mapper.Map<ViewQuestionModel, QuestionDTO>(question);
                        questionService.Update(newQuestion);
                        TempData["message"] = string.Format("Добавление вопроса выполнено успешно");
                    }
                }
                if (userService.GetUserByEmail(User.Identity.Name).RoleId == 2)
                {
                    return RedirectToAction("TeacherPanel", "Teacher" , new
                    {
                        result = "questions",
                        questType = questService.GetById(question.QuestId).Type,
                        id = question.QuestId
                    });
                }
                return RedirectToAction("AdminPanel", new
                {
                    result = "questions",
                    questType = questService.GetById(question.QuestId).Type,
                    id = question.QuestId
                });
            }
            return View();
        }

        [HttpPost]
        public ActionResult DeleteQuestion(int questionId)
        {
            QuestionDTO deleteQuestion = questionService.GetById(questionId);
            if (deleteQuestion != null)
            {
                questionService.Delete(questionId);
                TempData["message"] = string.Format("Удаление \"{0}\" выполнено успешно", deleteQuestion.Text_of_question);
            }

            if (userService.GetUserByEmail(User.Identity.Name).RoleId == 2)
            {
                return RedirectToAction("TeacherPanel", "Teacher", new
                {
                    result = "questions",
                    questType = questService.GetById(deleteQuestion.QuestId).Type,
                    id = deleteQuestion.QuestId
                });
            }
            return RedirectToAction("AdminPanel", new
            {
                result = "questions",
                questType = questService.GetById(deleteQuestion.QuestId).Type,
                id = deleteQuestion.QuestId
            });
        }

        public ViewResult CreateQuestion(int questId)
        {
            ViewBag.User = userService.GetUserByEmail(User.Identity.Name);
            ViewBag.Images = imageService.GetAll();
            return View("EditQuestion", new ViewQuestionModel(questId));
        }

        public ActionResult Answers(int questionId, string questType)
        {
            ViewBag.User = userService.GetUserByEmail(User.Identity.Name);
            ViewBag.QuestType = questType;
            IEnumerable<AnswerDTO> answers = answerService.GetAllByQuestionId(questionId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AnswerDTO, ViewAnswerModel>()).CreateMapper();
            var questsMap = mapper.Map<IEnumerable<AnswerDTO>, List<ViewAnswerModel>>(answers);
            ViewBag.QuestId = questionService.GetById(questionId).QuestId;
            ViewBag.QuestionId = questionId;
            ViewBag.Answers = questsMap;
            return PartialView(questsMap);
        }
         
        public ViewResult CreateAnswer(int questionId, string questType)
        {
            ViewBag.Id = questionId;
            ViewBag.QuestionName = questionService.GetById(questionId).Text_of_question;
            ViewBag.User = userService.GetUserByEmail(User.Identity.Name);
            ViewBag.EnteryType = questionService.GetById(questionId).EntryType;
            ViewBag.QuestType = questService.GetById(questionService.GetById(questionId).QuestId).Type;
            return View("EditAnswer", new ViewAnswerModel(questionId));
        }

        public ActionResult EditAnswer(int answerId, string questType)
        {
            ViewBag.QuestType = questService.GetById(questionService.GetById(answerService.GetById(answerId).QuestionId).QuestId).Type;
            AnswerDTO answer = answerService.GetById(answerId);
            ViewBag.QuestionName = questionService.GetById(answer.QuestionId).Text_of_question;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AnswerDTO, ViewAnswerModel>()).CreateMapper();
            ViewAnswerModel viewAnswerModel = mapper.Map<AnswerDTO, ViewAnswerModel>(answer);
            ViewBag.Id = questionService.GetById(answer.QuestionId).Id;
            ViewBag.User = userService.GetUserByEmail(User.Identity.Name);
            return View(viewAnswerModel);
        }

        [HttpPost]
        public ActionResult EditAnswer(ViewAnswerModel answer)
        {
            ViewBag.User = userService.GetUserByEmail(User.Identity.Name);
            int flag = 0;
            foreach (var item in answerService.GetAllByQuestionId(answer.QuestionId))
            {
                if (item.Text_of_answer.Equals(answer.Text_of_answer))
                {
                    flag++;
                    break;
                }
            }
            if (flag > 0 && answer.Id == 0) ModelState.AddModelError("", "Такой ответ уже существует");
            if (ModelState.IsValid)
            {
                if (answer.Id == 0)
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ViewAnswerModel, AnswerDTO>()).CreateMapper();
                    AnswerDTO answerDTO = mapper.Map<ViewAnswerModel, AnswerDTO>(answer);
                    answerService.Create(answerDTO);
                    TempData["message"] = string.Format("Добавление ответа выполнено успешно");
                }
                else
                {
                    AnswerDTO newAnswer = answerService.GetById(answer.Id);
                    if (newAnswer != null)
                    {
                        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ViewAnswerModel, AnswerDTO>()).CreateMapper();
                        newAnswer = mapper.Map<ViewAnswerModel, AnswerDTO>(answer);
                        answerService.Update(newAnswer);
                        TempData["message"] = string.Format("Добавление ответа выполнено успешно");
                    }
                }

                if (userService.GetUserByEmail(User.Identity.Name).RoleId == 2)
                {
                    return RedirectToAction("TeacherPanel", "Teacher" , new
                    {
                        result = "answers",
                        questType = questService.GetById(questionService.GetById(answer.QuestionId).QuestId).Type,
                        id = answer.QuestionId
                    });
                }
                return RedirectToAction("AdminPanel", new
                {
                    result = "answers",
                    questType = questService.GetById(questionService.GetById(answer.QuestionId).QuestId).Type,
                    id = answer.QuestionId
                });
            }
            ViewBag.QuestType = questService.GetById(questionService.GetById(answer.QuestionId).QuestId).Type;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteAnswer(int answerId)
        {
            AnswerDTO deleteAnswer = answerService.GetById(answerId);
            string type = questService.GetById(questionService.GetById(deleteAnswer.QuestionId).QuestId).Type;
            int id = deleteAnswer.QuestionId;
            Debug.WriteLine("DELETE " + id + " " + type);
            if (deleteAnswer != null)
            {
                answerService.Delete(answerId);
                TempData["message"] = string.Format("Удаление \"{0}\" выполнено успешно", deleteAnswer.Text_of_answer);
            }
            if (userService.GetUserByEmail(User.Identity.Name).RoleId == 2)
            {
                return RedirectToAction("TeacherPanel", "Teacher", new { result = "answers", questType = type, id = id });
            }
            return RedirectToAction("AdminPanel", "Admin", new { result = "answers", questType = type, id = id });
        }

        public ActionResult Images()
        {
            IEnumerable<ImageDTO> images = imageService.GetAll();           
            var imageMap = Mapper.Map<IEnumerable<ImageDTO>, List<ViewImageModel>>(images);
            ViewBag.Images = imageMap;
            return View();
        }


        public ActionResult EditImage(int id)
        {
            ViewBag.User = userService.GetUserByEmail(User.Identity.Name);
            ViewImageModel image = Mapper.Map<ImageDTO, ViewImageModel>(imageService.GetById(id));
            return View(image);
        }

        [HttpPost]
        public ActionResult EditImage(HttpPostedFileBase uploadImage, ViewImageModel model)
        {            
            ViewBag.User = userService.GetUserByEmail(User.Identity.Name);
            int flag = 0;
            foreach (var item in imageService.GetAll())
            {
                if (item.Name.Equals(model.Name))
                {
                    flag++;
                    break;
                }
            }
            if (flag > 0 && model.Id == 0) ModelState.AddModelError("", "Такое наименование уже существует");

            if (ModelState.IsValid)
            {

                if (uploadImage != null && model.Id == 0)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                    }
                    imageService.Create(new ImageDTO { Id = model.Id, Data = imageData, Name = model.Name });
                    TempData["message"] = string.Format("Добавление изображения выполнено успешно");

                    if (userService.GetUserByEmail(User.Identity.Name).RoleId == 2)
                    {
                        return RedirectToAction("TeacherPanel", "Teacher", new { result = "images" });
                    }
                    return RedirectToAction("AdminPanel", new { result = "images" });
                }
                if (model.Id != 0)
                {
                    if (uploadImage != null)
                    {
                        byte[] imageData = null;
                        using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                        {
                            imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                        }
                        imageService.Update(new ImageDTO { Id = model.Id, Data = imageData, Name = model.Name });
                        if (userService.GetUserByEmail(User.Identity.Name).RoleId == 2)
                        {
                            return RedirectToAction("TeacherPanel", "Teacher", new { result = "images" });
                        }
                        return RedirectToAction("AdminPanel", new { result = "images" });
                    }
                    else
                    {
                        imageService.Update(new ImageDTO { Id = model.Id, Data = imageService.GetById(model.Id).Data, Name = model.Name });
                        if (userService.GetUserByEmail(User.Identity.Name).RoleId == 2)
                        {
                            return RedirectToAction("TeacherPanel", "Teacher", new { result = "images" });
                        }
                        return RedirectToAction("AdminPanel", new { result = "images" });
                    }
                }
            }

            return View();
        }

        public ActionResult CreateImage()
        {
            ViewBag.User = userService.GetUserByEmail(User.Identity.Name);
            return View("EditImage", new ViewImageModel());
        }

        public ActionResult DeleteImage(int imageId)
        {
            ImageDTO deleteImage = imageService.GetById(imageId);
            if (deleteImage != null)
            {
                imageService.Delete(imageId);
                TempData["message"] = string.Format("Удаление изображения выполнено успешно");
            }
            if (userService.GetUserByEmail(User.Identity.Name).RoleId == 2)
            {
                return RedirectToAction("TeacherPanel", "Teacher", new { result = "images" });
            }
            return RedirectToAction("AdminPanel", new { result = "images" });
        }

        public ActionResult ShowImage(int imageId)
        {
            ViewBag.Data = null;
            if(imageId != 0)
            {
                ViewBag.Data = imageService.GetById(imageId).Data;
            }
            return PartialView();
        }
    }
}