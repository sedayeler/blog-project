using MediatR;

namespace BlogProject.Application.Features.Queries.Post.GetByIdPost
{
    public class GetByIdPostRequest : IRequest<GetByIdPostResponse>
    {
        public Guid Id { get; set; }
    }
}
