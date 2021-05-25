using System;

namespace MarkDonile.Blog.Dto
{
    public class CreateBlogPostDto
    {
        public string Post { get; set; }
        public DateTime WrittenDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public String Author { get; set; }
        public String Title { get; set; }
    }
}