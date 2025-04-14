using MediatR;

namespace BlogProject.Application.Features.Queries.Post.GetPostsByCategoryId
{
    public class GetPostsByCategoryIdQueryRequest : IRequest<List<GetPostsByCategoryIdQueryResponse>>
    {
        public Guid CategoryId { get; set; }
    }
}
