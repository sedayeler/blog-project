using BlogProject.Application.Abstractions.Services;
using BlogProject.Application.DTOs;
using BlogProject.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Persistence.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly BlogProjectDbContext _context;

        public CategoryService(BlogProjectDbContext context)
        {
            _context = context;
        }

        public async Task<List<ListCategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _context.categories
                .OrderBy(c => c.Name)
                .Select(c => new ListCategoryDto()
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync();

            return categories;
        }
    }
}
