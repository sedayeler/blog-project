using BlogProject.Application.Abstractions.Services;
using MediatR;

namespace BlogProject.Application.Features.Commands.Post.SummarizePost
{
    public class SummarizePostCommandHandler : IRequestHandler<SummarizePostCommandRequest, SummarizePostCommandResponse>
    {
        private readonly IPostService _postService;

        public SummarizePostCommandHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<SummarizePostCommandResponse> Handle(SummarizePostCommandRequest request, CancellationToken cancellationToken)
        {
            string summary = await _postService.SummarizePostAsync(request.Id);

            return new SummarizePostCommandResponse()
            {
                Summary = summary
            };
        }
    }
}
