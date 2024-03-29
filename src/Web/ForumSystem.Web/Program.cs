﻿namespace ForumSystem.Web
{
    using System;
    using System.Reflection;

    using ForumSystem.Common.Models;
    using ForumSystem.Data;
    using ForumSystem.Data.Common;
    using ForumSystem.Data.Common.Repositories;
    using ForumSystem.Data.Models;
    using ForumSystem.Data.Repositories;
    using ForumSystem.Data.Seeding;
    using ForumSystem.Hubs.Filters;
    using ForumSystem.Services.Data.Interfaces;
    using ForumSystem.Services.Mapping;
    using ForumSystem.Services.Weather.Interfaces;
    using ForumSystem.Web.Hubs;
    using ForumSystem.Web.Infrastructure;
    using ForumSystem.Web.ViewModels;
    using Hangfire;
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
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

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
            services.AddSignalR(options =>
                {
                    options.EnableDetailedErrors = true;
                });

            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(configuration);

            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = configuration.GetConnectionString("DefaultConnection");
                options.SchemaName = "dbo";
                options.TableName = "CacheRecords";
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            // Hangfire
            services.AddHangfire(options => options
             .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
             .UseSimpleAssemblyNameTypeSerializer()
             .UseRecommendedSerializerSettings()
             .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();

            // Data repositories
            services.AddScoped(
                typeof(IDeletableEntityRepository<>),
                typeof(EfDeletableEntityRepository<>));
            services.AddScoped(
                typeof(IRepository<>),
                typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddConventionalServices(
                typeof(ICategoriesService).Assembly,
                typeof(IWeatherService).Assembly);
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
            app.UseResponseCompression();
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangFireAuthorizationFilter() },
            });
            SeedJobs(app.Services);
            app.MapHub<WeatherHub>("/weatherHub");
            app.MapControllerRoute(
                "areaRoute",
                "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                "default",
                "{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                "forumCategory",
                "f/{name:minlength(3)}",
                new { controller = "Categories", action = "ByName" });

            app.MapRazorPages();
        }

        private static void SeedJobs(IServiceProvider services)
        {
            var weatherService = services.GetRequiredService<IWeatherService>();
            RecurringJob.AddOrUpdate("weatherUpdate", () => weatherService.Update(), Cron.Minutely);
        }
    }
}
