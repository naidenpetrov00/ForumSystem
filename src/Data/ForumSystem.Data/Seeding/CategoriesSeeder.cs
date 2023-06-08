namespace ForumSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ForumSystem.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<(string Name, string ImageUrl)>()
            {
            ("Sport", "https://www.tvevropa.com/wp-content/uploads/2019/12/sport.jpg"),
            ("COVID", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQUippbW0wQNFj8beGjqB0c_DWu9SJj7Zirow&usqp=CAU"),
            ("News", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRjCAfVgATBaPFFWX2WWJF6x-gVW4P1mdvfKA&usqp=CAU"),
            ("Music", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTdyuUH-jJVNQkw61TNppZbgDFvQDmhTEMjUQ&usqp=CAU"),
            ("Cats", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ_dFNmibo2ZUzFvE_ycE-hSDovIsEerbkbmA&usqp=CAU"),
            };
            foreach (var category in categories)
            {
                await dbContext.Categories.AddAsync(new Category
                {
                    Name = category.Name,
                    Description = category.Name,
                    Title = category.Name,
                    ImageUrl = category.ImageUrl,
                });
            }
        }
    }
}
