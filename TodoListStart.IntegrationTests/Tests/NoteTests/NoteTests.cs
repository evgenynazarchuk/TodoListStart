using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TodoListStart.IntegrationTests.Support;
using TodoListStart.IntegrationTests.Support.Builder;

namespace TodoListStart.IntegrationTests.Tests.NoteTests
{
    /*[TestClass]
    public class NoteTests : TestBase
    {
        [TestMethod]
        public void PostNoteShouldBeCreateNoteWithSpecifiedDate()
        {
            // Arange
            var time1 = new DateTime(2020, 02, 03);
            var listNote = Data.AddListNote();
            var noteValue = NoteValueBuilder
                .CreateDefaultBuilder()
                .Configure(i => i.ListNoteId = listNote.Id)
                .Build();

            // Act
            Date.SetCurrentDateTime(time1);
            var result = Facade.PostNote(noteValue).Value;

            // Assert
            result.Text.Should().Be("Text");
            result.DueDate.Should().BeNull();
            result.IsCompleted.Should().BeFalse();
            result.IsPublic.Should().BeFalse();
            result.CreatedDate.Should().Be(time1);
            result.ModifiedDate.Should().BeNull();
        }
        [TestMethod]
        public void PostNoteShouldBeFullFilledNote()
        {
            // Arange
            var time1 = new DateTime(2020, 02, 03);
            var dueDate = DateTime.Today.Add(TimeSpan.FromDays(3));
            var listNote = Data.AddListNote();
            var noteValue = NoteValueBuilder.CreateDefaultBuilder().Build();
            noteValue.ListNoteId = listNote.Id;
            noteValue.DueDate = dueDate;
            noteValue.IsPublic = true;
            noteValue.IsCompleted = true;

            // Act
            Date.SetCurrentDateTime(time1);
            var result = Facade.PostNote(noteValue).Value;

            // Assert
            result.Text.Should().Be("Text");
            result.DueDate.Should().Be(dueDate);
            result.IsCompleted.Should().BeTrue();
            result.IsPublic.Should().BeTrue();
            result.CreatedDate.Should().Be(time1);
            result.ModifiedDate.Should().BeNull();
            result.DueDate.Should().Be(dueDate);
        }
        [TestMethod]
        public void PutNoteShouldBeUpdateNoteWithSpecifiedDate()
        {
            // Arange
            var time1 = new DateTime(2019, 02, 03);
            var time2 = new DateTime(2020, 02, 03);
            var dueDate = DateTime.Today.Add(TimeSpan.FromDays(3));
            Date.SetCurrentDateTime(time1);
            var noteId = Data.AddNote().Id;
            var newListNoteId = Data.AddListNote().Id;
            var note = Facade.GetNoteById(noteId).Value;
            note.IsCompleted = true;
            note.IsPublic = true;
            note.Text = "New Title";
            note.ListNoteId = newListNoteId;
            note.DueDate = dueDate;

            // Act
            Date.SetCurrentDateTime(time2);
            Facade.PutNote(note);

            // Assert
            var result = Facade.GetNoteById(noteId).Value;
            result.Text.Should().Be("New Title");
            result.IsCompleted.Should().BeTrue();
            result.IsPublic.Should().BeTrue();
            result.DueDate.Should().Be(dueDate);
            result.ListNoteId.Should().Be(newListNoteId);
            result.CreatedDate.Should().Be(time1);
            result.ModifiedDate.Should().Be(time2);
        }
        [TestMethod]
        public void PutNoteShouldBeUpdated()
        {
            // Arrange
            var note = Data.AddNote();
            note.Text = "updated text";

            // Act
            Facade.PutNote(note);

            // Assert
            var result = Facade.GetNoteById(note.Id).Value;
            result.Text.Should().Be("updated text");
        }
        [TestMethod]
        public void GetNotesShouldBeReturnNotes()
        {
            // Arange
            var listId = Data.AddListNote().Id;
            Data.AddNote(listNoteId: listId);
            Data.AddNote(listNoteId: listId);
            Data.AddNote(listNoteId: listId);

            // Act
            var result = Facade.GetNotes().Value;

            // Assert
            result.Count.Should().Be(3);
        }
        [TestMethod]
        public void DeleteNoteShouldBeDeleteNote()
        {
            // Arange
            Data.AddNote();
            var itemId = Data.AddNote().Id;

            // Act
            var result = Facade.DeleteNote(itemId);

            // Assert
            var items = Facade.GetNotes().Value;
            result.Value.Should().BeTrue();
            items.Count.Should().Be(1);
        }
        [TestMethod]
        public void GetAllPublicNotesShouldBeReturnPublicNotes()
        {
            // Arange
            var list1 = Data.AddListNote();
            var list2 = Data.AddListNote();
            var note1 = NoteValueBuilder.CreateDefaultBuilder().Configure(n => n.IsPublic = true).Build();
            var note2 = NoteValueBuilder.CreateDefaultBuilder().Configure(n => n.IsPublic = false).Build();
            var note3 = NoteValueBuilder.CreateDefaultBuilder().Configure(n => n.IsPublic = true).Build();
            var note4 = NoteValueBuilder.CreateDefaultBuilder().Configure(n => n.IsPublic = true).Build();
            var note5 = NoteValueBuilder.CreateDefaultBuilder().Configure(n => n.IsPublic = false).Build();
            Data.AddNote(note1, list1.Id);
            Data.AddNote(note2, list1.Id);
            Data.AddNote(note3, list1.Id);
            Data.AddNote(note4, list1.Id);
            Data.AddNote(note5, list1.Id);

            // Act
            var result = Facade.GetAllPublicNotes().Value;

            // Assert
            result.Count.Should().Be(3);
        }
    }*/
}
