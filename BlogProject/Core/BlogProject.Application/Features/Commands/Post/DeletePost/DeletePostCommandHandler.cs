using BlogProject.Application.Abstractions.Services;
using MediatR;

namespace BlogProject.Application.Features.Commands.Post.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommandRequest, DeletePostCommandResponse>
    {
        private readonly IPostService _postService;

        public DeletePostCommandHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<DeletePostCommandResponse> Handle(DeletePostCommandRequest request, CancellationToken cancellationToken)
        {
            await _postService.DeletePostAsync(request.Id);

            return new DeletePostCommandResponse();
        }
    }
}
