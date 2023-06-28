namespace ForumSystem.Web.Controllers
{
	using System;

	public class GuestVoteCookieModel
	{
		public GuestVoteCookieModel()
		{
			this.Key = "GuestCookie";
			this.Value = Guid.NewGuid().ToString();
		}

		public string Key { get; set; }

		public string Value { get; set; }
	}

}