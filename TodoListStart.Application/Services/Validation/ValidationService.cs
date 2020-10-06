using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListStart.Application.ApplicationServices;
using TodoListStart.Application.ValueObjects;
using TodoListStart.Application.Models;
using Microsoft.EntityFrameworkCore;
using TodoListStart.Application.Constants;
using TodoListStart.Application.Controllers;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.Application.Services.Validation
{
    public partial class ValidationService : IValidationService
    {
        private readonly IRepository _repo;
        public ValidationService(IRepository repo)
        {
            _repo = repo;
        }
        public bool IsNull<TValueObj>(TValueObj entity)
        {
            return entity == null ? true : false;
        }
        public bool IsNullOrEmptyOrWhiteSpace(string field)
        {
            return string.IsNullOrEmpty(field) || string.IsNullOrWhiteSpace(field);
        }
    }
}
