using System.ComponentModel.DataAnnotations;

namespace MarkDonile.Blog.Models
{
    public class Credentials
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}