using Microsoft.AspNetCore.Mvc;


namespace BookStoreMVC.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Hakkimizda()
        {
            return View();
        }

        public IActionResult Iletisim()
        {
            return View();
        }
    }
}
