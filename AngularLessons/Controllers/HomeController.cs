using System.Web.Mvc;

namespace AngularLessons.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/app/views/index.html");
        }
    }
}
