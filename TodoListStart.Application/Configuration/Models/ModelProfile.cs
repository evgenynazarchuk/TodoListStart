using AutoMapper;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;

namespace TodoListStart.Application.Configuration.Models
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            CreateMap<TodoList, TodoListValue>();
            CreateMap<TodoListValue, TodoList>();

            CreateMap<TodoItem, TodoItemValue>();
            CreateMap<TodoItemValue, TodoItem>();
        }
    }
}
