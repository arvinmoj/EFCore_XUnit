using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.EfStructures.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

    }
}
