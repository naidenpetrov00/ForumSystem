namespace ForumSystem.Common.Models
{
    using System;

    public class GuestCookieModel
	{
		public static string Key => "GuestIdentity";

		public static string Value => Guid.NewGuid().ToString();

		public static DateTimeOffset Expires => DateTimeOffset.UtcNow.AddYears(1);
	}
}
