using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using PersonalBlog.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace PersonalBlog.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ArticleWithTags> ArticleWithTags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string UserAdminID = Guid.NewGuid().ToString();
            string RoleAdminID = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = RoleAdminID,
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "user",
                NormalizedName = "USER"
            });

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            { 
                Id = UserAdminID,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "verxolazow@yandex.ru",
                NormalizedEmail = "VERXOLAZOW@YANDEX.RU",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "qwerty123")
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { 
                UserId = UserAdminID,
                RoleId = RoleAdminID
            });

        }
    }
}