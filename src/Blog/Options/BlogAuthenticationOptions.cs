using Microsoft.AspNetCore.Hosting;

namespace MarkDonile.Blog.Options
{
    public class BlogAuthenticationOptions
    {
        public string Audience { get; set; }
        public string Domain { get; set; }
        public IWebHostEnvironment Environment { get; set; }
        public string Issuer { get; set; }
    }
}