using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TodoListStart.IntegrationTests.Support;
using TodoListStart.IntegrationTests.Support.Builder;

namespace TodoListStart.IntegrationTests.Tests.ListNoteTests
{
    [TestClass]
    public class ListNoteTests : TestBase
    {
        [TestMethod]
        public void PostListNoteShouldBeCreateWitSpecifiedDate()
        {
            // Arange
            DateTime time1 = new DateTime(2019, 01, 01, 10, 01, 01);
            DateTime time2 = new DateTime(2020, 01, 01, 10, 01, 01);
            Date.SetCurrentDateTime(time1);
            var listNote = ListNoteValueBuilder
                .CreateDefaultBuilder()
                .Build();

            // Act
            Date.SetCurrentDateTime(time2);
            var todoList = Facade.PostListNote(listNote).Value;

            // Assert
            todoList.Title.Should().Be("Title");
            todoList.CreatedDate.Should().Be(time2);
            todoList.ModifiedDate.Should().BeNull();
        }
        [TestMethod]
        public void PutListNoteShouldBeUpdateWithSpecifiedDate()
        {
            // Arange
            DateTime time1 = new DateTime(2019, 01, 01, 10, 01, 01);
            DateTime time2 = new DateTime(2020, 01, 01, 10, 01, 01);
            string newTitle = "New Title";
            Date.SetCurrentDateTime(time1);
            var listNote = Data.AddListNote().Id;
            var listNoteValue = Facade.GetListNoteById(listNote).Value;
            listNoteValue.Title = newTitle;

            // Act
            Date.SetCurrentDateTime(time2);
            Facade.PutListNote(listNoteValue);

            // Assert
            var result = Facade.GetListNoteById(listNoteValue.Id).Value;
            result.Title.Should().Be(newTitle);
            result.CreatedDate.Should().Be(time1);
            result.ModifiedDate.Should().Be(time2);
        }
        [TestMethod]
        public void GetListNoteByIdShouldBeReturnListNoteById()
        {
            // Arange
            DateTime time1 = new DateTime(2019, 01, 01, 10, 01, 01);
            DateTime time2 = new DateTime(2020, 01, 01, 10, 01, 01);
            Date.SetCurrentDateTime(time1);
            var todoId = Data.AddListNote().Id;

            // Act
            Date.SetCurrentDateTime(time2);
            var todoList = Facade.GetListNoteById(todoId).Value;

            // Assert
            todoList.Title.Should().Be("Title");
            todoList.CreatedDate.Should().Be(time1);
            todoList.ModifiedDate.Should().BeNull();
        }
        [TestMethod]
        public void GetListNotesShouldBeReturnListNotes()
        {
            // Arange
            Data.AddListNote();
            Data.AddListNote();
            Data.AddListNote();

            // Act
            var listNotes = Facade.GetListNotes().Value;

            // Assert
            listNotes.Count.Should().Be(3);
        }
        [TestMethod]
        public void DeleteListNoteShouldBeDeleteListNote()
        {
            // Arange
            var listNoteId = Data.AddListNote().Id;

            // Act
            var result = Facade.DeleteListNote(listNoteId).Value;

            // Assert
            result.Should().BeTrue();
        }
        [TestMethod]
        public void DeleteListNoteShouldBeDeleteAllNotes()
        {
            // Arange
            var listNote1 = Data.AddListNote();
            var listNote2 = Data.AddListNote();
            Data.AddNote(listNoteId: listNote1.Id);
            Data.AddNote(listNoteId: listNote1.Id);
            Data.AddNote(listNoteId: listNote2.Id);
            Data.AddNote(listNoteId: listNote2.Id);
            Data.AddNote(listNoteId: listNote2.Id);

            // Act
            Facade.DeleteListNote(listNote1.Id);

            // Assert
            var items = Facade.GetNotes().Value;
            items.Count.Should().Be(3);
        }
    }
}
