using EcomShop26.DAL.DTOs.Response;
using EcomShop26.DAL.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.BLL.MapsterConfigration
{
    public static class MapsterConfig
    {

        public static void MapsterConfigRegister()
        {
            TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
                .Map(dest =>dest.CreatedBy,source=> source.User.UserName).TwoWays();

            TypeAdapterConfig<Category, CategoryUserResponse>.NewConfig()
                .Map(dest => dest.Name, source => source.Translations.Where(t => t.Language == MapContext.Current.Parameters["lang"].ToString())
                .Select(t => t.Name).FirstOrDefault()).TwoWays();

        }
    }
}
