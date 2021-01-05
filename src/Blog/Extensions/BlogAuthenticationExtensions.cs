using MarkDonile.Blog.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Okta.AspNetCore;

namespace MarkDonile.Blog.Extensions
{
    public static class BlogAuthenticationExtensions
    {
        public static IServiceCollection AddBlogAuthentication(this IServiceCollection services, BlogAuthenticationOptions options)
        {
            services.AddAuthentication(authConfig =>
                {
                    authConfig.DefaultAuthenticateScheme = OktaDefaults.ApiAuthenticationScheme;
                    authConfig.DefaultChallengeScheme = OktaDefaults.ApiAuthenticationScheme;
                    authConfig.DefaultSignInScheme = OktaDefaults.ApiAuthenticationScheme;
                }
            ).AddOktaWebApi(new OktaWebApiOptions()
                {
                    OktaDomain = options.Domain,
                }
            );
            
            // Suppressing the validation of the JWT signing key and token lifetime 
            // in the development environment allows for mock tokens to be hard coded  
            // in the REST Client .http files used to manually validate the API is 
            // working correctly
            if (options.Environment.IsDevelopment())
            {
                services.PostConfigure<JwtBearerOptions>(OktaDefaults.ApiAuthenticationScheme, options => 
                    {
                        options.TokenValidationParameters.ValidateIssuerSigningKey = false;
                        options.TokenValidationParameters.ValidateLifetime = false;
                    }
                );
            }

            return services;
        }
    }
}