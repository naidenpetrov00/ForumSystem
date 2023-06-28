namespace ForumSystem.Web.Controllers
{
    using System;

	public class GuestCookieModel
	{
		public GuestCookieModel()
		{
			this.Value = Guid.NewGuid().ToString();
		}

		public string Key { get; set; }

		public string Value { get; set; }
	}

}