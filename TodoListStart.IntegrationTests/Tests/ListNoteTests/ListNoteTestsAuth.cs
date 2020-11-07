using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TodoListStart.IntegrationTests.Support;
using TodoListStart.IntegrationTests.Support.Builder;
using TodoListStart.Application.Models.Auth;
using TodoListStart.IntegrationTests.Support.Constants;

namespace TodoListStart.IntegrationTests.Tests.ListNoteTests
{
    [TestClass]
    public class ListNotesWithAuth : TestBase
    {
        [TestMethod]
        public void AddListNoteWithAuth()
        {
            // Arange
            var time = new DateTime(2020, 1, 15);
            this.Date.SetCurrentDateTime(time);
            var listNote = ListNoteValueBuilder.Build();
            var newUser = RegistrationUserBuilder.Build();

            // Act
            Facade.Registration(newUser);
            Facade.SignOut();
            Facade.SignIn(new UserSignIn { Email = "default@default.com", Password = "password" });
            var result = Facade.PostListNote(listNote).Value;

            // Assert
            result.Title.Should().Be(DefaultValues.ListNoteTitle);
            result.CreatedBy.Should().Be("default@default.com");
            result.CreatedDate.Should().Be(time);
            result.ModifiedBy.Should().BeNull();
            result.ModifiedDate.Should().BeNull();
        }
        [TestMethod]
        public void AddNoteWithAuth()
        {
            // Arange
            var time = new DateTime(2020, 1, 15);
            var newUser = RegistrationUserBuilder.Build();
            var listNote = ListNoteValueBuilder.Build();
            var note = NoteValueBuilder.Build();
            var signin = new UserSignIn { Email = "default@default.com", Password = "password" };
            this.Date.SetCurrentDateTime(time);
            Auth.Registration(newUser);
            Auth.SetUser(signin.Email);
            var listNoteId = Data.AddListNote(listNote).Id;
            note.ListNoteId = listNoteId;

            // Act
            Facade.SignIn(signin);
            var result = Facade.PostNote(note).Value;

            // Assert
            result.CreatedBy.Should().Be("default@default.com");
        }
        [TestMethod]
        public void GetNotesShouldBeReturnOnlySelfNotes()
        {
            // Arange
            var user1 = RegistrationUserBuilder.Build();
            var user2 = RegistrationUserBuilder.Build();
            user1.Email = "default1@default.com";
            user2.Email = "default2@default.com";
            Auth.Registration(user1);
            Auth.Registration(user2);

            var listNote1 = ListNoteValueBuilder.Build();
            var listNote2 = ListNoteValueBuilder.Build();
            var note1 = NoteValueBuilder.Build();
            var note2 = NoteValueBuilder.Build();
            var note3 = NoteValueBuilder.Build();
            var note4 = NoteValueBuilder.Build();
            var note5 = NoteValueBuilder.Build();

            Auth.SetUser(user1.Email);
            var listNoteId1 = Data.AddListNote(listNote1).Id;
            note1.ListNoteId = note2.ListNoteId = note3.ListNoteId = listNoteId1;
            Data.AddNote(note1);
            Data.AddNote(note2);
            Data.AddNote(note3);

            Auth.SetUser(user2.Email);
            var listNoteId2 = Data.AddListNote(listNote2).Id;
            note4.ListNoteId = note5.ListNoteId = listNoteId2;
            Data.AddNote(note4);
            Data.AddNote(note5);

            // Act
            Facade.SignIn(new UserSignIn { Email = user1.Email, Password = "password" });
            var result = Facade.GetNotes().Value;

            // Assert
            result.Count.Should().Be(3);
        }
    }
}
