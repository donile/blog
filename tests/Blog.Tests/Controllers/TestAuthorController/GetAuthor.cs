using System;
using AutoFixture;
using MarkDonile.Blog.Controllers;
using MarkDonile.Blog.DataAccess;
using MarkDonile.Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Blog.Tests
{
    [TestFixture]
    public class GetAuthor {

        [Test]
        public void Given_Author_Is_In_Repository_Return_OkObjectResult() {

            // arrange
            var fixture = new Fixture();
            var authorId = fixture.Create<Guid>();
            fixture.Register(() => new Author { Id = authorId });
            var author = fixture.Create<Author>();

            var mockAuthorRepository = new Mock<IAuthorRepository>();
            mockAuthorRepository
                .Setup(repository => repository.Get(authorId))
                .Returns(author);
            
            var sut = new AuthorController(mockAuthorRepository.Object);

            // act
            var actual = sut.GetAuthor(authorId);
            var actionResult = actual.Result as OkObjectResult;
            var returnedValue = actionResult.Value as Author;

            // assert
            Assert.That(actual, Is.InstanceOf<ActionResult<Author>>());
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());
            Assert.That(returnedValue, Is.InstanceOf<Author>());
            Assert.That(returnedValue.Id, Is.EqualTo(authorId));
        }

        [Test]
        public void Given_Author_Is_Not_Found_In_Repository_Return_NotFoundObjectResult() {

            // arrange
            var fixture = new Fixture();
            var authorId = fixture.Create<Guid>();

            var mockAuthorRepository = new Mock<IAuthorRepository>();
            mockAuthorRepository
                .Setup(repository => repository.Get(authorId))
                .Returns<Author>(null);

            var sut = new AuthorController(mockAuthorRepository.Object);

            // act
            var actual = sut.GetAuthor(authorId);

            // assert
            Assert.That(actual.Result, Is.InstanceOf<NotFoundObjectResult>());
        }
    }
}