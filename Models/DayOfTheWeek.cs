using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HersFlowers.Models
{
    public class DayOfTheWeek
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Meeting Day")]
        public string Day { get; set; }
    }
}
