using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.DTOs.Response
{
    public class PagenatedResponse<T>
    {
        public int TotalCount { get; set; }

        public int page { get; set; }
        public int limit { get; set; }
        public List<T> Data { get; set; }

    }
}
