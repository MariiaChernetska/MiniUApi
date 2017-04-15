using System.Web.Mvc;

namespace MiniUApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectPermanent("/swagger/ui/index");
        }
    }
}
