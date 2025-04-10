using BlogProject.Domain.Entities;
using BlogProject.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace BlogProject.Persistence.SeedData
{
    public static class CategorySeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BlogProjectDbContext>();

            if (!context.categories.Any())
            {
                var categories = new List<Category>
            {
                new Category { Id = Guid.NewGuid(), Name = "Bilim" },
                new Category { Id = Guid.NewGuid(), Name = "Eğitim" },
                new Category { Id = Guid.NewGuid(), Name = "Finans" },
                new Category { Id = Guid.NewGuid(), Name = "Haber & Gündem" },
                new Category { Id = Guid.NewGuid(), Name = "Kültür & Sanat" },
                new Category { Id = Guid.NewGuid(), Name = "Sağlık" },
                new Category { Id = Guid.NewGuid(), Name = "Spor" },
                new Category { Id = Guid.NewGuid(), Name = "Teknoloji" },
                new Category { Id = Guid.NewGuid(), Name = "Yazılım" },
                new Category { Id = Guid.NewGuid(), Name = "Yaşam" }
            };

                await context.categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }
        }
    }

}
