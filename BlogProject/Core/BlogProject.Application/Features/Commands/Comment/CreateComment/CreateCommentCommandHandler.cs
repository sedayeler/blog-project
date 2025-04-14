using BlogProject.Application.Abstractions.Services;
using BlogProject.Application.DTOs.Comment;
using MediatR;

namespace BlogProject.Application.Features.Commands.Comment.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommandRequest, CreateCommentCommandResponse>
    {
        private readonly ICommentService _commentService;

        public CreateCommentCommandHandler(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<CreateCommentCommandResponse> Handle(CreateCommentCommandRequest request, CancellationToken cancellationToken)
        {
            await _commentService.CreateCommentAsync(new CreateCommentDto()
            {
                Content = request.Content,
                PostId = request.PostId
            });

            return new CreateCommentCommandResponse();
        }
    }
}
