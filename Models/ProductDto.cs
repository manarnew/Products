using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Models
{
    public class ProductDto
    {
        public required string Name { get; set; }
        public  string? Description { get; set; }
        public required decimal Price { get; set; }
    }
}