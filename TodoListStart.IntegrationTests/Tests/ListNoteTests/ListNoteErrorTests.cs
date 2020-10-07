using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TodoListStart.IntegrationTests.Support;
using TodoListStart.IntegrationTests.Support.Builder;
using TodoListStart.Application.Constants;
using System;

namespace TodoListStart.IntegrationTests.Tests.TodoList
{
    [TestClass]
    public class ListNoteErrorTests : TestBase
    {
        [TestMethod]
        public void AddIncorrectListNoteWithEmptyTitle()
        {
            // Arange
            var noteList = ListNoteValueBuilder
                .CreateDefaultBuilder()
                .Build();
            noteList.Title = "";

            // Act
            var result = Facade.PostListNote(noteList).Errors;

            // Assert
            result.Should().BeEquivalentTo(new List<string>() { ErrorMessages.ListNoteEmpty });
        }
        [TestMethod]
        public void AddIncorrectListNoteWithMoreTitleThan144()
        {
            // Arange
            var noteList = ListNoteValueBuilder
                .CreateDefaultBuilder()
                .Build();
            noteList.Title = "01234567890123456789012345678901234567890123456789012345678901234\\" +
                "5678901234567890123456789012345678901234567890123456789012345678901234567890123456789";

            // Act
            var result = Facade.PostListNote(noteList).Errors;

            // Assert
            result.Should().BeEquivalentTo(new List<string>() { ErrorMessages.ListNoteTitleIncorrectLenght });
        }
        [TestMethod]
        public void AddIncorrectListNoteWithExistTitle()
        {
            // Arange
            Data.AddListNote();
            var noteList = ListNoteValueBuilder
                .CreateDefaultBuilder()
                .Build();

            // Act
            var result = Facade.PostListNote(noteList).Errors;

            // Assert
            result.Should().BeEquivalentTo(new List<string>() { ErrorMessages.ListNoteTitleNotUnique });
        }
        [TestMethod]
        public void UpdateInCorrectListNoteWithEmptyTitle()
        {
            // Arange
            var listNoteId = Data.AddListNote().Id;
            var listNoteValue = Facade.GetListNoteById(listNoteId).Value;
            listNoteValue.Title = "";

            // Act
            var result = Facade.PutListNote(listNoteValue).Errors;

            // Assert
            result.Should().BeEquivalentTo(new List<string>() { ErrorMessages.ListNoteEmpty });
        }
        [TestMethod]
        public void UpdateNotExistListNote()
        {
            // Arange
            var listNodeValue = ListNoteValueBuilder.CreateDefaultBuilder().Build();
            listNodeValue.Id = 1;

            // Act
            var result = Facade.PutListNote(listNodeValue);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { ErrorMessages.NotFound });
        }
        [TestMethod]
        public void DeleteNotExistListNote()
        {
            // Arange

            // Act
            var result = Facade.DeleteListNote(1);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { ErrorMessages.NotFound });
        }
        [TestMethod]
        public void GetNotExistListNote()
        {
            // Arange
            // Act
            var result = Facade.GetListNoteById(100);

            // Assert
            result.Errors.Should().BeEquivalentTo(new List<string>() { ErrorMessages.NotFound });
        }
    }
}
