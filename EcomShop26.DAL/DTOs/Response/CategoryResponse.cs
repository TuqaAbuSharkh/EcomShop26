using EcomShop26.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.DTOs.Response
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public Status Status { get; set; }
      
        public List<CategoryTranslResponse> Translations { get; set; }
    }
}
