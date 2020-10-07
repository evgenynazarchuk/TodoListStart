using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TodoListStart.IntegrationTests.Support;
using TodoListStart.IntegrationTests.Support.Builder;
using TodoListStart.Application.Constants;

namespace TodoListStart.IntegrationTests.Tests.TodoList
{
    [TestClass]
    public class ListNoteErrorTests : TestBase
    {
        [TestMethod]
        public void CreateIncorrectTodoList()
        {
            // Arange
            var list = ListNoteValueBuilder
                .CreateDefaultBuilder()
                .Configure(l =>
                {
                    l.Title = "";
                })
                .Build();

            // Act
            var result = Facade.PostTodoList(list);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { ErrorMessages.ListNoteEmpty });
        }
        [TestMethod]
        public void UpdateInCorrectTodoList()
        {
            // Arange
            var listId = Data.AddTodoList().Id;
            var list = Facade.GetTodoListById(listId).Value;
            list.Title = "";

            // Act
            var result = Facade.PutTodoList(list);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { ErrorMessages.ListNoteEmpty });
        }
        [TestMethod]
        public void UpdateNotExistTodoList()
        {
            // Arange
            var listId = Data.AddTodoList().Id;
            var list = Facade.GetTodoListById(listId).Value;
            list.Id += 1;

            // Act
            var result = Facade.PutTodoList(list);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { ErrorMessages.NotFound });
        }
        [TestMethod]
        public void DeleteNotExistTodoList()
        {
            // Arange
            var listId = Data.AddTodoList().Id;

            // Act
            var result = Facade.DeleteTodoList(listId + 1);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { ErrorMessages.NotFound });
        }
        [TestMethod]
        public void GetNotExistTodoList()
        {
            // Arange
            // Act
            var result = Facade.GetTodoListById(100);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { ErrorMessages.NotFound });
        }
    }
}
