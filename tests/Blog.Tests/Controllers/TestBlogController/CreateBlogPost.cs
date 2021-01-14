using MarkDonile.Blog.Controllers;
using MarkDonile.Blog.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Blog.Tests
{
    [TestFixture]
    public class CreateBlogPost
    {        
        [Test]
        public void If_CreateBlogPostDto_Is_Null_Return_Bad_Request()
        {
            var mockBlogPostRepository = new Mock<IBlogPostRepository>();
            var blogPostRepository = mockBlogPostRepository.Object;

            var controller = new BlogPostController(blogPostRepository);
            var result = controller.CreateBlogPost(null);

            Assert.That(result, Is.InstanceOf<BadRequestResult>());
        }
    }
}