using System;
using System.Collections.Generic;
using System.Text;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;

namespace TodoListStart.IntegrationTests.Support.Builder
{
    public class TodoListValueBuilder
    {
        private readonly TodoListValue _todoListValue;
        private TodoListValueBuilder()
        {
            _todoListValue = new TodoListValue();
        }
        private TodoListValueBuilder(TodoListValue todoList)
        {
            _todoListValue = todoList;
        }
        public static TodoListValueBuilder CreateDefaultBuilder()
        {
            var todoList = new TodoListValue()
            {
                Title = "Title",
                Description = "Description"
            };
            return new TodoListValueBuilder(todoList);
        }
        public TodoListValueBuilder Configure(Action<TodoListValue> config)
        {
            config(_todoListValue);
            return this;
        }
        public TodoListValue Build()
        {
            return _todoListValue;
        }
    }
}
