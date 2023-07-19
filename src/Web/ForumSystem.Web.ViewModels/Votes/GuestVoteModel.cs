namespace ForumSystem.Web.ViewModels.Votes
{
    using ForumSystem.Services.Mapping;
    using ForumSystem.Web.Controllers;

    public class GuestVoteModel : IMapFrom<VoteInputModel>
	{
		public int PostId { get; set; }

		public bool IsUpVote { get; set; }

		public string GuestId { get; set; }
	}
}
