namespace ForumSystem.Services.Data
{
    using System.Threading.Tasks;

    public interface IVoteService
	{
		Task VoteAsync(int postId, bool isUpvote, string identity, bool signedUser);

		int GetVotes(int postId);
	}
}
