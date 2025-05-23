﻿using BlogProject.Domain.Entities.Common;
using BlogProject.Domain.Entities.Identity;

namespace BlogProject.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
