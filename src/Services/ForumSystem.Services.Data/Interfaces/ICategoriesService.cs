﻿namespace ForumSystem.Services.Data.Interfaces
{
	using System.Collections.Generic;

	public interface ICategoriesService
	{
		IEnumerable<T> GetAll<T>(int? count = null);

		T GetByName<T>(string name);
	}
}
