﻿namespace ForumSystem.Services.Data
{
	using System.Threading.Tasks;

	using ForumSystem.Data.Common.Repositories;
	using ForumSystem.Data.Models;
	using ForumSystem.Services.Data.Interfaces;

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
	}
}