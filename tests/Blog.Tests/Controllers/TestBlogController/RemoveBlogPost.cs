using AutoFixture;
using AutoMapper;
using MarkDonile.Blog.Controllers;
using MarkDonile.Blog.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;

namespace Blog.Tests.Controllers.TestBlogController
{
    [TestFixture]
    public class RemoveBlogPost : BaseTest
    {
        protected Guid _blogPostId;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _blogPostId = _fixture.Create<Guid>();
        }

        [Test]
        public void Remove_BlogPost_From_Repository()
        {
            // act
            _sut.DeleteBlogPost(_blogPostId);

            // assert
            _mockBlogPostRepository.Verify(repository => repository.Remove(_blogPostId), Times.Once);
            _mockBlogPostRepository.Verify(repository => repository.SaveChanges(), Times.Once);
        }

        [Test]
        public void Returns_204_No_Content()
        {
            // act
            var actual = _sut.DeleteBlogPost(_blogPostId);

            // assert
            Assert.That(actual, Is.InstanceOf<NoContentResult>());
        }

    }
}
