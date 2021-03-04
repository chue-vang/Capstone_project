using HersFlowers.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HersFlowers.ActionFilters
{
    public class GlobalRouting : IActionFilter
    {
        private readonly ClaimsPrincipal _claimsPrincipal;
        public GlobalRouting(ClaimsPrincipal claimsPrincipal)
        {
            _claimsPrincipal = claimsPrincipal;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.RouteData.Values["controller"];
            if (controller.Equals("Home"))
            {
                if (_claimsPrincipal.IsInRole("Owner"))
                {
                    context.Result = new RedirectToActionResult("Index",
                    "Owners", null);
                }
                else if (_claimsPrincipal.IsInRole("Customer"))
                {
                    context.Result = new RedirectToActionResult("Index",
                    "Customers", null);
                }
            }
        }

        //public void OnActionExecuted(ActionExecutedContext context, )
        //{
        //    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    var customer = _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();
        //    if (customer == null)
        //    {
        //        return RedirectToAction("Index", "Customers");
        //    }
        //    else (customer == )
        //    {
        //        return RedirectToAction("Index", "Owners");
        //    }
        //}

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
