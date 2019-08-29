using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using TestingService.BLL.DTO;
using TestingService.BLL.Interfaces;
using TestingService.DAL.Source;
using TestingService.Models.ContextModels;
using TestingService.Models.CreatorsModels;
using TestingService.Models.ViewModels;

namespace TestingService.Controllers
{
    [Authorize(Roles = "student")]
    public class StudentController : Controller
    {
        private Context db = new Context();

        private IAnswerService answerService;
        private IQuestService questService;
        private IUserService userService;
        private IQuestionService questionService;
        private IResultService resultService;
        private IImageSrvice imageService;

        public StudentController(IQuestService questService, IAnswerService answerService, IUserService userService,
             IQuestionService questionService, IResultService resultService , IImageSrvice imageService)
        {
            this.questService = questService;
            this.answerService = answerService;
            this.userService = userService;
            this.questionService = questionService;
            this.resultService = resultService;
            this.imageService = imageService;
        }

        public ActionResult StudentPanel()
        {
            if (userService.GetUserByEmail(User.Identity.Name).Group != null)
            {
                ViewBag.Info = userService.GetUserByEmail(User.Identity.Name).Group.Name;
            }
            else
            {
                ViewBag.Info = "none";
            }
            return View();
        }

        public ActionResult TableOfQuests(string group)
        {
            var groupsMap = Mapper.Map<IEnumerable<QuestDTO>, IEnumerable<ViewQuestModel>>(questService.FindQuestsByGroup(group));
            return PartialView(groupsMap);
        }

        public ActionResult DoingQuest(int questId, string typeQuest)
        {
            AnswerModel answerModel = new AnswerModel();
            answerModel.idQuest = questId;

            if (questionService.GetAllByQuestId(questId) != null)
            {
                answerModel.questions = questionService.GetAllByQuestId(questId).ToList();
                answerModel.results = new List<AnswerDTO>();
                answerModel.Type = typeQuest;
                if (answerModel.questions[0].ImageId != null)
                {
                    ViewBag.Image = imageService.GetById(answerModel.questions[0].ImageId).Data;
                }
                
                return View("AnswerQuestion", answerModel);
            }
            else
            {
                TempData["message"] = string.Format("Не найдено вопросов в \"{0}\" ", questService.GetById(questId).Name);
                return View("StudentPanel");
            }
        }

        [HttpPost]
        public ActionResult AnswerQuestion(AnswerModel answerModel, string[] results)
        {
            answerModel.questions = (List<QuestionDTO>)TempData["questions"];
            answerModel.results = (List<AnswerDTO>)TempData["answers"];

            if (answerModel.Type.Equals("Тест"))
            {
                if (answerModel.questions[0].EntryType.Equals("Множественный"))
                {
                    if (results != null)
                    {
                        if (checkResults(results, answerModel.questions[0]))
                        {
                            answerModel.results.Add(new AnswerDTO
                            {
                                Text_of_answer = string.Join("", results),
                                isTrue = true,
                                QuestionId = answerModel.questions[0].Id
                            });
                        }
                        else
                        {
                            answerModel.results.Add(new AnswerDTO
                            {
                                Text_of_answer = string.Join("", results),
                                isTrue = false,
                                QuestionId = answerModel.questions[0].Id
                            });
                        }
                    }
                }
                else
                {
                    if (answerModel.questions[0].Answers.FirstOrDefault(x => x.Text_of_answer.Equals(answerModel.result)) != null)
                    {
                        answerModel.results.Add(answerModel.questions[0].Answers.FirstOrDefault(x => x.Text_of_answer.Equals(answerModel.result)));
                    }
                    else
                    {
                        if (answerModel.questions[0].Answers.Where(x => x.isTrue == false).Count() > 0)
                        {
                            answerModel.results.Add(answerModel.questions[0].Answers.FirstOrDefault(x => x.isTrue == false));
                        }
                        else
                        {
                            if (answerModel.result != null)
                            {
                                answerModel.results.Add(new AnswerDTO
                                {
                                    Text_of_answer = answerModel.result.ToString(),
                                    isTrue = false,
                                    QuestionId = answerModel.questions[0].Id
                                });
                            }
                            else
                            {
                                answerModel.results.Add(new AnswerDTO
                                {
                                    Text_of_answer = "не отвечено",
                                    isTrue = false,
                                    QuestionId = answerModel.questions[0].Id
                                });
                            }
                        }
                    }
                }
            }

            if (answerModel.Type.Equals("Опрос"))
            {
                if (answerModel.questions[0].EntryType.Equals("Множественный"))
                {
                    if (results != null)
                    {                       
                        
                            answerModel.results.Add(new AnswerDTO
                            {
                                Text_of_answer = string.Join("", results),
                                isTrue = true,
                                QuestionId = answerModel.questions[0].Id
                            });
                        
                    }
                    else
                    {
                        answerModel.results.Add(new AnswerDTO
                        {
                            isTrue = false,
                            QuestionId = answerModel.questions[0].Id
                        });
                    }
                }
                else
                {
                    if (answerModel.questions[0].EntryType.Equals("Ввод"))
                    {
                        if (null == answerModel.result)
                        {
                            answerModel.results.Add(new AnswerDTO { isTrue = false, QuestionId = answerModel.questions[0].Id });
                        }
                        else
                        {
                            answerModel.results.Add(new AnswerDTO
                            {
                                isTrue = true,
                                Text_of_answer = answerModel.result,
                                QuestionId = answerModel.questions[0].Id
                            });
                        }
                    }
                    else
                    {
                        if (answerModel.questions[0].Answers.FirstOrDefault(x => x.Text_of_answer.Equals(answerModel.result)) != null)
                        {
                            answerModel.results.Add(answerModel.questions[0].Answers.FirstOrDefault(x => x.Text_of_answer.Equals(answerModel.result)));
                        }
                        else
                        {
                            answerModel.results.Add(new AnswerDTO
                            {
                                isTrue = false,
                                QuestionId = answerModel.questions[0].Id
                            });
                        }
                    }
                }
            }

            answerModel.questions.RemoveAt(0);
            answerModel.result = "";

            if (answerModel.questions.Count > 0 && questService.GetById(answerModel.idQuest).Active)
            {
                if (answerModel.questions[0].ImageId != null)
                {
                    ViewBag.Image = imageService.GetById(answerModel.questions[0].ImageId).Data;
                }
                return View("AnswerQuestion", answerModel);
            }
            else
            {
                if (!questService.GetById(answerModel.idQuest).Active)
                {
                    if (questService.GetById(answerModel.idQuest).Type.Equals("Опрос"))
                    {
                        foreach (var item in answerModel.questions)
                        {
                            answerModel.results.Add(new AnswerDTO
                            {
                                isTrue = false,
                                QuestionId = item.Id
                            });
                        }
                    }
                    else
                    {
                        foreach (var item in answerModel.questions)
                        {
                            answerModel.results.Add(new AnswerDTO
                            {
                                Text_of_answer = "не отвечено",
                                isTrue = false,
                                QuestionId = item.Id
                            });
                        }
                    }
                }
            }
            resultService.Create(CreateResult(answerModel));

            var result = Mapper.Map<ResultDTO, ViewResultModel>(CreateResult(answerModel));
            Debug.WriteLine("RESULT " + result.Id + " " + result.Mark + " " + result.Percente + " " + " " + result.QuestId);

            if (answerModel.Type.Equals("Тест")) return View("Result", result);
            else return View("AskingResult", result);
        }

        private ResultDTO CreateResult(AnswerModel answerModel)
        {
            ResultDTO result = new ResultDTO();
            result.Answers = new List<AnswerInfo>();
            result.Date = System.DateTime.Now.ToString();
            foreach (var item in answerModel.results)
            {
                AnswerInfo answerInfo = new AnswerInfo();
                answerInfo.Text_of_answer = item.Text_of_answer;
                answerInfo.isTrue = item.isTrue;
                answerInfo.QuestionId = item.QuestionId;
                answerInfo.ResultId = result.Id;
                result.Answers.Add(answerInfo);
            }

            double countTrusAnswers = 0;

            QuestDTO quest = questService.GetById(answerModel.idQuest);

            result.QuestId = answerModel.idQuest;

            result.UserId = userService.GetUserByEmail(User.Identity.Name).Id;

            if (answerModel.Type.Equals("Тест"))
            {
                foreach (var item in answerModel.results)
                {
                    if (item.isTrue)
                    {
                        countTrusAnswers++;
                    }
                }

                double value = (countTrusAnswers / answerModel.results.Count) * 100;
                result.Percente = Math.Round(value, 2);

                if (result.Percente >= quest.Percent_Of_Exelent)
                {
                    result.Mark = 5;
                }
                else
                {
                    if (result.Percente >= quest.Percent_Of_Good)
                    {
                        result.Mark = 4;
                    }
                    else
                    {
                        if (result.Percente >= quest.Percent_Of_Satisfactory)
                        {
                            result.Mark = 3;
                        }
                        else
                        {
                            result.Mark = 2;
                        }
                    }
                }
            }

            return result;
        }

        private bool checkResults(string[] results, QuestionDTO question)
        {
            int countFalse = 0;
            int countTrue = 0;
            int countOfTrues = 0;

            foreach (var item in question.Answers)
            {
                if (item.isTrue) countOfTrues++;
            }

            for (int i = 0; i < results.Length; i++)
            {
                foreach (var item in question.Answers)
                {
                    if (results[i].Equals(item.Text_of_answer) && item.isTrue)
                    {
                        countTrue++;
                    }
                }
            }

            for (int i = 0; i < results.Length; i++)
            {
                foreach (var item in question.Answers)
                {
                    if (results[i].Equals(item.Text_of_answer) && !item.isTrue)
                    {
                        countFalse++;
                    }
                }
            }

            if (countFalse > 0 || countTrue != countOfTrues) return false;
            return true;
        }
    }
}