using Microsoft.AspNetCore.Mvc;

namespace enterpriseP2.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
