using BlogProject.Application.Abstractions.Services;
using BlogProject.Application.DTOs;
using MediatR;

namespace BlogProject.Application.Features.Commands.Post.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommandRequest, CreatePostCommandResponse>
    {
        private readonly IPostService _postService;

        public CreatePostCommandHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<CreatePostCommandResponse> Handle(CreatePostCommandRequest request, CancellationToken cancellationToken)
        {
            await _postService.CreatePostAsync(new CreatePostDto()
            {
                Title = request.Title,
                Content = request.Content,
                ImagePath = request.ImagePath,
                CategoryId = request.CategoryId
            });

            return new CreatePostCommandResponse();
        }
    }
}
