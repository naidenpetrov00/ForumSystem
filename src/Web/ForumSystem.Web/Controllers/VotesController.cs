namespace ForumSystem.Web.Controllers
{
	using System.Threading.Tasks;
	using ForumSystem.Data.Models;
	using ForumSystem.Services.Data;
	using ForumSystem.Web.ViewModels.Votes;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.IdentityModel.Tokens;

	[ApiController]
	[Route("api/[controller]")]
	public class VotesController : ControllerBase
	{
		private readonly IVoteService voteService;
		private readonly UserManager<ApplicationUser> userManager;

		public VotesController(
			IVoteService voteService,
			UserManager<ApplicationUser> userManager)
		{
			this.voteService = voteService;
			this.userManager = userManager;
		}


		[HttpPost]
		//[Authorize]
		public async Task<ActionResult<VoteResponseModel>> Post(VoteInputModel input)
		{
			var userId = this.userManager.GetUserId(this.User);
			if (userId == null)
			{
				var cookie = new GuestVoteCookieModel();
				this.Response.Cookies.Append(cookie.Key, cookie.Value);
				this.Response.Cookies.[cookie.Key].Value = userId;
				await this.voteService.VoteAsync(input.PostId, input.IsUpVote, guestCookie);
			}
			await this.voteService.VoteAsync(input.PostId, input.IsUpVote, userId);
			var votes = this.voteService.GetVotes(input.PostId);
			var voteOutput = new VoteResponseModel
			{
				VotesCount = votes,
			};
			return voteOutput;
		}
	}
}
