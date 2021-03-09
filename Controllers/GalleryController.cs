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

        [HttpPost]
        public IActionResult AddPhotos(ImageViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var Files = model.image.filePhoto;
            if (Files.Count > 0)
            {
                foreach (var item in Files)
                {
                    Image image = new Image();
                    var guid = Guid.NewGuid().ToString();
                    var filePath = "wwwroot/photos/" + guid + item.FileName;
                    var fileName = guid + item.FileName;
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        item.CopyTo(stream);
                        image.Name = fileName;
                        image.Path = filePath;
                        image.Title = item.FileName;
                        image.NoOfViews = 1;
                        _context.Images.Add(image);
                        _context.SaveChanges();
                    }
                }
                return RedirectToAction("Index", "Gallery");
            }
            return RedirectToAction("Index", "Gallery");
        }
    }
}
