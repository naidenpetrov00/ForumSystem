namespace ForumSystem.Services.Data
{
	using System.Linq;
	using System.Security.Principal;
	using System.Threading.Tasks;

	using AutoMapper;
	using ForumSystem.Common;
	using ForumSystem.Data.Common.Repositories;
	using ForumSystem.Data.Models;

	public class VotesService : IVoteService
	{
		private readonly IRepository<Vote> voteRepository;
		private readonly IMapper mapper;

		public VotesService(IRepository<Vote> voteRepository,
			IMapper mapper)
		{
			this.voteRepository = voteRepository;
			this.mapper = mapper;
		}

		public int GetVotes(int postId)
		{
			var votes = this.voteRepository
				.All()
				.Where(x => x.PostId == postId)
				.Sum(x => (int)x.Type);

			return votes;
		}

		public async Task GuestVoteAsync(int postId, bool isUpvote, string identity, bool signedUser)
		{
			var vote = this.voteRepository
				.All()
				.FirstOrDefault(x => x.PostId == postId && x.GuestId == identity);


			if (vote != null)
			{
				vote.Type = isUpvote ? VoteType.UpVote : VoteType.DownVote;
			}
			else
			{
				vote = new Vote();
				vote.PostId = postId;
				if (isUpvote)
				{
					vote.UserId = identity;
				}
				else
				{
					vote.GuestId = identity;
				}
				vote.Type = isUpvote ? VoteType.UpVote : VoteType.DownVote;

				await this.voteRepository.AddAsync(vote);
			}

			await this.voteRepository.SaveChangesAsync();
		}

		public async Task UserVoteAsync(int postId, bool isUpvote, string userId)
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
