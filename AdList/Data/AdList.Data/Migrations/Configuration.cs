namespace AdList.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using AdList.Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;

            // TODO: Remove in production
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            // Get managers
            var userManager = new UserManager<User>(new UserStore<User>(context));
            var rolesManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Create roles
            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                var role = new IdentityRole { Name = "Administrator" };
                rolesManager.Create(role);
            }

            // Create users
            if (!context.Users.Any(u => u.UserName == "admin@abv.bg"))
            {
                var user = new User { UserName = "admin@abv.bg", Email = "admin@abv.bg" };
                userManager.Create(user, password: "111111");
                userManager.AddToRole(user.Id, "Administrator");
            }

            if (!context.Users.Any(u => u.UserName.Contains("user00")))
            {
                var user = new User()
                {
                    UserName = "user001@abv.bg",
                    Email = "user001@abv.bg",
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    City = "Sofia",
                    ImageUrl = "default-avatar.jpg",
                    PhoneNumber = "0885-123-456",
                };
                userManager.Create(user, password: "111111");
            }

            if (!context.Categories.Any())
            {
                var categories = new List<Category>()
                {
                    new Category() { Name = "Phones" },
                    new Category() { Name = "Computers" },
                    new Category() { Name = "Cameras" },
                    new Category() { Name = "Sports" },
                    new Category() { Name = "Furniture" },
                    new Category() { Name = "Art" },
                    new Category() { Name = "Books"  },
                    new Category() { Name = "Music"  },
                    new Category() { Name = "Clothing"  },
                    new Category() { Name = "Watches"  },
                    new Category() { Name = "Toys"  },
                    new Category() { Name = "Car-parts" },
                    new Category() { Name = "Baby-gear"  },
                    new Category() { Name = "Others" },
                };

                context.Categories.AddOrUpdate(categories.ToArray());
                context.SaveChanges();
            }

            // Ads
            if (context.Ads.Count() < 3)
            {
                // ATTENTION: DELETES ALL ADS
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Ads]");

                var ads = new List<Ad>()
                {
                    new Ad() { 
                        Title = "iPhone 5S like new",
                        Description = "I am buying the new iPhone 6, so selling this one.",
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Phones"),
                        Price = 300,
                        Featured = true,
                        Author = context.Users.FirstOrDefault(u => u.UserName == "user001@abv.bg"),
                        ImageUrl = "iPhone-5-White-Design.jpg"
                    },
                    new Ad() { 
                        Title = "MacBook Air 13\"",
                        Description = "Selling my old mac...",
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Computers"),
                        Price = 890,
                        Featured = true,
                        Author = context.Users.FirstOrDefault(u => u.UserName == "user001@abv.bg"),
                        ImageUrl = "MacBook_Air_13-inch_35330106_01_620x433.jpg"
                    },
                    new Ad() { 
                        Title = "Monet reproduction",
                        Description = "I bought this from an online auction for $1000.",
                        Category = context.Categories.FirstOrDefault(c => c.Name == "Art"),
                        Price = 500,
                        Featured = true,
                        Author = context.Users.FirstOrDefault(u => u.UserName == "user001@abv.bg"),
                        ImageUrl = "monet001.jpg"
                    },
                };

                context.Ads.AddOrUpdate(ads.ToArray());
                context.SaveChanges();
            }            
        }
    }
}
