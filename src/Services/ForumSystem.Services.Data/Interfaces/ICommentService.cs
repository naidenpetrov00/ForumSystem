namespace ForumSystem.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    public interface ICommentService
    {
        Task Create(int postId, string userId, string content, int? parentCommentId = null);

        bool IsInPostId(int commentId, int postId);
    }
}
