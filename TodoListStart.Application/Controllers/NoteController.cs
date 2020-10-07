using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;
using AutoMapper;
using TodoListStart.Application.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNet.OData;

namespace TodoListStart.Application.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NoteController : RestController<Note, NoteValue>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repo;
        private readonly IValidationService _validator;
        public NoteController(
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
        public override async Task<IActionResult> Post([FromBody] NoteValue entitySource)
        {
            var errors = await _validator.ValidateNote(entitySource, "POST");
            if (errors.Count > 0)
            {
                return BadRequest(errors);
            }
            return await base.Post(entitySource);
        }
        [HttpPut]
        public override async Task<IActionResult> Put([FromBody] NoteValue entitySource)
        {
            if (!await _repo.IsExist<Note>(entitySource.Id))
            {
                return NotFound(entitySource);
            }

            var errors = await _validator.ValidateNote(entitySource, "PUT");
            if (errors.Count > 0)
            {
                return BadRequest(errors);
            }

            return await base.Put(entitySource);
        }
        [HttpGet("getallpublic")]
        //[EnableQuery]
        public async Task<List<NoteValue>> GetAllPublicNotes()
        {
            var publicNotes = await _repo.GetAllPublicNotes();
            var publicNotesValue = _mapper.Map<List<Note>, List<NoteValue>>(publicNotes);
            return publicNotesValue;
        }
    }
}
