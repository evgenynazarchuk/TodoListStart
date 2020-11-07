using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TodoListStart.IntegrationTests.Support;
using TodoListStart.IntegrationTests.Support.Builder;
using TodoListStart.Application.Constants;
using FluentAssertions;
using System;

namespace TodoListStart.IntegrationTests.Tests.NoteTests
{
    /*[TestClass]
    public class NoteErrorTests : TestBase
    {
        [TestMethod]
        public void PutNoteShouldBeReturnManyErrors()
        {
            // Arange
            var itemId = Data.AddNote().Id;
            var itemValue = Facade.GetNoteById(itemId).Value;
            itemValue.ListNoteId = 2;
            itemValue.Text = "";
            itemValue.DueDate = DateTime.Today - TimeSpan.FromDays(1);

            // Act
            var result = Facade.PutNote(itemValue);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>()
            {
                ErrorMessages.ListNoteNotExist,
                ErrorMessages.NoteEmpty,
                ErrorMessages.NoteDueDateLessCurrentDate
            });
        }
        [TestMethod]
        public void PostNoteShouldBeReturnNoteEmptyError()
        {
            // Arange
            var list = Data.AddListNote();
            var noteValue = NoteValueBuilder
                .CreateDefaultBuilder()
                .Configure(i =>
                {
                    i.Text = "";
                    i.ListNoteId = list.Id;
                })
                .Build();

            // Act
            var result = Facade.PostNote(noteValue);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string> { ErrorMessages.NoteEmpty });
        }
        [TestMethod]
        public void PostNoteShouldBeReturnNoteDueDateLessCurrentDateError()
        {
            // Arange
            var list = Data.AddListNote();
            var noteValue = NoteValueBuilder
                .CreateDefaultBuilder()
                .Configure(i =>
                {
                    i.ListNoteId = list.Id;
                    i.DueDate = DateTime.Today - TimeSpan.FromDays(1);
                })
                .Build();

            // Act
            var result = Facade.PostNote(noteValue);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string> { ErrorMessages.NoteDueDateLessCurrentDate });
        }
        [TestMethod]
        public void PostNoteShouldBeReturnNoteTextIncorrectLenghtError()
        {
            // Arange
            var list = Data.AddListNote();
            var noteValue = NoteValueBuilder
                .CreateDefaultBuilder()
                .Configure(i =>
                {
                    i.Text = "01234567890123456789012345678901234567890123456789012345678901234567890123456789\\" +
                    "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789\\" +
                    "01234567890123456789";
                    i.ListNoteId = list.Id;
                })
                .Build();

            // Act
            var result = Facade.PostNote(noteValue);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string> { ErrorMessages.NoteTextIncorrectLenght });
        }
        [TestMethod]
        public void PostNoteShouldBeReturnNoteNotUniqueError()
        {
            // Arange
            var listId = Data.AddListNote().Id;
            Data.AddNote(listNoteId: listId);
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
        public void PostNoteShouldBeReturnListNoteNotExistError()
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
        public void PutNoteShouldBeReturnNoteEmptyError()
        {
            // Assert
            var itemId = Data.AddNote().Id;
            var item = Facade.GetNoteById(itemId).Value;
            item.Text = "";

            // Act
            var result = Facade.PutNote(item);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string> { ErrorMessages.NoteEmpty });
        }
        [TestMethod]
        public void PutNoteShouldBeReturnNoteTextIncorrectLenghtError()
        {
            // Assert
            var itemId = Data.AddNote().Id;
            var item = Facade.GetNoteById(itemId).Value;
            item.Text = "01234567890123456789012345678901234567890123456789012345678901234567890123456789\\" +
                    "0123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789\\" +
                    "01234567890123456789";

            // Act
            var result = Facade.PutNote(item);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string> { ErrorMessages.NoteTextIncorrectLenght });
        }
        [TestMethod]
        public void PutNoteShouldBeReturnNoteDueDateLessCurrentDateError()
        {
            // Assert
            var itemId = Data.AddNote().Id;
            var item = Facade.GetNoteById(itemId).Value;
            item.DueDate = DateTime.Today - TimeSpan.FromDays(1);

            // Act
            var result = Facade.PutNote(item);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string> { ErrorMessages.NoteDueDateLessCurrentDate });
        }
        [TestMethod]
        public void PutNoteShouldBeReturnNotFoundError()
        {
            // Assert
            var noteValue = NoteValueBuilder.CreateDefaultBuilder().Build();
            noteValue.ListNoteId = 1;

            // Act
            var result = Facade.PutNote(noteValue);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { ErrorMessages.NotFound });
        }
        [TestMethod]
        public void PutNoteShouldBeReturnNoteNotUniqueError()
        {
            // Arange
            var listId = Data.AddListNote().Id;
            Data.AddNote(listNoteId: listId);
            var item = NoteValueBuilder
                .CreateDefaultBuilder()
                .Configure(i => i.Text = "New Text")
                .Build();
            var itemId = Data.AddNote(item, listNoteId: listId).Id;
            var itemValue = Facade.GetNoteById(itemId).Value;
            itemValue.Text = "Text";

            // Act
            var result = Facade.PutNote(itemValue);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { ErrorMessages.NoteNotUnique });
        }
        [TestMethod]
        public void PutNoteShouldBeReturnListNoteNotExistError()
        {
            // Arange
            var itemId = Data.AddNote().Id;
            var itemValue = Facade.GetNoteById(itemId).Value;
            itemValue.ListNoteId = 2;

            // Act
            var result = Facade.PutNote(itemValue);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { ErrorMessages.ListNoteNotExist });
        }
        [TestMethod]
        public void GetNoteByIdShouldBeReturnNotFoundError()
        {
            // Assert

            // Act
            var result = Facade.GetNoteById(1);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { ErrorMessages.NotFound });
        }
        [TestMethod]
        public void DeleteNoteShouldBeReturnNotFoundError()
        {
            // Assert

            // Act
            var result = Facade.DeleteNote(1);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { ErrorMessages.NotFound });
        }
    }*/
}
