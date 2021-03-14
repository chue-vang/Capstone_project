using HersFlowers.APIKey;
using HersFlowers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Docs.Samples;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HersFlowers.Controllers
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
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BouquetCalculator(string LargeQuantity, string SmallQuantity)
        {
            double largeBouqet = Convert.ToDouble(LargeQuantity);
            ViewBag.LargeTotal = largeBouqet * 15;
            ViewBag.Total = ViewBag.LargeTotal + ViewBag.SmallTotal;
            return View("Services");
        }

        public IActionResult Contact()
        {
            APIKeys apiKey = new APIKeys();
            var googleKey = apiKey.ToString();
            ViewBag.key = googleKey;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
