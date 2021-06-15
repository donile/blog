using AutoFixture;
using AutoMapper;
using MarkDonile.Blog.Controllers;
using MarkDonile.Blog.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;

namespace Blog.Tests
{
    [TestFixture]
    public class RemoveBlogPost
    {
        [Test]
        public void Remove_BlogPost_From_Repository()
        {
            // arrange
            var fixture = new Fixture();
            var blogPostId = fixture.Create<Guid>(); 
            var mockBlogPostRepository = new Mock<IBlogPostRepository>();
            var mockMapper = new Mock<IMapper>();
            var sut = new BlogPostController(mockBlogPostRepository.Object, mockMapper.Object);

            // act
            sut.DeleteBlogPost(blogPostId);

            // assert
            mockBlogPostRepository.Verify(repository => repository.Remove(blogPostId), Times.Once);
            mockBlogPostRepository.Verify(repository => repository.SaveChanges(), Times.Once);
        }

        [Test]
        public void Returns_204_No_Content()
        {
            // arrange
            var fixture = new Fixture();
            var blogPostId = fixture.Create<Guid>(); 
            var mockBlogPostRepository = new Mock<IBlogPostRepository>();
            var mockMapper = new Mock<IMapper>();
            var sut = new BlogPostController(mockBlogPostRepository.Object, mockMapper.Object);

            // act
            var actual = sut.DeleteBlogPost(blogPostId);

            // assert
            Assert.That(actual, Is.InstanceOf<NoContentResult>());
        }

    }
}