using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TodoListStart.IntegrationTests.Support;
using TodoListStart.IntegrationTests.Support.Builder;

namespace TodoListStart.IntegrationTests.Tests.TodoListCRUD
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
            var todoList = Facade.PostTodoList(todoListValue);

            // Assert
            Assert.AreEqual("Title", todoList.Title);
            Assert.AreEqual("Description", todoList.Description);
            Assert.AreEqual(createTime, todoList.CreatedDate);
            Assert.IsNull(todoList.ModifiedDate);
        }
        [TestMethod]
        public void UpdateTodoList()
        {
            // Arange
            Date.SetCurrentDateTime(createTime);
            var todoListId = Data.AddTodoList().Id;
            var todoList = Facade.GetTodoListById(todoListId);
            todoList.Title = newTitle;
            todoList.Description = newDescription;

            // Act
            Date.SetCurrentDateTime(modifyTime);
            Facade.PutTodoList(todoList);

            // Assert
            var result = Facade.GetTodoListById(todoList.Id);
            Assert.AreEqual(newTitle, result.Title);
            Assert.AreEqual(newDescription, result.Description);
            Assert.AreEqual(createTime, result.CreatedDate);
            Assert.AreEqual(modifyTime, result.ModifiedDate);
        }
    }
}
