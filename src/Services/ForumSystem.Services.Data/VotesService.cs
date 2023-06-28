namespace ForumSystem.Services.Data
{
	using System.Linq;
	using System.Threading.Tasks;

	using ForumSystem.Common;
	using ForumSystem.Data.Common.Repositories;
	using ForumSystem.Data.Models;

	public class VotesService : IVoteService
	{
		private readonly IRepository<Vote> voteRepository;

		public VotesService(IRepository<Vote> voteRepository)
		{
			this.voteRepository = voteRepository;
		}

		public int GetVotes(int postId)
		{
			var votes = this.voteRepository
				.All()
				.Where(x => x.PostId == postId)
				.Sum(x => (int)x.Type);

			return votes;
		}

		public async Task VoteAsync(int postId, bool isUpvote, string userId)
		{
			var vote = this.voteRepository
				.All()
				.FirstOrDefault(x => x.PostId == postId && x.UserId == userId);

			if (vote != null)
			{
				vote.Type = isUpvote ? VoteType.UpVote : VoteType.DownVote;
			}
			else
			{
				vote = new Vote
				{
					PostId = postId,
					UserId = userId,
					Type = isUpvote ? VoteType.UpVote : VoteType.DownVote,
				};

				await this.voteRepository.AddAsync(vote);
			}

			await this.voteRepository.SaveChangesAsync();
		}
	}
}
