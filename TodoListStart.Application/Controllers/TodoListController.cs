using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoListStart.Application.ApplicationServices;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;
using AutoMapper;
using TodoListStart.Application.Interfaces;
using TodoListStart.Application.Constants;

namespace TodoListStart.Application.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TodoListController : RestController<TodoList, TodoListValue>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repo;
        private readonly IValidationService _validator;
        public TodoListController(
            IRepository repo, 
            IMapper mapper,
            IValidationService validator
            )
            : base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _validator = validator;
        }
        [HttpGet("{id}/items")]
        public virtual async Task<IEnumerable<TodoItemValue>> GetItemsById(int id)
        {
            var items = await _repo.GetTodoItemsByTodoListId(id);
            var itemsValue = _mapper.Map<IEnumerable<TodoItem>, IEnumerable<TodoItemValue>>(items);
            return itemsValue;
        }
        [HttpPost]
        public override async Task<IActionResult> Post([FromBody] TodoListValue entitySource)
        {
            var errors = await _validator.ValidateTodoList(entitySource);
            if (errors.Count > 0)
            {
                return BadRequest(errors); ;
            }
            return await base.Post(entitySource);
        }
        [HttpPut]
        public override async Task<IActionResult> Put([FromBody] TodoListValue entitySource)
        {
            if (!await _repo.IsExist<TodoList>(entitySource.Id))
            {
                return NotFound(entitySource);
            }

            var errors = await _validator.ValidateTodoList(entitySource);
            if (errors.Count > 0)
            {
                return BadRequest(errors);
            }

            return await base.Put(entitySource);
        }
    }
}
