namespace ForumSystem.Services.Weather
{
    using System.Threading.Tasks;

    using ForumSystem.Services.Weather.Interfaces;
    using ForumSystem.Web.Hubs;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Configuration;

    public class WeatherService : IWeatherService
    {
        private readonly IHubContext<WeatherHub> hubContext;
        private readonly IConfiguration configuration;

        public WeatherService(
            IHubContext<WeatherHub> hubContext,
            IConfiguration configuration)
        {
            this.hubContext = hubContext;
            this.configuration = configuration;
        }

        public async Task Update()
        {
            var apiKey = this.configuration["OpenWeather:ApiKey"];
            await this.hubContext.Clients.All.SendAsync("UpdateWeather", apiKey);
        }
    }
}
