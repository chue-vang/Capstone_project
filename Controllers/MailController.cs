using HersFlowers.EmailService;
using MailKit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HersFlowers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : Controller
    {
        private readonly EmailService.IMailService mailService;
        public MailController(EmailService.IMailService mailService)
        {
            this.mailService = mailService;
        }

        public IActionResult SendMonthlyEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMonthlyEmail([FromForm] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return RedirectToAction("Index", "Owners");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
