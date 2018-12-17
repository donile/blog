using System.ComponentModel.DataAnnotations;

namespace MarkDonile.Blog.Admin.ViewModels.User
{
    public class AddUserViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}