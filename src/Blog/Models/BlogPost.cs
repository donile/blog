using System;

namespace MarkDonile.Blog.Models
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Post { get; set; }
        public DateTime WrittenDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public String Author { get; set; }
        public String Title { get; set; }
    }
}