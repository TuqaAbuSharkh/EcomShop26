using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.DTOs.Request
{
    public class ProductTranslaRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string Language { get; set; } = "en";

    }
}
