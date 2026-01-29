using EcomShop26.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EcomShop26.DAL.DTOs.Response
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public OrderStauts OrderStatus { get; set; }
        public PaymentStautsEnum? PaymentStauts { get; set; }
        public decimal AmountPaid { get; set; }
        public string UserName { get; set; }

    }
}
