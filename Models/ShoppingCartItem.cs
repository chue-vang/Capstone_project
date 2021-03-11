using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HersFlowers.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("Flower")]
        public int FlowerId { get; set; }

    }
}
