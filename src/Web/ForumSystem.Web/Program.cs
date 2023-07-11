namespace ForumSystem.Web
{
	using System.Reflection;

	using ForumSystem.Common.Models;
	using ForumSystem.Data;
	using ForumSystem.Data.Common;
	using ForumSystem.Data.Common.Repositories;
	using ForumSystem.Data.Models;
	using ForumSystem.Data.Repositories;
	using ForumSystem.Data.Seeding;
	using ForumSystem.Services.Data;
	using ForumSystem.Services.Data.Interfaces;
	using ForumSystem.Services.Mapping;
	using ForumSystem.Services.Messaging;
	using ForumSystem.Web.ViewModels;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;

	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			ConfigureServices(builder.Services, builder.Configuration);
			var app = builder.Build();
			Configure(app);
			app.Run();
		}

		private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(
				options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

			services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
				.AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

			services.Configure<CookiePolicyOptions>(
				options =>
				{
					options.CheckConsentNeeded = context => true;
					options.MinimumSameSitePolicy = SameSiteMode.None;
				});

			services.AddControllersWithViews(
				options =>
				{
					options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
				})
				.AddRazorRuntimeCompilation();
			services.AddAutoMapper(typeof(AutoMapperConfig));
			services.AddAntiforgery(options =>
			{
				options.HeaderName = "X-CSRF-TOKEN";
			});
			services.AddSwaggerGen();

			services.AddRazorPages();
			services.AddDatabaseDeveloperPageExceptionFilter();

			services.AddSingleton(configuration);

			// Data repositories
			services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
			services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
			services.AddScoped<IDbQueryRunner, DbQueryRunner>();

			// Application services
			services.AddTransient<IEmailSender, NullMessageSender>();
			services.AddTransient<ISettingsService, SettingsService>();
			services.AddTransient<ICategoriesService, CategoriesService>();
			services.AddTransient<IPostService, PostService>();
			services.AddTransient<IVoteService, VotesService>();
		}

		private static void Configure(WebApplication app)
		{
			// Seed data on application startup
			using (var serviceScope = app.Services.CreateScope())
			{
				var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
				dbContext.Database.Migrate();
				new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
			}

			AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

			app.UseSwagger();
			app.UseSwaggerUI();

			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.Use((context, next) =>
			{
				if (!context.Request.Cookies.ContainsKey(GuestCookieModel.Key))
				{
					var cookieOptions = new CookieOptions()
					{
						Expires = GuestCookieModel.Expires,
						IsEssential = true,
						SameSite = SameSiteMode.Strict,
					};

					context.Response.Cookies.Append(GuestCookieModel.Key, GuestCookieModel.Value, cookieOptions);
				}

				return next();
			});
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
			app.MapControllerRoute("forumCategory", "f/{name:minlength(3)}", new { controller = "Categories", action = "ByName" });
			app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
			app.MapRazorPages();
		}
	}
}
