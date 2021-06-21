using AutoFixture;
using MarkDonile.Blog.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;


namespace Blog.Tests.Controllers.TestBlogController
{
    [TestFixture]
    public class GetBlogPost : BaseTest
    {
        protected int _blogPostId;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _blogPostId = _fixture.Create<int>();
        }

        [Test]
        public void If_BlogPostRepository_Returns_BlogPost_Then_Return_OkResult()
        {
            // arrange
            _mockBlogPostRepository
                .Setup(x => x.Get(_blogPostId))
                .Returns(_fixture.Create<BlogPost>());
            
            // act
            var actual = _sut.GetBlogPost(_blogPostId);

            // assert
            Assert.That(actual, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void If_BlogPostRepository_Returns_Null_Then_Return_NotFoundResult()
        {
            // arrange
            _mockBlogPostRepository
                .Setup(x => x.Get(_blogPostId))
                .Returns<BlogPost>(null);
            
            // act
            var actual = _sut.GetBlogPost(_blogPostId);

            // assert
            Assert.That(actual, Is.TypeOf<NotFoundResult>());
        }
    }
}
