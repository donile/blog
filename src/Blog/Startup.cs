using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MarkDonile.Blog.DataAccess;
using MarkDonile.Blog.Models;

namespace MarkDonile.Blog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = PostgreSqlConnectionString();
            Console.WriteLine($"Using database connection string: {connectionString}");

            services.AddEntityFrameworkNpgsql()
               .AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            services.AddSpaStaticFiles(options => {
                options.RootPath = "./wwwroot/dist";
            });

            services.AddTransient<IBlogPostRepository, EFBlogPostRepository>();

            services.AddMvc();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Admin/UserAuthorization/SignIn");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseEndpoints(cfg => cfg.MapControllers());
            app.UseSpaStaticFiles();
            app.UseSpa(spa => {
                spa.Options.SourcePath = "./wwwroot/dist";
                spa.Options.DefaultPage = "/index.html";
            });

            DatabaseContext.CreateAdminUser(app.ApplicationServices, Configuration).Wait();
        }

        private string PostgreSqlConnectionString()
        {
            string userId = Configuration["Database:PostgreSQL:UserId"];
            string password = Configuration["Database:PostgreSQL:Password"];
            string host = Configuration["Database:PostgreSQL:Host"];
            string port = Configuration["Database:PostgreSQL:Port"];
            string databaseName = Configuration["Database:PostgreSQL:Name"];

            string connectionString = $"User ID={userId}; Password={password}; Host={host}; Port={port}; Database={databaseName};";

            return connectionString;
        }
    }
}
