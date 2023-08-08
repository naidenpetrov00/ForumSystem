namespace ForumSystem.Web.Hubs
{
	using System.Threading.Tasks;
	using Microsoft.Extensions.Configuration;
	using ForumSystem.Services.Weather.Interfaces;
	using Hangfire;
	using Microsoft.AspNetCore.SignalR;

	public class WeatherHub : Hub
	{
		private readonly IWeatherService weatherService;
		private readonly IConfiguration configuration;

		public WeatherHub(IWeatherService weatherService, IConfiguration configuration)
		{
			this.weatherService = weatherService;
			this.configuration = configuration;
		}

		public void GetWeatherUpdate()
		{
			RecurringJob.AddOrUpdate("weatherUpdate", () => this.Update(), Cron.Minutely);
		}

		private async Task Update()
		{
			var apiKey = this.configuration["OpenWeather:ApiKey"];
			await this.Clients.All.SendAsync("WeatherUpdate", apiKey);
		}
	}
}
