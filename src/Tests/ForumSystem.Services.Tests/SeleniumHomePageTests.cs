namespace ForumSystem.Services.Tests
{
	using ForumSystem.Web;
	using OpenQA.Selenium.Chrome;
	using OpenQA.Selenium.Remote;

	public class SeleniumHomePageTests
	{
		private RemoteWebDriver browser;
		private SeleniumServerFactory<Program> serverFactory;

		public SeleniumHomePageTests()
		{
			this.serverFactory = new SeleniumServerFactory<Program>();
			serverFactory.CreateClient();
			var options = new ChromeOptions();
			options.AddArguments("--headless", "--ignore-certificate-errors");

			this.browser = new RemoteWebDriver(options);
		}

		[Fact]
		public void HomePageShouldHaveH1Tag()
		{
			this.browser.Navigate().GoToUrl(this.serverFactory.RootUri + "/Home/Index");
			Assert.Contains("Welcome to", this.browser.FindElementByCssSelector("h1").Text);

		}
	}
}
