using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using TestingService.BLL.DTO;
using TestingService.BLL.Interfaces;
using TestingService.Models.CreatorsModels;
using TestingService.Models.ViewModels;

namespace TestingService.Controllers
{
    [Authorize(Roles = "teacher")]
    public class TeacherController : Controller
    {
        private IUserService userService;
        private IQuestService questService;
        private IResultService resultService;

        public TeacherController(IUserService userService, IQuestService questService, IResultService resultService)
        {
            this.userService = userService;
            this.questService = questService;
            this.resultService = resultService;
        }

        public ActionResult TeacherPanel(string result, string questType, int? id)
        {
            ViewBag.Result = result;
            ViewBag.Quest_Type = questType;
            ViewBag.Id = id;
            return View();
        }

        public ActionResult TeachersQuests()
        {
            IEnumerable<QuestDTO> quests = questService.FindQuestsByTeacher(User.Identity.Name);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<QuestDTO, ViewQuestModel>()).CreateMapper();
            var questsMap = mapper.Map<IEnumerable<QuestDTO>, List<ViewQuestModel>>(quests);
            return PartialView(questsMap);
        }

        public ActionResult StoryQuests()
        {
            IEnumerable<QuestDTO> quests = questService.FindQuestsByTeacher(User.Identity.Name);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<QuestDTO, ViewQuestModel>()).CreateMapper();
            var questsMap = mapper.Map<IEnumerable<QuestDTO>, List<ViewQuestModel>>(quests);
            return PartialView(questsMap);
        }

        public ActionResult ShowResultsOfQuest(int questId)
        {
            if (questService.GetById(questId).Type.Equals("Тест"))
            {
                int count_5 = 0, count_4 = 0, count_3 = 0, count_2 = 0;
                foreach (var item in resultService.FindResultsByQuestId(questId))
                {
                    switch (item.Mark)
                    {
                        case 5:
                            count_5++;
                            break;

                        case 4:
                            count_4++;
                            break;

                        case 3:
                            count_3++;
                            break;

                        case 2:
                            count_2++;
                            break;
                    }
                }
                HardQuestionsModel statisticModel = Check(questId);
                ViewBag.FalseQuestions = statisticModel.FalseQuestions.ToList();
                ViewBag.FalseStatus = statisticModel.FalseStatus;
                ViewBag.TrueQuestions = statisticModel.TrueQuestions.ToList();
                ViewBag.TrueStatus = statisticModel.TrueStatus;
                ViewBag.CountOf_5 = count_5;
                ViewBag.CountOf_4 = count_4;
                ViewBag.CountOf_3 = count_3;
                ViewBag.CountOf_2 = count_2;
            }
            else
            {
                ViewBag.Questions = questService.GetById(questId).Questions;
            }
            ViewBag.Type = questService.GetById(questId).Type;
            ViewBag.questId = questId;
            ViewBag.MultyRes = "";
            return View();
        }

        public ActionResult ShowResultOfUser(int resultId, int questId, string userFullName)
        {
            ViewBag.UserFullName = userFullName;
            ViewBag.Length = resultService.GetById(resultId).Answers.ToArray().Length;
            ViewBag.Result = resultService.GetById(resultId).Answers.ToArray();
            ViewBag.Questions = questService.GetById(questId).Questions.ToArray();
            return View();
        }

        public ActionResult ResultsData(int questId, string group, string dateFirst, string dateSecond, string mark)
        {
            Debug.WriteLine("ResultDATA " + questId + " / " + group + " / " + dateFirst + " / " + dateSecond + " / " + mark);
            ViewBag.QuestId = questId;
            ViewBag.Group = group;
            ViewBag.DateFirst = dateFirst;
            ViewBag.DateSecond = dateSecond;
            ViewBag.Mark = mark;
            ViewBag.UserService = userService;
            ViewBag.QuestService = questService;
            ViewBag.Type = questService.GetById(questId).Type;

            var results = Mapper.Map<IEnumerable<ResultDTO>, List<ViewResultModel>>
                (resultService.MultyFindResultsByQuestId(questId, group, dateFirst, dateSecond, mark));
            ViewBag.Groups = GetGroups(results);
            return PartialView(results);
        }
        public ActionResult ResultStaticOpros (int questId , int position)
        {
            QuestionnaireStatisticModel statistic = CheckQuestionnaire(questId, position);
            ViewBag.Status = statistic.status;
            ViewBag.Question = statistic.question;
            ViewBag.Answers = statistic.answers.ToList();
            return PartialView();
        }
        public HardQuestionsModel Check(int questId)
        {
            var results = resultService.FindResultsByQuestId(questId);
            List<string> questions = new List<string>();
            List<string> resultQuestions = new List<string>();
            int[] trueStatus = new int[questService.GetById(questId).Questions.Count];
            int[] falseStatus = new int[questService.GetById(questId).Questions.Count];
            double[] resultsStatus = new double[falseStatus.Length];
            double[] sortedResultsStatus = new double[resultsStatus.Length];
            List<QuestionDTO> questionsOfQuest = (List<QuestionDTO>)questService.GetById(questId).Questions;
            foreach (var result in results)
            {
                int i = 0;
                foreach (var answer in result.Answers)
                {
                    if (answer.isTrue)
                    {
                        if (!questions.Contains(questionsOfQuest.ElementAt(i).Text_of_question))
                        {
                            questions.Add(questionsOfQuest.ElementAt(i).Text_of_question);
                            trueStatus[questions.IndexOf(questionsOfQuest.ElementAt(i).Text_of_question)]++;
                        }
                        else
                        {
                            trueStatus[questions.IndexOf(questionsOfQuest.ElementAt(i).Text_of_question)]++;
                        }
                    }
                    else
                    {
                        if (!questions.Contains(questionsOfQuest.ElementAt(i).Text_of_question))
                        {
                            questions.Add(questionsOfQuest.ElementAt(i).Text_of_question);
                            falseStatus[questions.IndexOf(questionsOfQuest.ElementAt(i).Text_of_question)]++;
                        }
                        else
                        {
                            falseStatus[questions.IndexOf(questionsOfQuest.ElementAt(i).Text_of_question)]++;
                        }
                    }
                    i++;
                }
            }
            for (int i = 0; i < falseStatus.Length; i++)
            {
                double percent = ((double)falseStatus[i] / (falseStatus[i] + trueStatus[i])) * 100;
                resultsStatus[i] = Math.Round(percent, 2);
            }
            for (int i = 0; i < resultsStatus.Length; i++)
            {
                sortedResultsStatus[i] = resultsStatus[i];
            }
            Array.Sort(sortedResultsStatus);
            for (int i = 0; i < sortedResultsStatus.Length; i++)
            {
                for (int a = 0; a < resultsStatus.Length; a++)
                {
                    if (resultsStatus[a] == sortedResultsStatus[i])
                    {
                        resultQuestions.Add("'" + questions.ElementAt(a) + "'" + " Верно:" + trueStatus[a] + " | Неверно:" + falseStatus[a]);
                        break;
                    }
                }
            }
            if (sortedResultsStatus.Length >= 4)
            {
                int length = sortedResultsStatus.Length;
                int count = resultQuestions.Count();

                if(count == 0)
                {
                    return new HardQuestionsModel(new List<string> { } , new List<string> { }, new double[] { }, new double[] { });
                }
                return new HardQuestionsModel(

                    new List<string> { resultQuestions.ElementAt(0), resultQuestions.ElementAt(1), resultQuestions.ElementAt(2) },
                    new List<string> { resultQuestions.ElementAt(count - 1), resultQuestions.ElementAt(count - 2), resultQuestions.ElementAt(count - 3) },
                    new double[] { 100 - sortedResultsStatus[0], 100 - sortedResultsStatus[1], 100 - sortedResultsStatus[2] },
                    new double[] { sortedResultsStatus[length - 1], sortedResultsStatus[length - 2], sortedResultsStatus[length - 3] }
                       );
            }
            else
            {
                return new HardQuestionsModel(resultQuestions,
                    resultQuestions,
                     sortedResultsStatus, sortedResultsStatus);
            }
        }

        public QuestionnaireStatisticModel CheckQuestionnaire(int questId, int i)
        {
            var results = resultService.FindResultsByQuestId(questId);
            var question = questService.GetById(questId).Questions.ElementAt(i);
            List<string> answers = new List<string>();
            int[] status = null;

            if (question.EntryType.Equals("Выбор"))
            {
                foreach (var answer in question.Answers)
                {
                    answers.Add(answer.Text_of_answer);
                }
                answers.Add("Не отвечено");
                status = new int[answers.Count];
                foreach (var result in results)
                {
                    foreach (var answer in result.Answers)
                    {
                        if (answer.QuestionId.Equals(question.Id) && answer.Text_of_answer != null)
                        {
                            status[answers.IndexOf(answer.Text_of_answer)]++;
                        }
                        if (answer.QuestionId.Equals(question.Id) && answer.Text_of_answer == null)
                        {
                            status[status.Length - 1]++;
                        }
                    }
                }
            }
            else
            {
                if (question.EntryType.Equals("Ввод") || question.EntryType.Equals("Множественный"))
                {
                    int statusLength = 0;
                    foreach (var result in results)
                    {
                        foreach (var answer in result.Answers)
                        {
                            if (answer.QuestionId.Equals(question.Id) && answer.Text_of_answer != null)
                            {
                                if (!answers.Contains(answer.Text_of_answer))
                                {
                                    answers.Add(answer.Text_of_answer);
                                }
                            }

                            if (answer.QuestionId.Equals(question.Id) && answer.Text_of_answer == null)
                            {
                                if (!answers.Contains("Не отвечено"))
                                {
                                    answers.Add("Не отвечено");
                                }
                            }
                        }
                    }
                    status = new int[answers.Count()];
                    foreach (var result in results)
                    {
                        foreach (var answer in result.Answers)
                        {
                            if (answer.QuestionId.Equals(question.Id) && answer.Text_of_answer != null)
                            {
                                status[answers.IndexOf(answer.Text_of_answer)]++;
                            }
                            if (answer.QuestionId.Equals(question.Id) && answer.Text_of_answer == null)
                            {
                                status[answers.IndexOf("Не отвечено")]++;
                            }
                        }
                    }

                    Debug.WriteLine("statusLength " + statusLength);
                    Debug.WriteLine("Question : " + question.Text_of_question);
                    Debug.Write("AnswersLength: " + answers.Count() + " " + "Status: " + status.Length);
                    Debug.Write("Answers: ");
                    foreach (var item in answers)
                    {
                        Debug.Write(item);
                    }
                    Debug.WriteLine("");
                    Debug.Write("Status: ");
                    foreach (var item in status)
                    {
                        Debug.Write(item);
                    }
                    Debug.WriteLine("");
                }
            }

            return new QuestionnaireStatisticModel(status, answers, question.Text_of_question);
        }

        private List<string> GetGroups(List<ViewResultModel> list)
        {
            List<string> listOfGroupsName = new List<string>();

            foreach (var item in list)
            {
                if (questService.GetById(item.QuestId).Group != null && !listOfGroupsName.Contains(questService.GetById(item.QuestId).Group.Name))
                {
                    listOfGroupsName.Add(questService.GetById(item.QuestId).Group.Name);
                }
            }

            return listOfGroupsName;
        }
    }
}