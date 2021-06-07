using AutoFixture;
using AutoMapper;
using MarkDonile.Blog.Controllers;
using MarkDonile.Blog.DataAccess;
using MarkDonile.Blog.Dto;
using MarkDonile.Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Blog.Tests
{
    [TestFixture]
    public class CreateBlogPost
    {        
        [Test]
        public void If_CreateBlogPostDto_Is_Valid_Return_CreatedResult()
        {
            var fixture = new Fixture();
            var blogPostToCreate = fixture.Create<CreateBlogPostDto>();
            var blogPost = fixture.Create<BlogPost>();

            var mockBlogPostRepository = new Mock<IBlogPostRepository>();

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<BlogPost>(blogPostToCreate)).Returns(blogPost);
            
            var controller = new BlogPostController(mockBlogPostRepository.Object, mockMapper.Object);

            var result = controller.CreateBlogPost(blogPostToCreate);

            Assert.That(result, Is.InstanceOf<CreatedAtRouteResult>());
        }
    }
}