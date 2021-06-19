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
        [Test]
        public void THEN_save_Author_in_repository()
        {
            // arrange
            var fixture = new Fixture();
            var createAuthorDto = fixture.Create<CreateAuthorDto>();
            var author = new Author();

            var mockAuthorRepository = new Mock<IAuthorRepository>();

            var mockMapper = new Mock<IMapper>();
            mockMapper
                .Setup(mapper => mapper.Map<Author>(createAuthorDto))
                .Returns(author);

            var sut = new AuthorController(mockAuthorRepository.Object, mockMapper.Object);

            // act
            var actual = sut.PostAuthor(createAuthorDto);

            // assert
            mockAuthorRepository.Verify(repository => repository.Add(author), Times.Once);
            mockAuthorRepository.Verify(repository => repository.SaveChanges(), Times.Once);
        }

        [Test]
        public void THEN_return_CreatedAtRouteObjectResult()
        {
            // arrange
            var fixture = new Fixture();
            var createAuthorDto = fixture.Create<CreateAuthorDto>();
            var author = new Author();

            var mockAuthorRepository = new Mock<IAuthorRepository>();

            var mockMapper = new Mock<IMapper>();
            mockMapper
                .Setup(mapper => mapper.Map<Author>(createAuthorDto))
                .Returns(author);

            var sut = new AuthorController(mockAuthorRepository.Object, mockMapper.Object);

            // act
            var actionResult = sut.PostAuthor(createAuthorDto);
            var result = actionResult.Result as CreatedAtRouteResult;
            var value = result.Value; 

            // assert
            Assert.That(actionResult, Is.InstanceOf<ActionResult<Author>>());
            Assert.That(result, Is.InstanceOf<CreatedAtRouteResult>());
            Assert.That(value, Is.InstanceOf<Author>());
        }
    }
}
