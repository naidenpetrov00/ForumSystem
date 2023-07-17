﻿namespace ForumSystem.Web.Controllers
{
	using System.Threading.Tasks;

	using ForumSystem.Data.Models;
	using ForumSystem.Services.Data.Interfaces;
	using ForumSystem.Web.ViewModels.Categories;
	using ForumSystem.Web.ViewModels.Posts;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;

	public class PostsController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly ICategoriesService categoriesService;
		private readonly IPostService postService;

		public PostsController(
			UserManager<ApplicationUser> userManager,
			ICategoriesService categoriesService,
			IPostService postService)
		{
			this.userManager = userManager;
			this.categoriesService = categoriesService;
			this.postService = postService;
		}

		public IActionResult ById(int id)
		{
			var postViewModel = this.postService.GetById<PostViewModel>(id);
			if (postViewModel == null)
			{
				return this.NotFound();
			}

			return this.View(postViewModel);
		}

		[Authorize]
		public IActionResult Create()
		{
			var categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();
			var viewModel = new PostCreateInputModel
			{
				Categories = categories,
			};

			return this.View(viewModel);
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
			this.TempData["InfoMessage"] = "Form post created!";

			return this.RedirectToAction(nameof(this.ById), new { id = postId });
		}
	}
}
