using BlogProject.Application.Abstractions.Services;
using MediatR;

namespace BlogProject.Application.Features.Queries.Post.SearchPosts
{
    public class GetSearchPostsQueryHandler : IRequestHandler<GetSearchPostsQueryRequest, List<GetSearchPostsQueryResponse>>
    {
        private readonly IPostService _postService;

        public GetSearchPostsQueryHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<List<GetSearchPostsQueryResponse>> Handle(GetSearchPostsQueryRequest request, CancellationToken cancellationToken)
        {
            var posts = await _postService.GetSearchPostsAsync(request.Keyword);

            return posts.Select(p => new GetSearchPostsQueryResponse()
            {
                Id = p.Id,
                CreatedAt = p.CreatedAt,
                Title = p.Title,
                Content = p.Content,
                ImagePath = p.ImagePath,
                UserId = p.UserId,
                UserFullName = p.UserFullName,
                CategoryId = p.CategoryId,
                CategoryName = p.CategoryName
            }).ToList();
        }
    }
}
