using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.Models
{
    public class BaseModel
    {
        public int Id { get; set; }

        public DateTime CreatedAt { set; get; } = DateTime.UtcNow;

        public Status Status { get; set; }

    }
}
