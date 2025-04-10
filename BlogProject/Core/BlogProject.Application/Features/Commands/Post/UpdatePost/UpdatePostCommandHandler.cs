using BlogProject.Application.Abstractions.Services;
using BlogProject.Application.DTOs;
using MediatR;

namespace BlogProject.Application.Features.Commands.Post.UpdatePost
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommandRequest, UpdatePostCommandResponse>
    {
        private readonly IPostService _postService;

        public UpdatePostCommandHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<UpdatePostCommandResponse> Handle(UpdatePostCommandRequest request, CancellationToken cancellationToken)
        {
            await _postService.UpdatePostAsync(new UpdatePostDto()
            {
                Id = request.Id,
                Title = request.Title,
                Content = request.Content,
                ImagePath = request.ImagePath,
                CategoryId = request.CategoryId
            });

            return new UpdatePostCommandResponse();
        }
    }
}
