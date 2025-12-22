using EcomShop26.DAL.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.DTOs.Request
{
    public  class ResetPasswordRequest 
    {

        public string code { get; set; }
        public string newPassword { get; set; }

        public string Email { get; set; }
    }
}
