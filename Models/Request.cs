using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HersFlowers.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DisplayFormat(DataFormatString = "{0:###-###-####}")]
        [DataType(DataType.PhoneNumber)]
        public double PhoneNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:dddd, dd MMMM yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Display(Name = "Start Time")]
        [DisplayFormat(DataFormatString = "{0:h:mm tt}")]
        [DataType(DataType.Time)]
        public DateTime? StartTime { get; set; }

        [Display(Name = "End Time")]
        [DisplayFormat(DataFormatString = "{0:h:mm tt}")]
        public DateTime? EndTime { get; set; }

        public string? Message { get; set; }

        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        [NotMapped]
        public DayOfWeek DayOfWeek { get; set; }
    }
}
