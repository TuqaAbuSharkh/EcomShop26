using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.DTOs.Response
{
    public class ErrorDetails
    {
        public int StautsCode { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        
    }
}
