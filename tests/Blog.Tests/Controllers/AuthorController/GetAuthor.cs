using AutoFixture;
using MarkDonile.Blog.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;

namespace Blog.Tests.Controllers.TestAuthorController
{
    [TestFixture]
    public class GetAuthor : BaseTest 
    {
        protected Guid _authorId;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _authorId = _fixture.Create<Guid>();
        }
        
        [Test]
        public void Given_Author_Is_In_Repository_Return_OkObjectResult() {

            // arrange
            _fixture.Register(() => new Author { Id = _authorId });
            var author = _fixture.Create<Author>();

            _mockAuthorRepository
                .Setup(repository => repository.Get(_authorId))
                .Returns(author);

            // act
            var actual = _sut.GetAuthor(_authorId);
            var actionResult = actual.Result as OkObjectResult;
            var returnedValue = actionResult.Value as Author;

            // assert
            Assert.That(actual, Is.InstanceOf<ActionResult<Author>>());
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());
            Assert.That(returnedValue, Is.InstanceOf<Author>());
            Assert.That(returnedValue.Id, Is.EqualTo(_authorId));
        }

        [Test]
        public void Given_Author_Is_Not_Found_In_Repository_Return_NotFoundObjectResult() {

            // arrange
            _mockAuthorRepository
                .Setup(repository => repository.Get(_authorId))
                .Returns<Author>(null);
            
            // act
            var actual = _sut.GetAuthor(_authorId);

            // assert
            Assert.That(actual.Result, Is.InstanceOf<NotFoundObjectResult>());
        }
    }
}