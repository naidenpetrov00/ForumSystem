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
		Task UserVoteAsync(int postId, bool isUpvote, string userId);

		Task GuestVoteAsync(int postId, bool isUpvote, string guestId);

		int GetVotes(int postId);
	}
}
