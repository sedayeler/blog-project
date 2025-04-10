using BlogProject.Application.Abstractions.Services;
using MediatR;

namespace BlogProject.Application.Features.Queries.Category.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQueryRequest, List<GetAllCategoriesQueryResponse>>
    {
        private readonly ICategoryService _categoryService;

        public GetAllCategoriesQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<List<GetAllCategoriesQueryResponse>> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            return categories.Select(c => new GetAllCategoriesQueryResponse()
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }
    }
}
