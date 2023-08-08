using Microsoft.AspNetCore.SignalR;

namespace ForumSystem.Web.Hubs
{
	public class WeatherHub : Hub
	{
		//private readonly IWeatherService weatherService;

		public WeatherHub()
		{
			//this.weatherService = weatherService;
		}

		public async Task GetWeatherUpdate()
		{
			//RecurringJob.AddOrUpdate("weatherUpdate", () => this.weatherService.Update(), Cron.Minutely);
		}
	}
}
