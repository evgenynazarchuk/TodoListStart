using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TodoListStart.IntegrationTests.Support;
using TodoListStart.IntegrationTests.Support.Builder;
using TodoListStart.Application.Constants;
using FluentAssertions;

namespace TodoListStart.IntegrationTests.Tests.TodoItem
{
    [TestClass]
    public class TodoItemBasicErrorTests : TestBase
    {
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
            result.Errors.Should().BeEquivalentTo(new List<string>
            {
                Errors.ItemTitleEmpty,
                Errors.ItemTitleLength
            });
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
            result.Errors.Should().BeEquivalentTo(new List<string>
            {
                Errors.ItemTitleEmpty,
                Errors.ItemTitleLength
            });
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
        [TestMethod]
        public void CreateExistItemTitle()
        {
            // Arange
            var listId = Data.AddTodoList().Id;
            Data.AddTodoItem(todoListId: listId);
            var newItemValue = TodoItemValueBuilder
                .CreateDefaultBuilder()
                .Configure(i => i.TodoListId = listId)
                .Build();

            // Act
            var result = Facade.PostTodoItem(newItemValue);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { Errors.TodoItemNotUnique });
        }
        [TestMethod]
        public void UpdateExistItemTitleWithExistTitleName()
        {
            // Arange
            var listId = Data.AddTodoList().Id;
            Data.AddTodoItem(todoListId: listId);
            var item = TodoItemValueBuilder.CreateDefaultBuilder().Configure(i => i.Title = "Any title name").Build();
            var itemId = Data.AddTodoItem(item, todoListId: listId).Id;
            var itemValue = Facade.GetTodoItemById(itemId).Value;
            itemValue.Title = "Title";

            // Act
            var result = Facade.PutTodoItem(itemValue);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { Errors.TodoItemNotUnique });
        }
        [TestMethod]
        public void CreateTodoItemWithNotExistTodoList()
        {
            // Arange
            var newItemValue = TodoItemValueBuilder
                .CreateDefaultBuilder()
                .Configure(i => i.TodoListId = 1)
                .Build();
            // Act
            var result = Facade.PostTodoItem(newItemValue);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { Errors.TodoListNotExist });
        }
        [TestMethod]
        public void UpdateExistItemWithNonExistTodoList()
        {
            // Arange
            var itemId = Data.AddTodoItem().Id;
            var itemValue = Facade.GetTodoItemById(itemId).Value;
            itemValue.TodoListId = 2;

            // Act
            var result = Facade.PutTodoItem(itemValue);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { Errors.TodoListNotExist });
        }
    }
}
