using FluentAssertions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TodoListStart.IntegrationTests.Support;
using TodoListStart.IntegrationTests.Support.Builder;

namespace TodoListStart.IntegrationTests.Tests.TodoItem
{
    [TestClass]
    public class TodoItemBasicTests : TestBase
    {
        [TestMethod]
        public void CreateTodoItem()
        {
            // Arange
            var list = Data.AddTodoList();
            var todoItemValue = TodoItemValueBuilder
                .CreateDefaultBuilder()
                .Configure(i => i.TodoListId = list.Id)
                .Build();

            // Act
            Date.SetCurrentDateTime(new DateTime(2020, 02, 03));
            var item = Facade.PostTodoItem(todoItemValue).Value;

            // Assert
            item.Title.Should().Be("Title");
            item.Body.Should().Be("Body");
            item.DueDate.Should().Be(new DateTime().AddMonths(1));
            item.IsCompleted.Should().BeFalse();
            item.CreatedDate.Should().Be(new DateTime(2020, 02, 03));
            item.ModifiedDate.Should().BeNull();
        }
        [TestMethod]
        public void UpdateTodoItem()
        {
            // Arange
            Date.SetCurrentDateTime(new DateTime(2020, 02, 03));
            var itemId = Data.AddTodoItem().Id;
            var newTodoListId = Data.AddTodoList().Id;
            var item = Facade.GetTodoItemById(itemId).Value;
            item.IsCompleted = true;
            item.Title = "New Title";
            item.Body = "New Body";
            item.TodoListId = newTodoListId;

            // Act
            Date.SetCurrentDateTime(new DateTime(2020, 03, 03));
            Facade.PutTodoItem(item);

            // Assert
            var result = Facade.GetTodoItemById(itemId).Value;
            result.Title.Should().Be("New Title");
            result.Body.Should().Be("New Body");
            result.DueDate.Should().Be(new DateTime().AddMonths(1));
            result.IsCompleted.Should().BeTrue();
            result.CreatedDate.Should().Be(new DateTime(2020, 02, 03));
            result.ModifiedDate.Should().Be(new DateTime(2020, 03, 03));
            result.TodoListId.Should().Be(newTodoListId);
        }
        [TestMethod]
        public void GetTodoItems()
        {
            // Arange
            var listId = Data.AddTodoList().Id;
            Data.AddTodoItem(todoListId: listId);
            Data.AddTodoItem(todoListId: listId);
            Data.AddTodoItem(todoListId: listId);

            // Act
            var result = Facade.GetTodoItems().Value;

            // Assert
            result.Count.Should().Be(3);
        }
        [TestMethod]
        public void DeleteTodoItem()
        {
            // Arange
            Data.AddTodoItem();
            var itemId = Data.AddTodoItem().Id;

            // Act
            var result = Facade.DeleteTodoItem(itemId);
            var items = Facade.GetTodoItems().Value;

            // Assert
            result.Value.Should().BeTrue();
            items.Count.Should().Be(1);
        }
    }
}
