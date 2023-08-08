namespace ForumSystem.Web.ViewComponents
{
    using ForumSystem.Web.ViewModels.Weather;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    public class WeatherViewComponent : ViewComponent
    {
        private readonly IConfiguration configuration;

        public WeatherViewComponent(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new WeatherModel
            {
                ApiKey = this.configuration["OpenWeather:ApiKey"],
            };
            return this.View(viewModel);
        }
    }
}
