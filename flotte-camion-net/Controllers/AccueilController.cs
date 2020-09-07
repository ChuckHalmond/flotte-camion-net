using System.Web.Mvc;

namespace TP3_NET.Controllers
{
    public class AccueilController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}