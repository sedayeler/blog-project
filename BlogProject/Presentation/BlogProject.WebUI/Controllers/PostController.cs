using BlogProject.Application.Features.Commands.Post.CreatePost;
using BlogProject.Application.Features.Commands.Post.DeletePost;
using BlogProject.Application.Features.Commands.Post.UpdatePost;
using BlogProject.Application.Features.Queries.Category.GetAllCategories;
using BlogProject.Application.Features.Queries.Post.GetByIdPost;
using BlogProject.Application.Features.Queries.Post.GetPostsByCategoryId;
using BlogProject.Application.Features.Queries.Post.SearchPosts;
using BlogProject.Application.Features.Queries.Post.SummarizePost;
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

        private async Task LoadCategoriesAsync(CreatePostViewModel model)
        {
            var categories = await _mediator.Send(new GetAllCategoriesQueryRequest());

            var items = categories
                .Select(c => new SelectListItem()
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .OrderBy(s => s.Text)
                .ToList();

            items.Insert(0, new SelectListItem()
            {
                Value = Guid.Empty.ToString(),
                Text = "Kategori seçiniz"
            });

            model.Categories = items;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreatePostViewModel model = new CreatePostViewModel();
            await LoadCategoriesAsync(model);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadCategoriesAsync(model);

                return View(model);
            }

            CreatePostCommandRequest request = new CreatePostCommandRequest()
            {
                Title = model.Title,
                Content = model.Content,
                ImagePath = model.ImagePath,
                CategoryId = model.CategoryId
            };

            await _mediator.Send(request);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var post = await _mediator.Send(new GetByIdPostRequest() { Id = id });

            var commentsModel = post.Comments.Select(c => new ListCommentViewModel()
            {
                Id = c.Id,
                CreatedAt = c.CreatedAt,
                Content = c.Content,
                UserId = c.UserId,
                Username = c.Username
            }).ToList();

            ListPostViewModel model = new ListPostViewModel()
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
                NewComment = new CreateCommentViewModel()
                {
                    PostId = post.Id
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
            var post = await _mediator.Send(new GetByIdPostRequest { Id = id });

            UpdatePostViewModel model = new UpdatePostViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                ImagePath = post.ImagePath,
                CategoryId = post.CategoryId,
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
        public async Task<IActionResult> Summarize([FromBody] GetPostSummaryQueryRequest request)
        {
            var response = await _mediator.Send(request);

            return Json(new { summary = response.Summary });
        }

        [HttpGet]
        public async Task<IActionResult> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return RedirectToAction("Index", "Home");

            var posts = await _mediator.Send(new GetSearchPostsQueryRequest() { Keyword = keyword });

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

            ViewBag.SearchKeyword = keyword;

            return View("~/Views/Home/Index.cshtml", model);
        }
    }
}
