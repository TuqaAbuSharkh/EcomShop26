using EcomShop26.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.Repository
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category Creat(Category request);
    }
}
