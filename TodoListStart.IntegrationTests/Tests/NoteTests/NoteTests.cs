using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TodoListStart.IntegrationTests.Support;
using TodoListStart.IntegrationTests.Support.Builder;

namespace TodoListStart.IntegrationTests.Tests.TodoItem
{
    [TestClass]
    public class NoteTests : TestBase
    {
        [TestMethod]
        public void CreateTodoItem()
        {
            // Arange
            var list = Data.AddTodoList();
            var todoItemValue = NoteValueBuilder
                .CreateDefaultBuilder()
                .Configure(i => i.ListNoteId = list.Id)
                .Build();

            // Act
            Date.SetCurrentDateTime(new DateTime(2020, 02, 03));
            var item = Facade.PostNote(todoItemValue).Value;

            // Assert
            item.Text.Should().Be("Title");
            item.DueDate.Should().BeNull();
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
            var item = Facade.GetNoteById(itemId).Value;
            item.IsCompleted = true;
            item.Text = "New Title";
            item.ListNoteId = newTodoListId;

            // Act
            Date.SetCurrentDateTime(new DateTime(2020, 03, 03));
            Facade.PutNote(item);

            // Assert
            var result = Facade.GetNoteById(itemId).Value;
            result.Text.Should().Be("New Title");
            result.DueDate.Should().BeNull();
            result.IsCompleted.Should().BeTrue();
            result.CreatedDate.Should().Be(new DateTime(2020, 02, 03));
            result.ModifiedDate.Should().Be(new DateTime(2020, 03, 03));
            result.ListNoteId.Should().Be(newTodoListId);
        }
        [TestMethod]
        public void GetTodoItems()
        {
            // Arange
            var listId = Data.AddTodoList().Id;
            Data.AddTodoItem(listNoteId: listId);
            Data.AddTodoItem(listNoteId: listId);
            Data.AddTodoItem(listNoteId: listId);

            // Act
            var result = Facade.GetNotes().Value;

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
            var result = Facade.DeleteNote(itemId);
            var items = Facade.GetNotes().Value;

            // Assert
            result.Value.Should().BeTrue();
            items.Count.Should().Be(1);
        }
    }
}
