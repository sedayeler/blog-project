using BlogProject.Application.Abstractions.Services;
using BlogProject.Application.DTOs;
using BlogProject.Application.DTOs.Comment;
using BlogProject.Application.Repositories;
using BlogProject.Domain.Entities;
using BlogProject.Domain.Entities.Identity;
using BlogProject.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogProject.Persistence.Services
{
    public class PostService : IPostService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPostReadRepository _postReadRepository;
        private readonly IPostWriteRepository _postWriteRepository;
        private readonly UserManager<User> _userManager;
        private readonly BlogProjectDbContext _context;
        private readonly IAiService _aiService;
        private readonly IMailService _mailService;

        public PostService(IHttpContextAccessor httpContextAccessor, IPostReadRepository postReadRepository, IPostWriteRepository postWriteRepository, UserManager<User> userManager, BlogProjectDbContext context, IAiService aiService, IMailService mailService)
        {
            _httpContextAccessor = httpContextAccessor;
            _postReadRepository = postReadRepository;
            _postWriteRepository = postWriteRepository;
            _userManager = userManager;
            _context = context;
            _aiService = aiService;
            _mailService = mailService;
        }

        public async Task CreatePostAsync(CreatePostDto dto)
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (string.IsNullOrWhiteSpace(username))
                throw new Exception("Kullanıcı doğrulama hatası.");

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            Post post = new Post()
            {
                Title = dto.Title,
                Content = dto.Content,
                ImagePath = dto.ImagePath,
                UserId = user.Id,
                CategoryId = dto.CategoryId
            };

            await _postWriteRepository.AddAsync(post);
            await _postWriteRepository.SaveAsync();
        }

        public async Task UpdatePostAsync(UpdatePostDto dto)
        {
            Post post = await _postReadRepository.GetByIdAsync(dto.Id);
            if (post == null)
                throw new Exception("Gönderi bulunamadı.");

            post.Title = dto.Title;
            post.Content = dto.Content;
            post.ImagePath = dto.ImagePath;
            post.CategoryId = dto.CategoryId;

            _postWriteRepository.Update(post);
            await _postWriteRepository.SaveAsync();
        }

        public async Task DeletePostAsync(Guid id)
        {
            Post post = await _postReadRepository.GetByIdAsync(id);
            if (post == null)
                throw new Exception("Gönderi bulunamadı.");

            _postWriteRepository.Remove(post);
            await _postWriteRepository.SaveAsync();
        }

        public async Task<List<ListPostDto>> GetAllPostsAsync()
        {
            List<Post> posts = await _postReadRepository.GetAll()
                .Include(p => p.User)
                .Include(p => p.Category)
                .ToListAsync();

            List<ListPostDto> postDtos = posts.Select(p => new ListPostDto()
            {
                Id = p.Id,
                CreatedAt = p.CreatedAt,
                Title = p.Title,
                Content = p.Content,
                ImagePath = p.ImagePath,
                UserId = p.User.Id,
                UserFullName = p.User.FullName,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name
            }).ToList();

            return postDtos;
        }

        public async Task<ListPostDto> GetByIdPostAsync(Guid id)
        {
            var post = await _context.posts
                .Include(p => p.User)
                .Include(p => p.Category)
                .Include(p => p.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
                throw new Exception("Gönderi bulunamadı.");

            var commentDtos = post.Comments
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new ListCommentDto()
                {
                    Id = c.Id,
                    CreatedAt = c.CreatedAt,
                    Content = c.Content,
                    UserId = c.UserId,
                    Username = c.User.UserName,
                }).ToList();

            ListPostDto postDto = new ListPostDto()
            {
                Id = post.Id,
                CreatedAt = post.CreatedAt,
                Title = post.Title,
                Content = post.Content,
                ImagePath = post.ImagePath,
                UserId = post.UserId,
                UserFullName = post.User.FullName,
                CategoryId = post.CategoryId,
                CategoryName = post.Category.Name,
                Comments = commentDtos
            };

            return postDto;
        }

        public async Task<List<ListPostDto>> GetPostsByCategoryIdAsync(Guid categoryId)
        {
            var posts = await _context.posts
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Category)
                .Include(p => p.User)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
            if (posts == null)
                throw new Exception("Gönderi bulunamadı.");

            List<ListPostDto> postDtos = posts.Select(p => new ListPostDto()
            {
                Id = p.Id,
                CreatedAt = p.CreatedAt,
                Title = p.Title,
                Content = p.Content,
                ImagePath = p.ImagePath,
                UserId = p.UserId,
                UserFullName = p.User.FullName,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name
            }).ToList();

            return postDtos;
        }

        public async Task<string> GetPostSummaryAsync(Guid id)
        {
            var post = await _postReadRepository.GetByIdAsync(id);
            if (post == null)
                throw new Exception("Gönderi bulunamadı.");

            if (post.Content == null)
                throw new Exception("Gönderi içeriği boş.");

            return await _aiService.SummarizeTextAsync(post.Content);
        }

        public async Task<List<ListPostDto>> GetSearchPostsAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                throw new Exception("Aranacak kelime yok.");

            keyword = keyword.ToLower();

            return await _postReadRepository.GetAll()
                .Where(p => p.Title.Contains(keyword) || p.Content.Contains(keyword) || p.User.FullName.Contains(keyword) || p.Category.Name.Contains(keyword))
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new ListPostDto()
                {
                    Id = p.Id,
                    CreatedAt = p.CreatedAt,
                    Title = p.Title,
                    Content = p.Content,
                    ImagePath = p.ImagePath,
                    UserId = p.UserId,
                    UserFullName = p.User.FullName,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name
                }).ToListAsync();
        }
    }
}