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
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public string? Message { get; set; }

        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
