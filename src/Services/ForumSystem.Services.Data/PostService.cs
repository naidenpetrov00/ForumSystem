namespace ForumSystem.Services.Data
{
	using System.Linq;
	using System.Threading.Tasks;

	using ForumSystem.Data.Common.Repositories;
	using ForumSystem.Data.Models;
	using ForumSystem.Services.Data.Interfaces;
	using ForumSystem.Services.Mapping;

	public class PostService : IPostService
	{
		private readonly IDeletableEntityRepository<Post> postRepository;

		public PostService(IDeletableEntityRepository<Post> postRepository)
		{
			this.postRepository = postRepository;
		}

		public async Task<int> CreateAsync(string title, string content, int categoryId, string userId)
		{
			var post = new Post
			{
				CategoryId = categoryId,
				Content = content,
				Title = title,
				UserId = userId,
			};

			await this.postRepository.AddAsync(post);
			await this.postRepository.SaveChangesAsync();

			return post.Id;
		}

		public T GetById<T>(int id)
		{
			var post = this.postRepository
				.All()
				.Where(x => x.Id == id)
				.To<T>()
				.FirstOrDefault();

			return post;
		}
	}
}
