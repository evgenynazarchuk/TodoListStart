using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;

namespace TodoListStart.Application.Interfaces
{
    public interface IRepository
    {
        IQueryable<TModel> Read<TModel>()
            where TModel : class, IEntityIdentity, new();
        Task<TModel> Add<TModel>(TModel entity)
            where TModel : class, IEntityIdentity, new();
        Task<TModel> Find<TModel>(int id)
            where TModel : class, IEntityIdentity, new();
        Task Update<TModel>(TModel entity)
            where TModel : class, IEntityIdentity, new();
        Task Remove<TModel>(TModel entity)
            where TModel : class, new();
        Task<IEnumerable<TodoItem>> GetTodoItemsByTodoListId(int id);
        Task<bool> IsExist<TModel>(int id)
            where TModel : class, IEntityIdentity, new();
        Task<bool> IsExistItemTitleInList(TodoItemValue todoItem);
        Task<bool> IsExistListTitle(TodoListValue todoList);
    }
}
