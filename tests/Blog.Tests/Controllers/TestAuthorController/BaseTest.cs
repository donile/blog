using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using MarkDonile.Blog.Controllers;
using MarkDonile.Blog.DataAccess;
using Moq;
using NUnit.Framework;

namespace Blog.Tests.Controllers.TestAuthorController
{
    public class BaseTest
    {
        protected Fixture _fixture;
        protected Mock<IAuthorRepository> _mockAuthorRepository;
        protected Mock<IMapper> _mockMapper;
        protected AuthorController _sut;

        [SetUp]
        public virtual void SetUp()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
            _mockAuthorRepository = _fixture.Freeze<Mock<IAuthorRepository>>();
            _mockMapper = _fixture.Freeze<Mock<IMapper>>();
            _sut = _fixture.Build<AuthorController>().OmitAutoProperties().Create();
        }
    }
}