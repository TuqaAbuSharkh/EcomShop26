using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.DTOs.Response
{
    public class CartSummaryResponse
    {
        public List<CartRespnse> Items { get; set; }

        public decimal cartTotal => Items.Sum(i => i.TotalPrice);
    }
}
