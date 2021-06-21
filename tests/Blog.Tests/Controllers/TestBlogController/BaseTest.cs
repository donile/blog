using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using MarkDonile.Blog.Controllers;
using MarkDonile.Blog.DataAccess;
using Moq;
using NUnit.Framework;

namespace Blog.Tests.Controllers.TestBlogController
{
    public class BaseTest
    {
        protected Fixture _fixture;
        protected Mock<IBlogPostRepository> _mockBlogPostRepository;
        protected Mock<IMapper> _mockMapper;
        protected BlogPostController _sut;

        [SetUp]
        public virtual void SetUp()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
            _mockBlogPostRepository = _fixture.Freeze<Mock<IBlogPostRepository>>();
            _mockMapper = _fixture.Freeze<Mock<IMapper>>();
            _sut = _fixture.Build<BlogPostController>().OmitAutoProperties().Create();

        }
    }
}
