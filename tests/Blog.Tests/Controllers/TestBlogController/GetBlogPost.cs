using AutoFixture;
using AutoMapper;
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
            
            var mockMapper = new Mock<IMapper>();
            
            var sut = new BlogPostController(mockBlogPostRepository.Object, mockMapper.Object);
            var actual = sut.GetBlogPost(id);

            Assert.That(actual, Is.TypeOf<OkObjectResult>());
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
            
            var mockMapper = new Mock<IMapper>();
            
            var sut = new BlogPostController(mockBlogPostRepository.Object, mockMapper.Object);
            var actual = sut.GetBlogPost(id);

            Assert.That(actual, Is.TypeOf<NotFoundResult>());
        }
    }
}