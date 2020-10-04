using Microsoft.AspNetCore.Http.Features;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TodoListStart.IntegrationTests.Support;
using TodoListStart.IntegrationTests.Support.Builder;
using TodoListStart.Application.Constants;
using FluentAssertions;

namespace TodoListStart.IntegrationTests.Tests.TodoItem
{
    [TestClass]
    public class TodoItemBasicErrorTests : TestBase
    {
        public List<string> ExpectedPostOrUpdateErrors = new List<string>
        {
            Errors.ItemTitleEmpty,
            Errors.ItemTitleLength
        };
        [TestMethod]
        public void CreateIncorrectTodoItem()
        {
            // Arange
            var list = Data.AddTodoList();
            var todoItemValue = TodoItemValueBuilder
                .CreateDefaultBuilder()
                .Configure(i =>
                {
                    i.Title = "";
                    i.TodoListId = list.Id;
                })
                .Build();

            // Act
            var result = Facade.PostTodoItem(todoItemValue);

            // Assert
            result.ErrorTitle.Should().Be(Errors.ErrorTitle);
            result.Errors.Should().BeEquivalentTo(ExpectedPostOrUpdateErrors);
        }
        [TestMethod]
        public void UpdateIncorrectTodoItem()
        {
            // Assert
            var itemId = Data.AddTodoItem().Id;
            var item = Facade.GetTodoItemById(itemId).Value;
            item.Title = "";

            // Act
            var result = Facade.PutTodoItem(item);

            // Assert
            result.ErrorTitle.Should().Be(Errors.ErrorTitle);
            result.Errors.Should().BeEquivalentTo(ExpectedPostOrUpdateErrors);
        }
        [TestMethod]
        public void UpdateNotExistTodoItem()
        {
            // Assert
            var itemId = Data.AddTodoItem().Id;
            var item = Facade.GetTodoItemById(itemId).Value;
            item.Title = "New Title Name";
            item.Id += 1;

            // Act
            var result = Facade.PutTodoItem(item);

            // Assert
            result.ErrorTitle.Should().Be(Errors.NotFoundError);
        }
        [TestMethod]
        public void GetNotExistTodoItem()
        {
            // Assert
            var list = Data.AddTodoList();
            var item = Data.AddTodoItem(todoListId: list.Id);

            // Act
            var result = Facade.GetTodoItemById(item.Id + 1);

            // Assert
            result.ErrorTitle.Should().Be(Errors.NotFoundError);
        }
        [TestMethod]
        public void DeleteNotExistTodoItem()
        {
            // Assert
            var list = Data.AddTodoList();
            var item = Data.AddTodoItem(todoListId: list.Id);

            // Act
            var result = Facade.DeleteTodoItem(item.Id + 1);

            // Assert
            result.ErrorTitle.Should().Be(Errors.NotFoundError);
        }
    }
}
