using EcomShop26.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcomShop26.DAL.Data
{
    public class ApplicationDbContext :IdentityDbContext<ApplicationUser>
    {
     
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
           : base(options)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> categoryTranslations { get; set; }
        public IHttpContextAccessor HttpContextAccessor { get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

        }


        public override int SaveChanges()
        {
            var entres = ChangeTracker.Entries<BaseModel>();
            var currentUserId = HttpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            foreach(var entry in entres)
            {
                if(entry.State == EntityState.Added)
                {
                    entry.Property(x => x.CreatedBy).CurrentValue = currentUserId;
                    entry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;

                }else if(entry.State == EntityState.Modified)
                {
                    entry.Property(x => x.UpdatedBy).CurrentValue = currentUserId;
                    entry.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChanges();
        }


    }
}
