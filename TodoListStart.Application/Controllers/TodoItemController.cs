using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;
using AutoMapper;
using TodoListStart.Application.Interfaces;

namespace TodoListStart.Application.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TodoItemController : RestController<TodoItem, TodoItemValue>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repo;
        private readonly IValidationService _validator;
        public TodoItemController(
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
        [HttpPost]
        public override async Task<IActionResult> Post([FromBody] TodoItemValue entitySource)
        {
            var errors = await _validator.ValidateTodoItem(entitySource, "POST");
            if (errors.Count > 0)
            {
                return BadRequest(errors);
            }
            return await base.Post(entitySource);
        }
        [HttpPut]
        public override async Task<IActionResult> Put([FromBody] TodoItemValue entitySource)
        {
            if (!await _repo.IsExist<TodoItem>(entitySource.Id))
            {
                return NotFound(entitySource);
            }

            var errors = await _validator.ValidateTodoItem(entitySource, "PUT");
            if (errors.Count > 0)
            {
                return BadRequest(errors);
            }

            return await base.Put(entitySource);
        }
    }
}
