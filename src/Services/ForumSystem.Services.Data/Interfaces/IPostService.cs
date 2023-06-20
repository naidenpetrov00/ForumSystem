namespace ForumSystem.Services.Data.Interfaces
{
	using System.Threading.Tasks;

	public interface IPostService
	{
		Task<int> CreateAsync(string title, string content, int categoryId, string userId);
	}
}
