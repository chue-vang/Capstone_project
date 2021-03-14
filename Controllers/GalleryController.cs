using HersFlowers.Data;
using HersFlowers.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HersFlowers.Controllers
{
    public class GalleryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GalleryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ImageViewModel viewModel = new ImageViewModel();
            viewModel.imageList = _context.Images.ToList();
            viewModel.image = new Image();
            return View(viewModel);
        }
    }
}
