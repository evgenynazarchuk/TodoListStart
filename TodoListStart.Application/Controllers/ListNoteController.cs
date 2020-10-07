using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;
using AutoMapper;
using TodoListStart.Application.Interfaces;
using Microsoft.AspNet.OData;

namespace TodoListStart.Application.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ListNoteController : RestController<ListNote, ListNoteValue>
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repo;
        private readonly IValidationService _validator;
        public ListNoteController(
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
        public override async Task<IActionResult> Post([FromBody] ListNoteValue entitySource)
        {
            var errors = await _validator.ValidateListNote(entitySource, "POST");
            if (errors.Count > 0)
            {
                return BadRequest(errors); ;
            }
            return await base.Post(entitySource);
        }
        [HttpPut]
        public override async Task<IActionResult> Put([FromBody] ListNoteValue entitySource)
        {
            if (!await _repo.IsExist<ListNote>(entitySource.Id))
            {
                return NotFound(entitySource);
            }

            var errors = await _validator.ValidateListNote(entitySource, "PUT");
            if (errors.Count > 0)
            {
                return BadRequest(errors);
            }

            return await base.Put(entitySource);
        }
        [HttpGet("{id:int}/notes")]
        [EnableQuery]
        public async Task<IEnumerable<NoteValue>> GetNotesById(int id)
        {
            var notes = await _repo.GetNotesByListNoteId(id);
            var notesValue = _mapper.Map<IEnumerable<Note>, IEnumerable<NoteValue>>(notes);
            return notesValue;
        }
    }
}