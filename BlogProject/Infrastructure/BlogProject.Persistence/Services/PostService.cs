using BlogProject.Application.Abstractions.Services;
using BlogProject.Application.DTOs;
using BlogProject.Application.Repositories;
using BlogProject.Domain.Entities;
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

        public PostService(IHttpContextAccessor httpContextAccessor, IPostReadRepository postReadRepository, IPostWriteRepository postWriteRepository, UserManager<User> userManager, BlogProjectDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _postReadRepository = postReadRepository;
            _postWriteRepository = postWriteRepository;
            _userManager = userManager;
            _context = context;
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
                AuthorId = p.User.Id,
                AuthorName = p.User.FullName,
                //CategoryId = p.CategoryId,
                //CategoryName = p.Category.Name
                Category = p.Category
            }).ToList();

            return postDtos;
        }

        public async Task<ListPostDto> GetByIdPostAsync(Guid id)
        {
            var post = await _context.posts
                .Include(p => p.User)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
                throw new Exception("Gönderi bulunamadı.");

            ListPostDto postDto = new ListPostDto()
            {
                Id = post.Id,
                CreatedAt = post.CreatedAt,
                Title = post.Title,
                Content = post.Content,
                ImagePath = post.ImagePath,
                AuthorId = post.UserId,
                AuthorName = post.User.FullName,
                Category = post.Category
            };

            return postDto;
        }
    }
}