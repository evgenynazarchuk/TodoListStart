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

namespace TodoListStart.Application.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TodoItemController : RestController<TodoItem, TodoItemValue>
    {
        private readonly IMapper _mapper;
        private readonly Repository _repo;
        public TodoItemController(Repository repo, IMapper mapper)
            :base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
    }
}
