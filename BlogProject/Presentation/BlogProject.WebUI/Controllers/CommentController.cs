using BlogProject.Application.Features.Commands.Comment.CreateComment;
using BlogProject.Application.Features.Commands.Comment.DeleteComment;
using BlogProject.Application.Features.Queries.Post.GetByIdPost;
using BlogProject.WebUI.Models.Comment;
using BlogProject.WebUI.Models.Post;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebUI.Controllers
{
    public class CommentController : Controller
    {
        private readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm(Name = "NewComment")] CreateCommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var post = await _mediator.Send(new GetByIdPostRequest() { Id = model.PostId });

                var listPostModel = new ListPostViewModel()
                {
                    Id = post.Id,
                    CreatedAt = post.CreatedAt,
                    Title = post.Title,
                    Content = post.Content,
                    ImagePath = post.ImagePath,
                    AuthorId = post.UserId,
                    AuthorName = post.UserFullName, 
                    CategoryId = post.CategoryId,
                    CategoryName = post.CategoryName,
                    NewComment = model,
                    Comments = post.Comments.Select(c => new ListCommentViewModel()
                    {
                        Id = c.Id,
                        CreatedAt = c.CreatedAt,
                        Content = c.Content,
                        Username = c.Username
                    }).ToList()                  
                };

                return View("~/Views/Post/Details.cshtml", listPostModel);
            }

            CreateCommentCommandRequest request = new CreateCommentCommandRequest()
            {
                Content = model.Content,
                PostId = model.PostId
            };

            await _mediator.Send(request);

            return RedirectToAction("Details", "Post", new { Id = model.PostId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, Guid postId)
        {
            await _mediator.Send(new DeleteCommentCommandRequest() { Id = id });

            return RedirectToAction("Details", "Post", new { Id = postId });
        }
    }
}
