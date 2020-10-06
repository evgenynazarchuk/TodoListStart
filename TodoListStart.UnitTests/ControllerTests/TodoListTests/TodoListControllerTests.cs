using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TodoListStart.Application.Controllers;
using AutoMapper;
using TodoListStart.Application.ApplicationServices;
using TodoListStart.IntegrationTests.Support.Builder;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;
using System;
using System.Threading.Tasks;
using TodoListStart.Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TodoListStart.UnitTests.ControllerTests.TodoListTests
{
    [TestClass]
    public class TodoListControllerTests
    {
        [TestMethod]
        public void TodoListPostRequest()
        {
            // Arrange
            #region init_data
            var todoListCreatedTime = DateTime.Now;
            var todoListModel = new TodoList()
            {
                Id = 1,
                Title = "List",
                Description = "Description",
                CreatedDate = todoListCreatedTime
            };
            var toValue = new TodoListValue()
            {
                Id = 1,
                Title = "List",
                Description = "Description",
                CreatedDate = todoListCreatedTime
            };
            var toModel = new TodoList()
            {
                Id = 1,
                Title = "List",
                Description = "Description",
                CreatedDate = todoListCreatedTime
            };
            var todoListValue = TodoListValueBuilder
                .CreateDefaultBuilder()
                .Build();
            #endregion init_data

            #region dependecy
            var mockRepo = new Mock<IRepository>();
            var mockMap = new Mock<IMapper>();
            var mockValidator = new Mock<IValidationService>();
            mockMap.Setup(m => m.Map<TodoListValue, TodoList>(It.IsAny<TodoListValue>())).Returns(toModel);
            mockRepo.Setup(r => r.AddAsync(It.IsAny<TodoList>())).ReturnsAsync(todoListModel);
            mockMap.Setup(m => m.Map<TodoList, TodoListValue>(It.IsAny<TodoList>())).Returns(toValue);
            mockValidator.Setup(v => v.ValidateTodoList(It.IsAny<TodoListValue>())).ReturnsAsync(new List<string>());
            var todoListController = new TodoListController(mockRepo.Object, mockMap.Object, mockValidator.Object);
            #endregion dependency

            // Act
            var response = todoListController.Post(todoListValue).GetAwaiter().GetResult();

            // Assert
            response.Should().BeOfType<OkObjectResult>();
            var responseObjs = response as OkObjectResult;
            responseObjs.Value.Should().BeOfType<TodoListValue>();
            var todoListValueResponse = (responseObjs.Value as TodoListValue);

            responseObjs.StatusCode.Should().Be(200);
            todoListValueResponse.Should().Be(toValue);
        }
    }
}
