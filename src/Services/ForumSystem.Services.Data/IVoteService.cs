namespace ForumSystem.Services.Data
{
	using ForumSystem.Common;
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
		Task VoteAsync(int postId, bool isUpvote, string userId);

		Task NotSignedInUserVote(int postId, string guestCookie, bool isUpVote);

		int GetVotes(int postId);
	}
}
