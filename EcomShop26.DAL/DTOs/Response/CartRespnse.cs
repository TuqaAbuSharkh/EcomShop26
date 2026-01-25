using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.DTOs.Response
{
    public class CartRespnse
    {
        public int productId { get; set; }

        public string productName { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice => Price * Count;

    }
}
