using AutoFixture;
using MarkDonile.Blog.Controllers;
using MarkDonile.Blog.DataAccess;
using MarkDonile.Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;


namespace Blog.Tests
{
    [TestFixture]
    public class GetBlogPost
    {
        [Test]
        public void If_BlogPostRepository_Returns_BlogPost_Then_Return_OkResult()
        {
            var fixture = new Fixture();
            var id = fixture.Create<int>();

            var mockBlogPostRepository = new Mock<IBlogPostRepository>();
            mockBlogPostRepository
                .Setup(x => x.Get(id))
                .Returns(fixture.Create<BlogPost>());
            
            var sut = new BlogPostController(mockBlogPostRepository.Object);
            var actual = sut.GetBlogPost(id);

            Assert.That(actual, Is.TypeOf<OkResult>());
        }

        [Test]
        public void If_BlogPostRepository_Returns_Null_Then_Return_NotFoundResult()
        {
            var fixture = new Fixture();
            var id = fixture.Create<int>();

            var mockBlogPostRepository = new Mock<IBlogPostRepository>();
            mockBlogPostRepository
                .Setup(x => x.Get(id))
                .Returns<BlogPost>(null);
            
            var sut = new BlogPostController(mockBlogPostRepository.Object);
            var actual = sut.GetBlogPost(id);

            Assert.That(actual, Is.TypeOf<NotFoundResult>());
        }
    }
}