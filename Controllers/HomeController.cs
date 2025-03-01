using KenshiRandomizer.Models;
using Microsoft.AspNetCore.Mvc;

namespace KenshiRandomizer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string data = DataToRandomize.GetJsonData();
            HttpContext.Session.SetString("json", data);
            return View();
        }

        [HttpGet("GetRandomResult")]
        public IActionResult GetRandomResult(string name)
        {
            var data = DataToRandomize.ReadJson(HttpContext.Session.GetString("json"));
            var random = data.RandomizeSingle(name);
            return Ok(random);
        }
    }
}
