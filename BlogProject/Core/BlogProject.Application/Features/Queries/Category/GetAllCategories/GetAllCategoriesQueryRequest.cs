using MediatR;

namespace BlogProject.Application.Features.Queries.Category.GetAllCategories
{
    public class GetAllCategoriesQueryRequest : IRequest<List<GetAllCategoriesQueryResponse>>
    {
    }
}
