namespace ForumSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using ForumSystem.Common.Enums;
    using ForumSystem.Common.Models;
    using ForumSystem.Data.Models;
    using ForumSystem.Services.Data;
    using ForumSystem.Web.ViewModels.Votes;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<VoteResponseModel>> Post(VoteInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            if (userId != null)
            {
                await this.voteService.VoteAsync(input.PostId, input.IsUpVote, userId, SignedIn.True);
            }
            else
            {
                var guestCookie = this.Request.Cookies[GuestCookieModel.Key];
                await this.voteService.VoteAsync(input.PostId, input.IsUpVote, guestCookie, SignedIn.False);
            }

            var votes = this.voteService.GetVotes(input.PostId);

            var voteOutput = new VoteResponseModel
            {
                VotesCount = votes,
            };

            return voteOutput;
        }
    }
}
