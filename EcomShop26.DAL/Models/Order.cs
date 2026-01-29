using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.Models
{
    public enum OrderStauts
    {
        Pending=1,
        Cancelled=2,
        Approved=3,
        Shipped=4,
        Delivered=5
    }
    public enum PaymentMethodEnum
    {
        vise=2,
        cash =1
    }
    public enum PaymentStautsEnum
    {
        UnPaid = 1,
        Paid = 2
    }
    public class Order
    {
        public int Id { get; set; }
        public OrderStauts OrderStatus { get; set; } = OrderStauts.Pending;

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public DateTime? ShippedDate { get; set; }
        public string? sessionId { get; set; }
        public string? paymentId { get; set; }

        public PaymentStautsEnum? PaymentStauts { get; set; }
        public decimal? AmountPaid { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public PaymentMethodEnum PaymentMethod { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
