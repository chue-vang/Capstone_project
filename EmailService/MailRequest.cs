using HersFlowers.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HersFlowers.EmailService
{
    public class MailRequest
    {
        [Key]
        public int Id { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        [ForeignKey("Owner")]
        public int? OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
