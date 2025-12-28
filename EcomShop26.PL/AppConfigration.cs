using EcomShop26.BLL.Services;
using EcomShop26.DAL.Repository;
using EcomShop26.DAL.Utls;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace EcomShop26.PL
{
    public class AppConfigration
    {
        public static void Config(IServiceCollection Services)
        {

            Services.AddScoped<ICategoryRepository, CategoryRepository>();
            Services.AddScoped<ICategoryService, CategoryService>();
            Services.AddTransient<IEmailSender, EmailSender>();
            Services.AddScoped<ISeedData, RoleSeedData>();
            Services.AddScoped<ISeedData, UserSeedData>();
            Services.AddScoped<IAuthinticationService, AuthenticationService>();

        }
    }
}
