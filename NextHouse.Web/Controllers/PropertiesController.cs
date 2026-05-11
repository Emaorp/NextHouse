using Microsoft.AspNetCore.Mvc;

namespace NextHouse.Web.Controllers
{
    public class PropertiesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
