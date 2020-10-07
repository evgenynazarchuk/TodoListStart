using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TodoListStart.IntegrationTests.Support;
using TodoListStart.IntegrationTests.Support.Builder;
using TodoListStart.Application.Constants;
using FluentAssertions;

namespace TodoListStart.IntegrationTests.Tests.TodoItem
{
    [TestClass]
    public class NoteErrorTests : TestBase
    {
        [TestMethod]
        public void CreateIncorrectTodoItem()
        {
            // Arange
            var list = Data.AddTodoList();
            var todoItemValue = NoteValueBuilder
                .CreateDefaultBuilder()
                .Configure(i =>
                {
                    i.Text = "";
                    i.ListNoteId = list.Id;
                })
                .Build();

            // Act
            var result = Facade.PostNote(todoItemValue);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string> { ErrorMessages.NoteEmpty });
        }
        [TestMethod]
        public void UpdateIncorrectTodoItem()
        {
            // Assert
            var itemId = Data.AddTodoItem().Id;
            var item = Facade.GetNoteById(itemId).Value;
            item.Text = "";

            // Act
            var result = Facade.PutNote(item);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string> { ErrorMessages.NoteEmpty });
        }
        [TestMethod]
        public void UpdateNotExistTodoItem()
        {
            // Assert
            var itemId = Data.AddTodoItem().Id;
            var item = Facade.GetNoteById(itemId).Value;
            item.Text = "New Title Name";
            item.Id += 1;

            // Act
            var result = Facade.PutNote(item);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { ErrorMessages.NotFound });
        }
        [TestMethod]
        public void GetNotExistTodoItem()
        {
            // Assert

            // Act
            var result = Facade.GetNoteById(1);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { ErrorMessages.NotFound });
        }
        [TestMethod]
        public void DeleteNotExistTodoItem()
        {
            // Assert

            // Act
            var result = Facade.DeleteNote(1);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { ErrorMessages.NotFound });
        }
        [TestMethod]
        public void CreateExistItemTitle()
        {
            // Arange
            var listId = Data.AddTodoList().Id;
            Data.AddTodoItem(listNoteId: listId);
            var newItemValue = NoteValueBuilder
                .CreateDefaultBuilder()
                .Configure(i => i.ListNoteId = listId)
                .Build();

            // Act
            var result = Facade.PostNote(newItemValue);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { ErrorMessages.NoteNotUnique });
        }
        [TestMethod]
        public void UpdateExistItemTitleWithExistTitleName()
        {
            // Arange
            var listId = Data.AddTodoList().Id;
            Data.AddTodoItem(listNoteId: listId);
            var item = NoteValueBuilder
                .CreateDefaultBuilder()
                .Configure(i => i.Text = "Any title name")
                .Build();
            var itemId = Data.AddTodoItem(item, listNoteId: listId).Id;
            var itemValue = Facade.GetNoteById(itemId).Value;
            itemValue.Text = "Title";

            // Act
            var result = Facade.PutNote(itemValue);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { ErrorMessages.NoteNotUnique });
        }
        [TestMethod]
        public void CreateTodoItemWithNotExistTodoList()
        {
            // Arange
            var newItemValue = NoteValueBuilder
                .CreateDefaultBuilder()
                .Configure(i => i.ListNoteId = 1)
                .Build();
            // Act
            var result = Facade.PostNote(newItemValue);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { ErrorMessages.ListNoteNotExist });
        }
        [TestMethod]
        public void UpdateExistItemWithNonExistTodoList()
        {
            // Arange
            var itemId = Data.AddTodoItem().Id;
            var itemValue = Facade.GetNoteById(itemId).Value;
            itemValue.ListNoteId = 2;

            // Act
            var result = Facade.PutNote(itemValue);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { ErrorMessages.ListNoteNotExist });
        }
        [TestMethod]
        public void UpdateExistItemWithNonExistTodoListAndEmptyTitle()
        {
            // Arange
            var itemId = Data.AddTodoItem().Id;
            var itemValue = Facade.GetNoteById(itemId).Value;
            itemValue.ListNoteId = 2;
            itemValue.Text = "";

            // Act
            var result = Facade.PutNote(itemValue);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() 
            { 
                ErrorMessages.ListNoteNotExist,
                ErrorMessages.NoteEmpty
            });
        }
    }
}
