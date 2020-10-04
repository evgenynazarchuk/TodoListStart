using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using TodoListStart.Application.Services;
using TodoListStart.Application.Interfaces;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace TodoListStart.Application.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TodoListController : RestController<TodoList, TodoListValue>
    {
        private readonly IMapper _mapper;
        private readonly Repository _repo;
        public TodoListController(Repository repo, IMapper mapper)
            :base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet("{id}/items")]
        public async Task<IEnumerable<TodoItemValue>> GetItemsById(int id)
        {
            var items = await _repo.GetTodoItemsByTodoListId(id);
            var itemsValue = _mapper.Map<IEnumerable<TodoItem>, IEnumerable<TodoItemValue>>(items);
            return itemsValue;
        }
    }
}
