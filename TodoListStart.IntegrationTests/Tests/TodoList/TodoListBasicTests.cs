using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TodoListStart.IntegrationTests.Support;
using TodoListStart.IntegrationTests.Support.Builder;

namespace TodoListStart.IntegrationTests.Tests.TodoList
{
    [TestClass]
    public class TodoListBasicTests : TestBase
    {
        public static readonly DateTime createTime = new DateTime(2019, 01, 01, 10, 01, 01);
        public static readonly DateTime modifyTime = new DateTime(2019, 01, 01, 10, 01, 01);
        public static readonly string newTitle = "New Title";
        public static readonly string newDescription = "New Description";
        [TestMethod]
        public void AddTodoList()
        {
            // Arange
            var todoListValue = TodoListValueBuilder
                .CreateDefaultBuilder()
                .Build();

            // Act
            Date.SetCurrentDateTime(createTime);
            var todoList = Facade.PostTodoList(todoListValue).Value;

            // Assert
            todoList.Title.Should().Be("Title");
            todoList.Description.Should().Be("Description");
            todoList.CreatedDate.Should().Be(createTime);
            todoList.ModifiedDate.Should().BeNull();
        }
        [TestMethod]
        public void UpdateTodoList()
        {
            // Arange
            Date.SetCurrentDateTime(createTime);
            var todoListId = Data.AddTodoList().Id;
            var todoList = Facade.GetTodoListById(todoListId).Value;
            todoList.Title = newTitle;
            todoList.Description = newDescription;

            // Act
            Date.SetCurrentDateTime(modifyTime);
            Facade.PutTodoList(todoList);

            // Assert
            var result = Facade.GetTodoListById(todoList.Id).Value;
            result.Title.Should().Be(newTitle);
            result.Description.Should().Be(newDescription);
            result.CreatedDate.Should().Be(createTime);
            result.ModifiedDate.Should().Be(modifyTime);
        }
        [TestMethod]
        public void GetTodoList()
        {
            // Arange
            Date.SetCurrentDateTime(createTime);
            var todoId = Data.AddTodoList().Id;

            // Act
            var todoList = Facade.GetTodoListById(todoId).Value;

            // Assert
            todoList.Title.Should().Be("Title");
            todoList.Description.Should().Be("Description");
            todoList.CreatedDate.Should().Be(createTime);
            todoList.ModifiedDate.Should().BeNull();
        }
        [TestMethod]
        public void GetTodoLists()
        {
            // Arange
            Date.SetCurrentDateTime(createTime);
            Data.AddTodoList();
            Data.AddTodoList();
            Data.AddTodoList();

            // Act
            var todoLists = Facade.GetTodoLists().Value;

            // Assert
            todoLists.Count.Should().Be(3);
        }
        [TestMethod]
        public void DeleteTodoList()
        {
            // Arange
            var listId = Data.AddTodoList().Id;

            // Act
            var result = Facade.DeleteTodoList(listId).Value;

            // Assert
            result.Should().BeTrue();
        }
        [TestMethod]
        public void DeleteTodoListWithItems()
        {
            // Arange
            var todoList1 = Data.AddTodoList();
            var todoList2 = Data.AddTodoList();
            Data.AddTodoItem(todoListId: todoList1.Id);
            Data.AddTodoItem(todoListId: todoList1.Id);
            Data.AddTodoItem(todoListId: todoList2.Id);
            Data.AddTodoItem(todoListId: todoList2.Id);
            Data.AddTodoItem(todoListId: todoList2.Id);

            // Act
            Facade.DeleteTodoList(todoList1.Id);

            // Assert
            var items = Facade.GetTodoItems().Value;
            items.Count.Should().Be(3);
        }
    }
}
