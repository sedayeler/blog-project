using BlogProject.Application.Abstractions.Services;
using MediatR;

namespace BlogProject.Application.Features.Queries.Post.GetAllPosts
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQueryRequest, List<GetAllPostsQueryResponse>>
    {
        private readonly IPostService _postService;

        public GetAllPostsQueryHandler(IPostService postService)
        {
            _postService = postService;
        }
        public async Task<List<GetAllPostsQueryResponse>> Handle(GetAllPostsQueryRequest request, CancellationToken cancellationToken)
        {
            var posts = await _postService.GetAllPostsAsync();

            return posts.Select(p => new GetAllPostsQueryResponse()
            {
                Id = p.Id,
                CreatedAt = p.CreatedAt,
                Title = p.Title,
                Content = p.Content,
                ImagePath = p.ImagePath,
                UserId = p.UserId,
                UserName = p.UserFullName,
                CategoryId = p.CategoryId,
                CategoryName = p.CategoryName
            }).ToList();
        }
    }
}
