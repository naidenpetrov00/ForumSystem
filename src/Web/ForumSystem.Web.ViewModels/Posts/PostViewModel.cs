namespace ForumSystem.Web.ViewModels.Posts
{
	using ForumSystem.Data.Models;
	using ForumSystem.Services.Mapping;
	using Ganss.Xss;
	using System;

	public class PostViewModel : IMapFrom<Post>
	{
		public DateTime CreatedOn { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

		public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

		public string UserUserName { get; set; }
	}
}
