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

            AdSeeder.Seed(context);        
        }
    }
}
