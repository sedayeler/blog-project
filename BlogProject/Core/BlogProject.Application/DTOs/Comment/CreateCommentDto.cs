﻿namespace BlogProject.Application.DTOs.Comment
{
    public class CreateCommentDto
    {
        public string Content { get; set; }
        public Guid PostId { get; set; }
    }
}
