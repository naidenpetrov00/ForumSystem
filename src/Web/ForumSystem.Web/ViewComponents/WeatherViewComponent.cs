namespace ForumSystem.Web.ViewComponents
{
	using ForumSystem.Web.Hubs;
	using ForumSystem.Web.ViewModels.Weather;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.SignalR;
	using Microsoft.Extensions.Configuration;
	using System.Threading.Tasks;

	public class WeatherViewComponent : ViewComponent
	{
		private readonly IConfiguration configuration;
		private readonly IHubContext<WeatherHub> hubContext;

		public WeatherViewComponent(IConfiguration configuration, IHubContext<WeatherHub> hubContext)
		{
			this.configuration = configuration;
			this.hubContext = hubContext;
		}

		public IViewComponentResult Invoke()
		{
			var apiKey = this.configuration["OpenWeather:ApiKey"];
			this.hubContext.Clients.All.SendAsync("GetWeather", apiKey).GetAwaiter().GetResult();
			var viewModel = new WeatherModel
			{
				ApiKey = this.configuration["OpenWeather:ApiKey"],
			};
			return this.View(viewModel);
		}
	}
}
