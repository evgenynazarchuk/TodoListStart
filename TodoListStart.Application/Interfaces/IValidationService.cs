﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListStart.Application.ValueObjects;

namespace TodoListStart.Application.Interfaces
{
    public interface IValidationService
    {
        Task<List<string>> ValidateListNote(ListNoteValue listNoteValue, string httpMethod);
        Task<List<string>> ValidateNote(NoteValue noteValue, string httpMethod);
        bool IsNull<TValue>(TValue entityValue);
        bool IsNullOrEmptyOrWhiteSpace(string entityField);
    }
}
