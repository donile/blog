using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.Extensions.Configuration;
using MarkDonile.Blog.DataAccess;
using MarkDonile.Blog.Models;
using System.Runtime.InteropServices;

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

            services.AddApplicationInsightsTelemetry();
            services.AddEntityFrameworkNpgsql()
               .AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString))
               .BuildServiceProvider();
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
            app.UseSpaStaticFiles();
            app.UseSpa(spa => {
                spa.Options.SourcePath = "./wwwroot/dist";
                spa.Options.DefaultPage = "/index.html";
            });

            DatabaseContext.CreateAdminUser(app.ApplicationServices, Configuration).Wait();
        }

        private string MsSqlServerConnectionString()
        {
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows)){
                return WindowsConnectionString();
            }
            else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux)){
                return LinuxConnectionString();
            }
            else if(RuntimeInformation.IsOSPlatform(OSPlatform.OSX)){
                return MacConnectionString();
            }
            else{
                throw new NotImplementedException();
            }
        }

        private string LinuxConnectionString()
        {
            var connectionBuilder = new SqlConnectionStringBuilder();
            connectionBuilder.ConnectionString = Configuration["Database:Linux:ConnectionString"];
            connectionBuilder.UserID = Configuration["Database:Linux:UserId"];
            connectionBuilder.Password = Configuration["Database:Linux:Password"];

            return connectionBuilder.ConnectionString;
        }
        
        private string WindowsConnectionString()
        {
            return Configuration["Database:Windows:ConnectionString"];
        }

        private string MacConnectionString()
        {
            throw new NotImplementedException();
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

        private string ConnectionString()
        {
            string environment = Configuration["ASPNETCORE_ENVIRONMENT"];
            Console.WriteLine($"Using ASPNETCORE_ENVIRONMENT: {environment}");

            if (environment == "Development")
            {
                return Configuration["Database:MSSQLServer:Windows:ConnectionString"];
            }
            
            if (environment == "Production")
            {
                return Configuration["Database:MSSQLServer:Azure:ConnectionString"];
            }

            throw new Exception($"Invalid environment: {environment}");
        }
    }
}
