using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
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
            string connectionString = AppDbConnectionString();
            Console.WriteLine($"Using database connection string: {connectionString}");

            string identityConnectionString = AppIdentityDbConnectionString();
            Console.WriteLine($"Using database connection string: {identityConnectionString}");

            services.AddEntityFrameworkNpgsql()
               .AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString))
               .AddDbContext<AppIdentityDbContext>(options => options.UseNpgsql(identityConnectionString))
               .BuildServiceProvider();

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddSpaStaticFiles(options => {
                options.RootPath = "./wwwroot/dist";
            });

            services.AddTransient<IBlogPostRepository, EFBlogPostRepository>();

            services.AddMvc();

            services.ConfigureApplicationCookie(options => options.Events
                = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents{
                    OnRedirectToLogin = context => {
                        if (context.Request.Path.StartsWithSegments("/api")
                            && context.Response.StatusCode == 200) {
                                context.Response.StatusCode = 401;
                            }
                        else {
                            context.Response.Redirect(context.RedirectUri);
                        }
                        return Task.FromResult<object>(null);
                    }
                }
            );

            services.AddCors(corsOptions => {
                corsOptions.AddPolicy("DevelopmentOrigins", policyBuilder => {
                    policyBuilder
                        .WithOrigins("http://localhost:8080")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                app.UseCors("DevelopmentOrigins");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            }

            app.UseAuthentication();
            app.UseMvc();
            app.UseSpaStaticFiles();
            app.UseSpa(spa => {
                spa.Options.SourcePath = "./wwwroot/dist";
                spa.Options.DefaultPage = "/index.html";
            });

            AppIdentityDbContext.CreateAdminUser(app.ApplicationServices, Configuration).Wait();
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

        private string PostgreSqlConnectionString(string userId, string password, string host, string port, string databaseName)
        {
            return $"User ID={userId}; Password={password}; Host={host}; Port={port}; Database={databaseName};";
        }

        private string AppDbConnectionString()
        {
            string userId = Configuration["Database:AppDatabase:UserId"];
            string password = Configuration["Database:AppDatabase:Password"];
            string host = Configuration["Database:AppDatabase:Host"];
            string port = Configuration["Database:AppDatabase:Port"];
            string databaseName = Configuration["Database:AppDatabase:Name"];
            
            string databaseType = Configuration["Database:Type"];

            if(databaseType == "PostgreSQL")
            {
                return PostgreSqlConnectionString(userId, password, host, port, databaseName);
            }         

            throw new Exception($"Unknown {nameof(databaseType)}: {databaseType}");
        }

        private string AppIdentityDbConnectionString()
        {
            string userId = Configuration["Database:AppIdentityDatabase:UserId"];
            string password = Configuration["Database:AppIdentityDatabase:Password"];
            string host = Configuration["Database:AppIdentityDatabase:Host"];
            string port = Configuration["Database:AppIdentityDatabase:Port"];
            string databaseName = Configuration["Database:AppIdentityDatabase:Name"];
            
            string databaseType = Configuration["Database:Type"];

            if(databaseType == "PostgreSQL")
            {
                return PostgreSqlConnectionString(userId, password, host, port, databaseName);
            }         

            throw new Exception($"Unknown {nameof(databaseType)}: {databaseType}");
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
