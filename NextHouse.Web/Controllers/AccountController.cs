using Microsoft.AspNetCore.Mvc;

namespace NextHouse.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
