using EcomShop26.DAL.DTOs.Request;
using EcomShop26.DAL.DTOs.Response;
using EcomShop26.DAL.Models;
using EcomShop26.DAL.Repository;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository productRepository,IFileService fileService)
        {
           _productRepository = productRepository;
            _fileService = fileService;
        }
        public async Task<ProductResponse> CreatProduct(ProductRequest request)
        {
            var product = request.Adapt<Product>();
            if (request.MainImage != null)
            {
                var imagePath = await _fileService.UploadAsync(request.MainImage);
                product.MainImage = imagePath;
            }

            if (request.SubImages != null)
            {
                product.SubImages = new List<ProductImage>();
                foreach(var file in request.SubImages)
                {
                    var imagePath = await _fileService.UploadAsync(file);
                    product.SubImages.Add(new ProductImage { ImageName = imagePath });
                }
            }


                await _productRepository.CreatAsync(product);
            return product.Adapt<ProductResponse>();
        }


        public async Task<List<ProductUserResponse>> GetAllProductsForUser(string lang = "en",int page =1,
            int limit = 4,string? search= null)
        {
            var query =  _productRepository.Query();
            if(search is not null)
            {
                query = query.Where(p => p.Translations.Any(t => t.Language == lang && t.Name.Contains(search)|| t.Description.Contains(search)));
            }

            var totalCount = query.CountAsync();
            query = query.Skip((page - 1) * limit).Take(limit);

            var response = query.BuildAdapter().AddParameters("lang", lang).AdaptToType<List<ProductUserResponse>>();


            return response;
        }


        public async Task<List<ProductResponse>> GetAllProductsForAdmin()
        {
            var categories = await _productRepository.GetAllAsync();


            var response = categories.Adapt<List<ProductResponse>>();
            return response;
        }


        public async Task<ProductUserDetails> GetAllProductsDetailsForUser(int id,string lang = "en")
        {
            var products = await _productRepository.FindByIdAsync(id);
            var response = products.BuildAdapter().AddParameters("lang", lang).AdaptToType<ProductUserDetails>();


            return response;
        }


    }
}
