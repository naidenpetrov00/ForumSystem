using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using System.Diagnostics;
using Xunit.Abstractions;

namespace ForumSystem.Services.Tests
{
	public class SeleniumHomePageTests
	{
		private readonly string RootUri = "https://localhost:5001";
		private IWebDriver driver;

		public SeleniumHomePageTests()
		{
			var options = new ChromeOptions();
			options.AddArguments(
				"--headless",
				"--no-sandbox",
				"--ignore-certificate-errors",
				"--disable-blink-features=AutomationControlled");
			this.driver = new ChromeDriver(options);
		}

		[Fact]
		public void HomePageShouldHaveH1Tag()
		{
			this.driver.Navigate().GoToUrl(this.RootUri + "/");
			Assert.Contains("Welcome to", this.driver.PageSource);
		}
	}
}
