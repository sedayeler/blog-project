using BlogProject.Application.Abstractions.Services;
using MediatR;

namespace BlogProject.Application.Features.Commands.Comment.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommandRequest, DeleteCommentCommandResponse>
    {
        private readonly ICommentService _commentService;

        public DeleteCommentCommandHandler(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<DeleteCommentCommandResponse> Handle(DeleteCommentCommandRequest request, CancellationToken cancellationToken)
        {
            await _commentService.DeleteCommentAsync(request.Id);

            return new DeleteCommentCommandResponse();
        }
    }
}
