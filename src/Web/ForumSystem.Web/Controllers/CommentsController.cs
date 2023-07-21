namespace ForumSystem.Web.Controllers
{
	using System.Threading.Tasks;

	using ForumSystem.Data.Models;
	using ForumSystem.Services.Data.Interfaces;
	using ForumSystem.Web.ViewModels.Comments;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
	using Newtonsoft.Json;

	public class CommentsController : Controller
	{
		private readonly ICommentService commentService;
		private readonly UserManager<ApplicationUser> userManager;

		public CommentsController(
			ICommentService commentService,
			UserManager<ApplicationUser> userManager)
		{
			this.commentService = commentService;
			this.userManager = userManager;
		}

		[HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentInputModel input)
		{
			var parentCommentId = input.ParentCommentId == 0 ? (int?)null : input.ParentCommentId;

			if (parentCommentId.HasValue)
			{
				if (!this.commentService.IsInPostId(parentCommentId.Value, input.PostId))
				{
					return this.BadRequest();
				}
			}

			var userId = this.userManager.GetUserId(this.User);
			await this.commentService.Create(input.PostId, userId, input.Content, parentCommentId);

			return this.Ok();
		}
	}
}
