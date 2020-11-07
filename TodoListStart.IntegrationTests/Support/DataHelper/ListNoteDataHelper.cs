using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;
using TodoListStart.IntegrationTests.Support.Builder;
using System;
using TodoListStart.IntegrationTests.Support.Facade;
using TodoListStart.IntegrationTests.Support.Data;
using Microsoft.AspNetCore.TestHost;
using TodoListStart.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using TodoListStart.Application.ApplicationServices;
using TodoListStart.IntegrationTests.Support.Extensions;
using TodoListStart.Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace TodoListStart.IntegrationTests.Support.Data
{
    public partial class DataHelper
    {
        public ListNote AddListNote(ListNoteValue listNoteValue = null)
        {
            listNoteValue ??= ListNoteValueBuilder.Build();
            var listNote = _mapper.Map<ListNoteValue, ListNote>(listNoteValue);

            _repo = _services.GetRequiredService<IRepository>();
            _repo.Add(listNote).GetAwaiter().GetResult();
            return listNote;
        }
    }
}
