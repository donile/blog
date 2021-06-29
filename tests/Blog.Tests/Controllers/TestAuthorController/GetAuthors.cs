using System.Collections.Generic;
using AutoFixture;
using MarkDonile.Blog.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace Blog.Tests.Controllers.TestAuthorController
{
    [TestFixture]
    public class GetAuthors : BaseTest
    {
        [Test]
        public void GIVEN_repository_contains_zero_authors_THEN_return_an_empty_list()
        {
            // arrange
            _mockAuthorRepository
                .Setup(repository => repository.GetAll())
                .Returns(new List<Author>());

            // act
            var actual = _sut.GetAuthors();
            var actionResult = actual.Result as OkObjectResult;
            var returnedValue = actionResult.Value;

            // assert
            Assert.That(actual, Is.InstanceOf<ActionResult<IEnumerable<Author>>>());
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());
            Assert.That(returnedValue, Is.InstanceOf<IEnumerable<Author>>());
            Assert.That(returnedValue, Is.Empty);
        }

        [Test]
        public void GIVEN_repository_contains_one_author_THEN_return_an_list_with_one_author() 
        {
            // arrange
            var author = _fixture.Create<Author>();

            _mockAuthorRepository
                .Setup(repository => repository.GetAll())
                .Returns(new Author[] { author });

            // act
            var actual = _sut.GetAuthors();
            var actionResult = actual.Result as OkObjectResult;
            var returnedValue = actionResult.Value;

            // assert
            Assert.That(actual, Is.InstanceOf<ActionResult<IEnumerable<Author>>>());
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());
            Assert.That(returnedValue, Is.InstanceOf<IEnumerable<Author>>());
            Assert.That(returnedValue, Has.Exactly(1).Items);
        }

        [Test]
        public void GIVEN_repository_contains_n_authors_THEN_return_an_list_with_n_authors() 
        {
            // arrange
            var author1 = _fixture.Create<Author>();
            var author2 = _fixture.Create<Author>();
            var authors = new Author[] { author1, author2 };

            _mockAuthorRepository
                .Setup(repository => repository.GetAll())
                .Returns(authors);

            // act
            var actual = _sut.GetAuthors();
            var actionResult = actual.Result as OkObjectResult;
            var returnedValue = actionResult.Value;

            // assert
            Assert.That(actual, Is.InstanceOf<ActionResult<IEnumerable<Author>>>());
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());
            Assert.That(returnedValue, Is.InstanceOf<IEnumerable<Author>>());
            Assert.That(returnedValue, Has.Exactly(authors.Length).Items);
        }
    }
}