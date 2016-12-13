using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCclientDapper.Models
{
    public class ProductModels
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}