﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HersFlowers.Data;
using HersFlowers.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using HersFlowers.EmailService;
using MailKit.Net.Smtp;

namespace HersFlowers.Controllers
{
    [Authorize(Roles = "Owner")]
    public class OwnersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OwnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Owners
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = _context.Owners.Where(c => c.IdentityUserId == userId).SingleOrDefault();

            if (owner == null)
            {
                return RedirectToAction(nameof(Create));
            }
            else
            {
                ViewBag.Id = owner.Id;
                var allRequestedMeetings = _context.Requests.OrderBy(r => r.Date).ThenBy(r => r.StartTime).ToList();
                return View(allRequestedMeetings);
            }
        }
        public IActionResult UploadImages()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = _context.Owners.Where(c => c.IdentityUserId == userId).SingleOrDefault();
            ViewBag.Id = owner.Id;
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
                return RedirectToAction("UploadImages", "Owners");
            }
            return RedirectToAction("UploadImages", "Owners");
        }

        //public IActionResult FilterByDays()
        //{

        //    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var owner = _context.Owners.Where(c => c.IdentityUserId == userId).SingleOrDefault();
        //    var filterDays = _context.Requests.Where(r => r.Id == owner.Id).ToList();
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult FilterByDays(int? id)
        //{
        //    DayOfTheWeek dayOfTheWeek = new DayOfTheWeek();
        //    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var owner = _context.Owners.Where(c => c.IdentityUserId == userId).SingleOrDefault();
        //    var currentDay = DateTime.Today.DayOfWeek;
        //    //var filteredDays = _context.Requests.Where(r => r.Date.Value.DayOfWeek == currentDay).ToList();
        //    if (dayOfTheWeek.DayOfWeek == DayOfWeek.Monday)
        //    {
        //        var filterDay = _context.Requests.Where(r => r.Date.Value.DayOfWeek == currentDay).ToList();
        //        return View(filterDay);
        //    }
        //    else
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //}

        public IActionResult EmailMonthlySubscriber()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var owner = _context.Owners.Where(c => c.IdentityUserId == userId).SingleOrDefault();
            ViewBag.Id = owner.Id;
            var emailSubscribers = _context.Customers.Where(c => c.Subscribe == true).ToList();
            return View(emailSubscribers);
        }

        // GET: Owners/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var owner = _context.Owners.SingleOrDefault(m => m.Id == id);
            return View(owner);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Owners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,Email,IdentityUserId")] Owner owner)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                owner.IdentityUserId = userId;
                _context.Add(owner);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(owner);
            }
        }

        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", owner.IdentityUserId);
            return View(owner);
        }

        // POST: Owners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,Email,IdentityUserId")] Owner owner)
        {
            try
            {
                var ownerEdit = _context.Owners.Single(o => o.Id == owner.Id);
                ownerEdit.FirstName = owner.FirstName;
                ownerEdit.LastName = owner.LastName;
                ownerEdit.Email = owner.Email;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners
                .Include(o => o.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnerExists(int id)
        {
            return _context.Owners.Any(e => e.Id == id);
        }
    }
}
