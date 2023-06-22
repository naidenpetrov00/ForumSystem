﻿namespace ForumSystem.Services.Data.Interfaces
{
	using System.Collections.Generic;

	public interface ISettingsService
	{
		int GetCount();

		IEnumerable<T> GetAll<T>();
	}
}
