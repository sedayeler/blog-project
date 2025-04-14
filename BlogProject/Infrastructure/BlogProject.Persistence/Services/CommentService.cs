using BlogProject.Application.Abstractions.Services;
using BlogProject.Application.DTOs.Comment;
using BlogProject.Application.Repositories;
using BlogProject.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BlogProject.Persistence.Services
{
    public class CommentService : ICommentService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommentReadRepository _commentReadRepository;
        private readonly ICommentWriteRepository _commentWriteRepository;
        private readonly UserManager<User> _userManager;

        public CommentService(IHttpContextAccessor httpContextAccessor, ICommentReadRepository commentReadRepository, ICommentWriteRepository commentWriteRepository, UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _commentReadRepository = commentReadRepository;
            _commentWriteRepository = commentWriteRepository;
            _userManager = userManager;
        }

        public async Task CreateCommentAsync(CreateCommentDto dto)
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (string.IsNullOrWhiteSpace(username))
                throw new Exception("Kullanıcı doğrulama hatası.");

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            Comment comment = new Comment()
            {
                Content = dto.Content,
                UserId = user.Id,
                PostId = dto.PostId
            };

            await _commentWriteRepository.AddAsync(comment);
            await _commentWriteRepository.SaveAsync();
        }

        public async Task DeleteCommentAsync(Guid id)
        {
            var comment = await _commentReadRepository.GetByIdAsync(id);
            if (comment == null)
                throw new Exception("Yorum bulunamadı.");

            _commentWriteRepository.Remove(comment);
            await _commentWriteRepository.SaveAsync();
        }
    }
}
