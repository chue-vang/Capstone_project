using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HersFlowers.Models
{
    public class MeetingDay
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Meeting Day")]
        public string Date { get; set; }
    }
}
