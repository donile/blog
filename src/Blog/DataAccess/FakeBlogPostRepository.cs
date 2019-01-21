// using MarkDonile.Blog.Models;
// using System.Collections.Generic;
// using System.Linq;

// namespace MarkDonile.Blog.DataAccess
// {
//     public class FakeBlogPostRespository : IBlogPostRepository
//     {
//         public IQueryable<BlogPost> BlogPosts
//         {
//             get
//             {
//                 return new List<BlogPost>()
//                     {
//                         new BlogPost()
//                         {
//                             Title="First Blog Post",
//                             Post="Content of First Post",
//                             Author="Mark Donile",
//                             ReleaseDate=new System.DateTime(2018, 1, 1)
//                         },
//                         new BlogPost()
//                         {
//                             Title="Second Blog Post",
//                             Post="Content of Second Post",
//                             Author="Eric Ilg",
//                             ReleaseDate=new System.DateTime(2018, 2, 1)
//                         }
//                     }.AsQueryable();
//             }
//         }
//     }
// }