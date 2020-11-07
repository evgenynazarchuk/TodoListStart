using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TodoListStart.Application.Controllers;
using AutoMapper;
using TodoListStart.IntegrationTests.Support.Builder;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;
using System;
using TodoListStart.Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TodoListStart.UnitTests.ControllerTests.TodoListTests
{
    /*[TestClass]
    public class TodoListControllerTests
    {
        [TestMethod]
        public void ListNotePostShouldBeAddListNote()
        {
            // Arrange
            #region init_data
            var todoListCreatedTime = DateTime.Now;
            var todoListModel = new ListNote()
            {
                Id = 1,
                Title = "List",
                CreatedDate = todoListCreatedTime
            };
            var toValue = new ListNoteValue()
            {
                Id = 1,
                Title = "List",
                CreatedDate = todoListCreatedTime
            };
            var toModel = new ListNote()
            {
                Id = 1,
                Title = "List",
                CreatedDate = todoListCreatedTime
            };
            var todoListValue = ListNoteValueBuilder
                .CreateDefaultBuilder()
                .Build();
            #endregion init_data

            #region dependecy
            var mockRepo = new Mock<IRepository>();
            var mockMap = new Mock<IMapper>();
            var mockValidator = new Mock<IValidationService>();
            mockMap.Setup(m => m.Map<ListNoteValue, ListNote>(It.IsAny<ListNoteValue>())).Returns(toModel);
            mockRepo.Setup(r => r.Add(It.IsAny<ListNote>())).ReturnsAsync(todoListModel);
            mockMap.Setup(m => m.Map<ListNote, ListNoteValue>(It.IsAny<ListNote>())).Returns(toValue);
            mockValidator.Setup(v => v.ValidateListNote(It.IsAny<ListNoteValue>(), "POST")).ReturnsAsync(new List<string>());
            var todoListController = new ListNoteController(mockRepo.Object, mockMap.Object, mockValidator.Object);
            #endregion dependency

            // Act
            var response = todoListController.Post(todoListValue).GetAwaiter().GetResult();

            // Assert
            response.Should().BeOfType<OkObjectResult>();
            var responseObjs = response as OkObjectResult;
            responseObjs.Value.Should().BeOfType<ListNoteValue>();
            var todoListValueResponse = (responseObjs.Value as ListNoteValue);

            responseObjs.StatusCode.Should().Be(200);
            todoListValueResponse.Should().Be(toValue);
        }
    }*/
}
