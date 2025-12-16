using EcomShop26.DAL.Data;
using EcomShop26.DAL.DTOs.Request;
using EcomShop26.DAL.DTOs.Response;
using EcomShop26.DAL.Models;
using EcomShop26.PL.Resourses;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace EcomShop26.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CategoriesController(ApplicationDbContext context,IStringLocalizer<SharedResource> Localizer)
        {
            _context = context;
            _localizer = Localizer;
        }
        [HttpGet("")]

        public IActionResult index()
        {
            var categories = _context.Categories.Include(c=>c.Translations).ToList();
            var response = categories.Adapt <List< CategoryResponse >> ();
            return Ok(new {message = _localizer["Success"].Value,response });
        }
        [HttpPost("")]
        public IActionResult Creat(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok(new { message = _localizer["Success"].Value });
        }
    }
}
