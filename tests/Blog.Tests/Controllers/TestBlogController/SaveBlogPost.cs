using AutoFixture;
using MarkDonile.Blog.Dto;
using MarkDonile.Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;

namespace Blog.Tests.Controllers.TestBlogController
{
    [TestFixture]
    public class SaveBlogPost : BaseTest
    {
        Guid _blogPostId;
        CreateBlogPostDto _blogPostDto;
        BlogPost _blogPost;

        [SetUp]
        public void Setup()
        {
            _blogPostId = _fixture.Create<Guid>();
            _blogPostDto = _fixture.Create<CreateBlogPostDto>();
            _blogPost = _fixture.Create<BlogPost>();
        }

        [Test]
        public void When_BlogPost_Is_Not_Found_Return_CreatedAtRouteResult()
        {
            // arrange
            _mockBlogPostRepository
                .Setup(repo => repo.Get(_blogPostId))
                .Returns<BlogPost>(null);
            
            _mockMapper
                .Setup(mapper => mapper.Map<BlogPost>(_blogPostDto))
                .Returns(_blogPost);

            // act
            var actual = _sut.SaveBlogPost(_blogPostId, _blogPostDto);

            // assert
            Assert.That(actual, Is.InstanceOf<CreatedAtRouteResult>());
        }

        [Test]
        public void When_BlogPost_Is_Found_Return_OkObjectResult() {
            
            // arrange
            _mockBlogPostRepository
                .Setup(repo => repo.Get(_blogPostId))
                .Returns(_blogPost);
            
            _mockMapper
                .Setup(mapper => mapper.Map<BlogPost>(_blogPostDto))
                .Returns(_blogPost);

            // act
            var actual = _sut.SaveBlogPost(_blogPostId, _blogPostDto);

            // assert
            Assert.That(actual, Is.InstanceOf<OkResult>());
        }

        [Test]
        public void Given_BlogPost_Is_Found_Then_Update_Repository_With_Received_BlogPost() {
            
            // arrange
            _mockBlogPostRepository
                .Setup(repo => repo.Get(_blogPostId))
                .Returns(_blogPost);
            
            _mockMapper
                .Setup(mapper => mapper.Map<BlogPost>(_blogPostDto))
                .Returns(_blogPost);

            // act
            var actual = _sut.SaveBlogPost(_blogPostId, _blogPostDto);

            // assert
            _mockMapper.Verify(mapper => mapper.Map(_blogPostDto, _blogPost), Times.Once);
            _mockBlogPostRepository.Verify(repo => repo.Update(_blogPost), Times.Once);
            _mockBlogPostRepository.Verify(repo => repo.SaveChanges(), Times.Once);
        }
    }
}
