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

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        public IActionResult About()
        {
            return View();
        }
        //public IActionResult RequestAppointment()
        //{
        //    return View();
        //}

        public IActionResult Services()
        {
            return View();
        }


        [HttpPost]
        public IActionResult BouquetCalculator(string LargeQuantity, string SmallQuantity)
        {
            double largeBouqet = Convert.ToDouble(LargeQuantity);
            double smallBouquet = Convert.ToDouble(SmallQuantity);
            ViewBag.LargeTotal = largeBouqet * 15;
            ViewBag.SmallTotal = smallBouquet * 10;
            ViewBag.Total = ViewBag.LargeTotal + ViewBag.SmallTotal;
            return View("Services");
        }


        //[HttpPost]
        //public IActionResult Services(Calculator calculator)
        //{
        //    int largeBouquetPrice = 15;
        //    int smallBouquetPrice = 10;
        //    calculator.largeBouquetTotal = largeBouquetPrice * calculator.largeBouquetUserInput;
        //    calculator.smallBouquetTotal = smallBouquetPrice * calculator.smallBouquetUserInput;
        //    calculator.total = calculator.largeBouquetTotal + calculator.smallBouquetTotal;
        //    return View(calculator.total);
        //}


        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
