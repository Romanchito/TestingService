using System.Linq;
using System.Web.Mvc;
using TestingService.Models.ContextModels;

namespace TestingService.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Main()
        {           
            return View();
        }

        [Authorize(Roles = "teacher")]
        public ActionResult About()
        {
            return View();
        }
    }
}