using BlogProject.Application.Abstractions.Services;
using MediatR;

namespace BlogProject.Application.Features.Queries.Post.GetByIdPost
{
    public class GetByIdPostHandler : IRequestHandler<GetByIdPostRequest, GetByIdPostResponse>
    {
        private readonly IPostService _postService;

        public GetByIdPostHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<GetByIdPostResponse> Handle(GetByIdPostRequest request, CancellationToken cancellationToken)
        {
            var post = await _postService.GetByIdPostAsync(request.Id);

            return new GetByIdPostResponse()
            {
                Id = post.Id,
                CreatedAt = post.CreatedAt,
                Title = post.Title,
                Content = post.Content,
                ImagePath = post.ImagePath,
                AuthorName = post.AuthorName,
                CategoryName = post.CategoryName
            };
        }
    }
}
