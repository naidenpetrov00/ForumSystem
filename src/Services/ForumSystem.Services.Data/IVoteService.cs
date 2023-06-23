namespace ForumSystem.Services.Data
{
	using System.Threading.Tasks;

	public interface IVoteService
	{
		/// <summary>
		///
		/// </summary>
		/// <param name="postId"></param>
		/// <param name="userId"></param>
		/// <param name="isUpvote">If true - upVote, else - downVote.</param>
		/// <returns></returns>
		Task VoteAsync(int postId, string userId, bool isUpvote);

		int GetVotes(int postId);
	}
}
