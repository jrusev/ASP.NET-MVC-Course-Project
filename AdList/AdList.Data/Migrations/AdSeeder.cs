namespace AdList.Data.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using AdList.Data.Models;

    internal class AdSeeder
    {
        private const int SeedAdsCount = 15;

        public static void Seed(ApplicationDbContext context)
        {
            if (context.Ads.Count() < SeedAdsCount)
            {
                // ATTENTION: DELETES ALL ADS
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Ads]");

                var ads = new List<Ad>()
                {
                    new Ad() { 
                        Title = "iPhone 5S like new",
                        Description = "I am buying the new iPhone 6, so selling this one.",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Phones").Id,
                        Price = 300,
                        Featured = true,
                        Author = context.Users.FirstOrDefault(u => u.UserName == "user001@abv.bg"),
                        ImageUrl = "iPhone-5-White-Design.jpg"
                    },
                    new Ad() { 
                        Title = "MacBook Air 13\"",
                        Description = "Selling my old mac...",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Computers").Id,
                        Price = 890,
                        Featured = true,
                        Author = context.Users.FirstOrDefault(u => u.UserName == "user001@abv.bg"),
                        ImageUrl = "MacBook_Air_13-inch_35330106_01_620x433.jpg"
                    },
                    new Ad() { 
                        Title = "Monet reproduction",
                        Description = "I bought this from an online auction for $1000.",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Art").Id,
                        Price = 500,
                        Featured = true,
                        Author = context.Users.FirstOrDefault(u => u.UserName == "user001@abv.bg"),
                        ImageUrl = "monet001.jpg"
                    },
                    new Ad() { 
                        Title = "BMX Bycicle",
                        Description = "Check this bike!",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Sports").Id,
                        Price = 150,
                        Featured = false,
                        Author = context.Users.FirstOrDefault(u => u.UserName == "user001@abv.bg"),
                        ImageUrl = "s1600_92037130_1261063239.jpg"
                    },
                    new Ad() { 
                        Title = "Winter tyres",
                        Description = "A little wear and tear...",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Car-parts").Id,
                        Price = 180,
                        Featured = false,
                        Author = context.Users.FirstOrDefault(u => u.UserName == "user001@abv.bg"),
                        ImageUrl = "$_20.JPG"
                    },
                    new Ad() { 
                        Title = "Seiko Kinnetic 2K",
                        Description = "A real luxury item, well preserved.",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Watches").Id,
                        Price = 400,
                        Featured = false,
                        Author = context.Users.FirstOrDefault(u => u.UserName == "user001@abv.bg"),
                        ImageUrl = "SNL001P-3.jpg"
                    },
                    new Ad() { 
                        Title = "Luxury Lamp",
                        Description = "You can't find this piece of art any where.",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Furniture").Id,
                        Price = 75,
                        Featured = false,
                        Author = context.Users.FirstOrDefault(u => u.UserName == "user001@abv.bg"),
                        ImageUrl = "Spherical-Luxury-Lamp001.jpg"
                    },
                    new Ad() { 
                        Title = "Baby stroller",
                        Description = "Used for two of my babies.",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Baby-gear").Id,
                        Price = 380,
                        Featured = false,
                        Author = context.Users.FirstOrDefault(u => u.UserName == "user001@abv.bg"),
                        ImageUrl = "4moms-Origami-Stroller.jpg"
                    },
                    new Ad() { 
                        Title = "Encyclopedia Britannica",
                        Description = "A rare collection, got it from the UK.",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Books").Id,
                        Price = 30,
                        Featured = true,
                        Author = context.Users.FirstOrDefault(u => u.UserName == "user001@abv.bg"),
                        ImageUrl = "britannica001.jpg"
                    },
                    new Ad() { 
                        Title = "Canon 600D",
                        Description = "I wanted to shoot movies with this one, but the lenses are expensive :(",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Cameras").Id,
                        Price = 500,
                        Featured = true,
                        Author = context.Users.FirstOrDefault(u => u.UserName == "user001@abv.bg"),
                        ImageUrl = "canon600D_001.jpg"
                    },
                    new Ad() { 
                        Title = "Awesome Mix Tape",
                        Description = "If you've watched the movie, you will know what this is ;)",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Music").Id,
                        Price = 50,
                        Featured = true,
                        Author = context.Users.FirstOrDefault(u => u.UserName == "user001@abv.bg"),
                        ImageUrl = "mixtape001.jpg"
                    },
                    new Ad() { 
                        Title = "Top Gun Jacket",
                        Description = "For real movie fans, Tom Cruise weared this!",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Clothing").Id,
                        Price = 200,
                        Featured = true,
                        Author = context.Users.FirstOrDefault(u => u.UserName == "user001@abv.bg"),
                        ImageUrl = "top-gun-jacket-001.jpg"
                    },
                    new Ad() { 
                        Title = "Woody",
                        Description = "OK, this is Woodie action figure, wel preserved.",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Toys").Id,
                        Price = 20,
                        Featured = false,
                        Author = context.Users.FirstOrDefault(u => u.UserName == "user001@abv.bg"),
                        ImageUrl = "woody-toy-box.jpg"
                    },
                    new Ad() { 
                        Title = "Sony Vaio",
                        Description = "I got a new MacBook Air, so I don't need this.",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Computers").Id,
                        Price = 1430,
                        Featured = true,
                        Author = context.Users.FirstOrDefault(u => u.UserName == "user001@abv.bg"),
                        ImageUrl = "sony-pro-5.jpg"
                    },
                    new Ad() { 
                        Title = "Wilson nSix 90",
                        Description = "I got this as a present, but I use Babolat.",
                        CategoryId = context.Categories.FirstOrDefault(c => c.Name == "Others").Id,
                        Price = 90,
                        Featured = true,
                        Author = context.Users.FirstOrDefault(u => u.UserName == "user001@abv.bg"),
                        ImageUrl = "Wilson-nSix-One-Tour-90.jpg"
                    },
                };

                context.Ads.AddOrUpdate(ads.ToArray());
                context.SaveChanges();
            }            
        }
    }
}
