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

namespace HersFlowers.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Customers
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();

            if (customer == null)
            {
                return RedirectToAction(nameof(Create));
            }
            else
            {
                return View(customer);
            }
        }

        public IActionResult Products(int? id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var customer = _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();
            var flowerProducts = _context.Flowers.ToList();

            return View(flowerProducts);
        }

        [HttpPost]
        public IActionResult AddFlower(Flower flower, ShoppingCartItem shoppingCartItem)
        {

            //var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var customer = _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();

             var shoppingCart = 
             _context.Add(flower);
             _context.SaveChanges();
             return RedirectToAction("ShoppingCart");

        }


        //public IActionResult ShoppingCart()
        //{
        //    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var customer = _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();
        //    var cart = _context.ShoppingCarItems.Where(c => c.Id == customer.Id).SingleOrDefault();
        //    return View(cart);
        //}

        public IActionResult RequestMeeting(int? id)
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RequestMeeting(Request request, Customer customer) // pass customer to get customer id
        {
            var userId = customer.Id;
            var newCustomer = _context.Customers.Where(c => c.Id == userId).SingleOrDefault();
            if (customer != null)
            {
                _context.Add(request);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }            
        }

        //public IActionResult RequestMeeting()
        //{
        //    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var customer = _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();
        //    List<Request> requests = new List<Request>();
        //    requests.

        //}


        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.Id).SingleOrDefault(m => m.Id == id);
            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName,Email,PhoneNumber,Subscribe,IdentityUserId")] Customer customer)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                customer.IdentityUserId = userId;
                _context.Add(customer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(customer);
            }
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,Email,PhoneNumber,Subscribe,IdentityUserId")] Customer customer)
        {
            try
            {
                var customerEdit = _context.Customers.Single(c => c.Id == customer.Id);
                customerEdit.FirstName = customer.FirstName;
                customerEdit.LastName = customer.LastName;
                customerEdit.Email = customer.Email;
                customerEdit.PhoneNumber = customer.PhoneNumber;
                customerEdit.Subscribe = customer.Subscribe;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
