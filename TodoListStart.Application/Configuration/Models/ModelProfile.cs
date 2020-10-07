using AutoMapper;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;

namespace TodoListStart.Application.Configuration.Models
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            CreateMap<ListNote, ListNoteValue>();
            CreateMap<ListNoteValue, ListNote>();

            CreateMap<Note, NoteValue>();
            CreateMap<NoteValue, Note>();
        }
    }
}
