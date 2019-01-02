using System.Collections.Generic;
using MarkDonile.Blog.Models;

namespace MarkDonile.Blog.Admin.ViewModels.User{
    public class UserListViewModel{
        public IEnumerable<AppUser> AppUsers { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}