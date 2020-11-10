using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Controllers
{
    public class UiControlsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}