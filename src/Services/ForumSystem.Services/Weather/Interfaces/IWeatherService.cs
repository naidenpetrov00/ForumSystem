namespace ForumSystem.Services.Weather.Interfaces
{
    using System.Threading.Tasks;

    using ForumSystem.Common;

    public interface IWeatherService : IService
    {
        Task Update();
    }
}
