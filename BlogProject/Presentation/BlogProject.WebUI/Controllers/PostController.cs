using BlogProject.Application.Features.Commands.Post.CreatePost;
using BlogProject.Application.Features.Commands.Post.DeletePost;
using BlogProject.Application.Features.Commands.Post.SummarizePost;
using BlogProject.Application.Features.Commands.Post.UpdatePost;
using BlogProject.Application.Features.Queries.Category.GetAllCategories;
using BlogProject.Application.Features.Queries.Post.GetByIdPost;
using BlogProject.Application.Features.Queries.Post.GetPostsByCategoryId;
using BlogProject.WebUI.Models.Comment;
using BlogProject.WebUI.Models.Post;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace BlogProject.WebUI.Controllers
{
    public class PostController : Controller
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        private async Task PopulateCategoriesAsync(CreatePostViewModel viewModel)
        {
            var categories = await _mediator.Send(new GetAllCategoriesQueryRequest());

            viewModel.Categories = categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .OrderBy(s => s.Text)
                .ToList();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreatePostViewModel viewModel = new CreatePostViewModel();
            await PopulateCategoriesAsync(viewModel);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCategoriesAsync(viewModel);

                return View(viewModel);
            }

            CreatePostCommandRequest request = new CreatePostCommandRequest()
            {
                Title = viewModel.Title,
                Content = viewModel.Content,
                ImagePath = viewModel.ImagePath,
                CategoryId = viewModel.CategoryId
            };

            await _mediator.Send(request);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var response = await _mediator.Send(new GetByIdPostRequest() { Id = id });

            var commentsModel = response.Comments.Select(c => new ListCommentViewModel()
            {
                Id = c.Id,
                CreatedAt = c.CreatedAt,
                Content = c.Content,
                UserId = c.UserId,
                Username = c.Username
            }).ToList();

            ListPostViewModel model = new ListPostViewModel()
            {
                Id = response.Id,
                CreatedAt = response.CreatedAt,
                Title = response.Title,
                Content = response.Content,
                ImagePath = response.ImagePath,
                AuthorId = response.UserId,
                AuthorName = response.UserName,
                CategoryId = response.CategoryId,
                CategoryName = response.CategoryName,
                NewComment = new CreateCommentViewModel()
                {
                    PostId = response.Id
                },
                Comments = commentsModel ?? new List<ListCommentViewModel>()
            };

            Guid? currentUserId = null;

            if (User.Identity.IsAuthenticated)
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (Guid.TryParse(userIdString, out var parsedId))
                    currentUserId = parsedId;
            }

            ViewBag.CurrentUserId = currentUserId;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var response = await _mediator.Send(new GetByIdPostRequest { Id = id });

            UpdatePostViewModel model = new UpdatePostViewModel()
            {
                Id = response.Id,
                Title = response.Title,
                Content = response.Content,
                ImagePath = response.ImagePath,
                CategoryId = response.CategoryId,
            };

            ViewBag.Categories = new SelectList(await _mediator.Send(new GetAllCategoriesQueryRequest()), "Id", "Name", model.CategoryId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _mediator.Send(new GetAllCategoriesQueryRequest()), "Id", "Name", model.CategoryId);

                return View(model);
            }

            UpdatePostCommandRequest request = new UpdatePostCommandRequest()
            {
                Id = model.Id,
                Title = model.Title,
                Content = model.Content,
                ImagePath = model.ImagePath,
                CategoryId = model.CategoryId
            };

            await _mediator.Send(request);

            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeletePostCommandRequest() { Id = id });

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> Category(Guid categoryId)
        {
            var posts = await _mediator.Send(new GetPostsByCategoryIdQueryRequest() { CategoryId = categoryId });

            List<ListPostViewModel> model = posts
            .Select(p => new ListPostViewModel()
            {
                Id = p.Id,
                CreatedAt = p.CreatedAt,
                Title = p.Title,
                Content = p.Content.Length > 200 ? p.Content.Substring(0, 200) + "..." : p.Content,
                ImagePath = p.ImagePath,
                AuthorId = p.UserId,
                AuthorName = p.UserFullName,
                CategoryId = p.CategoryId,
                CategoryName = p.CategoryName
            }).ToList();

            if (model.Any())
                ViewBag.CategoryName = model.First().CategoryName;
            else
                ViewBag.CategoryName = "Gönderiler";

            return View("~/Views/Home/Index.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> SummarizeWithAI([FromBody] SummarizePostCommandRequest request)
        {
            var result = await _mediator.Send(request);

            return Json(new { summary = result.Summary });
        }
    }
}
