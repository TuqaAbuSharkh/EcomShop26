using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.DTOs.Response
{
    public class ChechoutResponse :BaseRespose
    {
        public string? Url { get; set; }
        public string? PaymentId { get; set; }

    }
}
