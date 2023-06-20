namespace ForumSystem.Web.Controllers
{
	using System.Threading.Tasks;

	using ForumSystem.Data.Common.Repositories;
	using ForumSystem.Data.Models;
	using ForumSystem.Services.Data.Interfaces;
	using ForumSystem.Web.ViewModels.Posts;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;

	public class PostController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly IPostService postService;

		public PostController(
			UserManager<ApplicationUser> userManager,
			IPostService postService)
		{
			this.userManager = userManager;
			this.postService = postService;
		}

		[Authorize]
		public IActionResult Create()
		{
			return this.View();
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Create(PostCreateInputModel input)
		{
			if (!this.ModelState.IsValid)
			{
				return this.View(input);
			}

			var user = await this.userManager.GetUserAsync(this.User);
			var postId = await this.postService.CreateAsync(input.Title, input.Content, input.CategoryId, user.Id);

			return this.RedirectToAction("ById", new { id = postId });
		}

		[Authorize]
		public IActionResult ById(int id)
		{
			return this.View();
		}
	}
}
