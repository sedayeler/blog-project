using MediatR;

namespace BlogProject.Application.Features.Queries.Post.GetAllPosts
{
    public class GetAllPostsQueryRequest : IRequest<List<GetAllPostsQueryResponse>>
    {
    }
}
