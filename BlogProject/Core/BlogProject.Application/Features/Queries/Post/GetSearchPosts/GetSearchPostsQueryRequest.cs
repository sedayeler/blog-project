using MediatR;

namespace BlogProject.Application.Features.Queries.Post.SearchPosts
{
    public class GetSearchPostsQueryRequest : IRequest<List<GetSearchPostsQueryResponse>>
    {
        public string Keyword { get; set; }
    }
}
