using Shopping.Api.Repositories;
using Shopping.Api.Repositories.Models;

namespace Shopping.Api
{
    public class DatabaseInitializer
    {
        public static void Initialize(WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DbContext>();

                // Seed initial products if not already added
                if (!context.Products.Any())
                {
                    context.Products.AddRange(
                        new Product { Id = 1, Name = "Apple", Price = 1.00m },
                        new Product { Id = 2, Name = "Banana", Price = 0.50m },
                        new Product { Id = 3, Name = "Orange", Price = 0.75m },
                        new Product { Id = 4, Name = "Grapes", Price = 2.50m },
                        new Product { Id = 5, Name = "Milk", Price = 1.50m },
                        new Product { Id = 6, Name = "Bread", Price = 1.25m },
                        new Product { Id = 7, Name = "Eggs", Price = 3.00m },
                        new Product { Id = 8, Name = "Cheese", Price = 2.75m },
                        new Product { Id = 9, Name = "Butter", Price = 2.00m },
                        new Product { Id = 10, Name = "Yogurt", Price = 1.00m }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
