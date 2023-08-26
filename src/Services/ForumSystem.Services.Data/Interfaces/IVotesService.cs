namespace ForumSystem.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    using ForumSystem.Common;

    public interface IVotesService : IService
    {
        Task VoteAsync(int postId, bool isUpvote, string identity, bool signedUser);

        int GetVotes(int postId);
    }
}
