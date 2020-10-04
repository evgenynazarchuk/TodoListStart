using System;
using System.Collections.Generic;
using System.Text;
using TodoListStart.Application.Models;
using TodoListStart.Application.ValueObjects;

namespace TodoListStart.IntegrationTests.Support.Builder
{
    public class TodoItemValueBuilder
    {
        private readonly TodoItemValue _todoItemValue;
        private TodoItemValueBuilder()
        {
            _todoItemValue = new TodoItem();
        }
        private TodoItemValueBuilder(TodoItemValue todoList)
        {
            _todoItemValue = todoList;
        }
        public static TodoItemValueBuilder CreateDefaultBuilder()
        {
            var todoList = new TodoItemValue()
            {
                Title = "Title",
                Body = "Body",
                TodoListId = 1,
                IsCompleted = false,
                DueDate = new DateTime().AddMonths(1)
            };
            return new TodoItemValueBuilder(todoList);
        }
        public TodoItemValueBuilder Configure(Action<TodoItemValue> config)
        {
            config(_todoItemValue);
            return this;
        }
        public TodoItemValue Build()
        {
            return _todoItemValue;
        }
    }
}
