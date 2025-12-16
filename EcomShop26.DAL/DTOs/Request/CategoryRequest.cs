using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.DTOs.Request
{
    public class CategoryRequest
    {
       public List<CategoryTranslaRequest> Translations { get; set; }
    }
}
