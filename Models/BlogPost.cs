using System;

namespace markdonile.com
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Post { get; set; }
        public DateTime WrittenDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public String Author { get; set; }
        public String Title { get; set; }
    }
}