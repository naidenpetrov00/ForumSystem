﻿namespace ForumSystem.Web.Controllers
{
	using ForumSystem.Services.Data.Interfaces;
	using ForumSystem.Web.ViewModels.Categories;
	using Microsoft.AspNetCore.Mvc;

	public class CategoriesController : Controller
	{
		private readonly ICategoriesService categoriesService;

		public CategoriesController(ICategoriesService categoriesService)
		{
			this.categoriesService = categoriesService;
		}

		public IActionResult ByName(string name)
		{
			var viewModel = this.categoriesService.GetByName<CategoryViewModel>(name);

			return this.View(viewModel);
		}
	}
}
