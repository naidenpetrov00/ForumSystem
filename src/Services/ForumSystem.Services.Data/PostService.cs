namespace ForumSystem.Services.Data
{
    using System.Collections.Generic;
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

		public IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0)
		{
			var query = this.postRepository
				.All()
				.OrderByDescending(x => x.CreatedOn)
				.Where(x => x.CategoryId == categoryId)
				.Skip(skip);
			if (take != null)
			{
				query = query.Take(take.Value);
			}

			return query.To<T>().ToList();
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

		public int GetCountByCategoryId(int categoryId)
		{
			return this.postRepository
				.All()
				.Count(x => x.CategoryId == categoryId);
		}
	}
}
