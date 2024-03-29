﻿using ForumSystem.Web;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ForumSystem.Services.Tests
{
	public class HomePageTests
	{
		[Fact]
		public async Task HomePageShouldHaveTitle()
		{


			var serverFactory = new WebApplicationFactory<Program>();
			var client = serverFactory.CreateClient();

			var response = await client.GetAsync("/");
			var responseAsString = await response.Content.ReadAsStringAsync();

			Assert.Contains("<h1", responseAsString);
		}
	}
}
