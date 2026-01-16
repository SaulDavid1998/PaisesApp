using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }


        public ActionResult BadRequest()
        {
            return View();
        }

        public ActionResult Conflict()
        {
            return View();
        }
    }
}
