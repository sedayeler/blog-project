using BlogProject.Application.Abstractions.Services;
using MediatR;

namespace BlogProject.Application.Features.Queries.Post.GetPostsByCategoryId
{
    public class GetPostsByCategoryIdQueryHandler : IRequestHandler<GetPostsByCategoryIdQueryRequest, List<GetPostsByCategoryIdQueryResponse>>
    {
        private readonly IPostService _postService;

        public GetPostsByCategoryIdQueryHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<List<GetPostsByCategoryIdQueryResponse>> Handle(GetPostsByCategoryIdQueryRequest request, CancellationToken cancellationToken)
        {
            var posts = await _postService.GetPostsByCategoryIdAsync(request.CategoryId);

            return posts.Select(p => new GetPostsByCategoryIdQueryResponse()
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
