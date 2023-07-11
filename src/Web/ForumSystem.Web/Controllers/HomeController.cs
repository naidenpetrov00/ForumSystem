namespace ForumSystem.Web.Controllers
{
	using System.Diagnostics;

	using ForumSystem.Services.Data.Interfaces;
	using ForumSystem.Web.ViewModels;
	using ForumSystem.Web.ViewModels.Home;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;

	public class HomeController : BaseController
	{
		private readonly ICategoriesService categoriesService;
		private readonly ILogger<HomeController> logger;

		public HomeController(
			ICategoriesService categoriesService,
			ILogger<HomeController> logger)
		{
			this.categoriesService = categoriesService;
			this.logger = logger;
		}

		public IActionResult Index()
		{
			this.logger.LogWarning(5, "Log Test");

			var viewModel = new IndexViewModel
			{
				Categories = this.categoriesService.GetAll<IndexCategoryViewModel>(),
			};

			return this.View(viewModel);
		}

		public IActionResult Privacy()
		{
			return this.View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return this.View(
				new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
		}
	}
}
