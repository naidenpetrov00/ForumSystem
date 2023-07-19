namespace ForumSystem.Web.Controllers
{
	using System;

	using ForumSystem.Services.Data.Interfaces;
	using ForumSystem.Web.ViewModels.Categories;
	using Microsoft.AspNetCore.Mvc;

	public class CategoriesController : Controller
	{
		private const int ItemsPerPage = 5;

		private readonly ICategoriesService categoriesService;
		private readonly IPostService postService;

		public CategoriesController(
			ICategoriesService categoriesService,
			IPostService postService)
		{
			this.categoriesService = categoriesService;
			this.postService = postService;
		}

		public IActionResult ByName(string name, int page = 1)
		{
			var viewModel = this.categoriesService.GetByName<CategoryViewModel>(name);
			viewModel.ForumPosts = this.postService.GetByCategoryId<PostInCategoryViewModel>(viewModel.Id, ItemsPerPage, (page - 1) * ItemsPerPage);

			var count = this.postService.GetCountByCategoryId(viewModel.Id);
			viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
			viewModel.CurrentPage = page;

			return this.View(viewModel);
		}
	}
}
