using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingService.BLL.DTO;
using TestingService.BLL.Interfaces;
using TestingService.Models.ViewModels;

namespace TestingService.Controllers
{
    public class TestController : Controller
    {        

        IQuestService questService;
        
        public TestController(IQuestService questService)
        {
            this.questService = questService;
        }

        public JsonResult CheckQuestName(string username)
        {
            var result = questService.GetQuestByName(username) == null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}