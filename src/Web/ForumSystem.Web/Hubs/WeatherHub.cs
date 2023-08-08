namespace ForumSystem.Web.Hubs
{
	using System.Threading.Tasks;

	using Hangfire;
	using Microsoft.AspNetCore.SignalR;

	public class WeatherHub : Hub
	{
		public void UpdateWeather()
		{
			RecurringJob.AddOrUpdate("weatherUpdate", () => null, Cron.Minutely);
		}
	}
}
