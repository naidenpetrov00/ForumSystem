﻿namespace ForumSystem.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    using ForumSystem.Data.Models;

    public interface ICommentService
    {
        Task<Comment> Create(int postId, string userId, string content, int? parentCommentId = null);

        bool IsInPostId(int commentId, int postId);
    }
}
