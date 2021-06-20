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
    public class PostAuthor
    {
        protected Fixture _fixture;
        protected Mock<IAuthorRepository> _mockAuthorRepository;
        protected Mock<IMapper> _mockMapper;
        protected AuthorController _sut;
        protected CreateAuthorDto _createAuthorDto;
        protected Author _authorWithoutId;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _mockAuthorRepository = new Mock<IAuthorRepository>();
            _mockMapper = new Mock<IMapper>();

            _createAuthorDto = _fixture.Create<CreateAuthorDto>();
            _authorWithoutId = _fixture.Build<Author>().Without(a => a.Id).Create();

            _mockMapper
                .Setup(mapper => mapper.Map<Author>(_createAuthorDto))
                .Returns(_authorWithoutId);

            _sut = new AuthorController(_mockAuthorRepository.Object, _mockMapper.Object);
        }

        [Test]
        public void THEN_save_Author_in_repository()
        {
            // act
            var actual = _sut.PostAuthor(_createAuthorDto);

            // assert
            _mockAuthorRepository.Verify(repository => repository.Add(It.Is<Author>(a => a.Id == default)), Times.Once);
            _mockAuthorRepository.Verify(repository => repository.SaveChanges(), Times.Once);
        }

        [Test]
        public void THEN_return_CreatedAtRouteResult()
        {
            // act
            var actionResult = _sut.PostAuthor(_createAuthorDto);
            var result = actionResult.Result as CreatedAtRouteResult;
            var value = result.Value; 

            // assert
            Assert.That(actionResult, Is.InstanceOf<ActionResult<Author>>());
            Assert.That(result, Is.InstanceOf<CreatedAtRouteResult>());
            Assert.That(value, Is.InstanceOf<Author>());
        }
    }
}
