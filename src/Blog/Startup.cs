using AutoMapper;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MarkDonile.Blog.DataAccess;
using MarkDonile.Blog.Extensions;
using MarkDonile.Blog.Options;
using MarkDonile.Blog.Dto;
using MarkDonile.Blog.Models;

namespace MarkDonile.Blog
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = _configuration["CONNECTION_STRING"];
            if (_environment.IsDevelopment())
            {
                Console.WriteLine($"Using database connection string: {connectionString}");
            }

            services.AddEntityFrameworkNpgsql()
               .AddDbContext<DatabaseContext>(options => 
                    options
                        .UseNpgsql(connectionString)
                        .UseSnakeCaseNamingConvention()
                );
            
            services.AddBlogAuthentication(new BlogAuthenticationOptions {
                Environment = _environment,
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"],
                Domain = _configuration["Jwt:Okta:OktaDomain"],
            });

            services.AddAutoMapper(options => {
                options.CreateMap<CreateBlogPostDto, BlogPost>();
            });

            services.AddSwaggerGen();

            services.AddTransient<IBlogPostRepository, EFBlogPostRepository>();
            services.AddTransient<IAuthorRepository, EFAuthorRepository>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(cfg => cfg.MapControllers());
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API");
            });
        }
    }
}
