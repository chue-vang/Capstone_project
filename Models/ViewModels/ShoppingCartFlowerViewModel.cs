using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HersFlowers.Models.ViewModels
{
    public class ShoppingCartFlowerViewModel
    {
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public List<Flower> Flowers { get; set; }
    }
}
