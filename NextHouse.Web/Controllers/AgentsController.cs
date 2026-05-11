using Microsoft.AspNetCore.Mvc;

namespace NextHouse.Web.Controllers
{
    public class AgentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
