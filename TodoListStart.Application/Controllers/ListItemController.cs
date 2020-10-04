using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoListStart.Application.Services;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;
using AutoMapper;
using TodoListStart.Application.Validations;

namespace TodoListStart.Application.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TodoItemController : RestController<TodoItem, TodoItemValue>
    {
        private readonly IMapper _mapper;
        private readonly Repository _repo;
        private readonly TodoItemValidationService _validator;
        public TodoItemController(
            Repository repo, 
            IMapper mapper,
            TodoItemValidationService validator)
            :base(repo, mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _validator = validator;
        }
        [HttpPost]
        public override async Task<IActionResult> Post([FromBody] TodoItemValue entitySource)
        {
            // base validator
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // extented validator
            var modelError = await _validator.Validate(entitySource);
            if (modelError.Errors.Count != 0)
            {
                return BadRequest(modelError);
            }
            return await base.Post(entitySource);
        }
        [HttpPut]
        public override async Task<IActionResult> Put([FromBody] TodoItemValue entitySource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modelError = await _validator.Validate(entitySource);
            if (modelError.Errors.Count != 0)
            {
                return BadRequest(modelError);
            }

            return await base.Put(entitySource);
        }
    }
}
