using AutoFixture;
using MarkDonile.Blog.Dto;
using MarkDonile.Blog.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace Blog.Tests.Controllers.TestBlogController
{
    [TestFixture]
    public class CreateBlogPost : BaseTest
    {        
        [Test]
        public void If_CreateBlogPostDto_Is_Valid_Return_CreatedResult()
        {
            // arrange
            var blogPostToCreate = _fixture.Create<CreateBlogPostDto>();
            var blogPost = _fixture.Create<BlogPost>();

            _mockMapper
                .Setup(m => m.Map<BlogPost>(blogPostToCreate))
                .Returns(blogPost);
            
            // act
            var result = _sut.CreateBlogPost(blogPostToCreate);

            // assert
            Assert.That(result, Is.InstanceOf<CreatedAtRouteResult>());
        }
    }
}
