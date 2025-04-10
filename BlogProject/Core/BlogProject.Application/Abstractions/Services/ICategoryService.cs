using BlogProject.Application.DTOs;

namespace BlogProject.Application.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<List<ListCategoryDto>> GetAllCategoriesAsync();
    }
}
