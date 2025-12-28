using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.DTOs.Request
{
    public  class ProductRequest
    {
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public IFormFile MainImage { get; set; }

        public int CategoryId { get; set; }
        public List<ProductTranslaRequest> Translations { get; set; }
    }
}
