using EcomShop26.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.DTOs.Request
{
    public class AddToCartRequest
    {
        public int ProductId { get; set; }
        public int Count { get; set; } = 1;
    }
}
