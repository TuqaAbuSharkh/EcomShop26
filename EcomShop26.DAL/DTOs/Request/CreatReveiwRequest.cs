using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.DTOs.Request
{
    public class CreatReveiwRequest
    {
        [Range(1,5)]
        [Required]
        public int Rating { get; set; }
        [Required]
        [MinLength(5)]
        public string Comment { get; set; }
    }
}
