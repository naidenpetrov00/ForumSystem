namespace ForumSystem.Data.Models
{
    using ForumSystem.Common;
    using ForumSystem.Data.Common.Models;

    public class Vote : BaseModel<int>
	{
		public int PostId { get; set; }

		public virtual Post Post { get; set; }

		public string GuestId { get; set; }

		public string UserId { get; set; }

		public virtual ApplicationUser User { get; set; }

		public VoteType Type { get; set; }
	}
}
