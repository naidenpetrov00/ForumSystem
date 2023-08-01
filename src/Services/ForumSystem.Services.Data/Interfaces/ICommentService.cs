namespace ForumSystem.Services.Data.Interfaces
{
    using ForumSystem.Data.Models;
    using System.Threading.Tasks;

    public interface ICommentService
    {
        Task<Comment> Create(int postId, string userId, string content, int? parentCommentId = null);

        bool IsInPostId(int commentId, int postId);
    }
}
