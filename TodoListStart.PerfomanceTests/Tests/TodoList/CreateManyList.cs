using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using TodoListStart.Application.ValueObjects;
using TodoListStart.IntegrationTests;
using TodoListStart.IntegrationTests.Support;
using TodoListStart.IntegrationTests.Support.Builder;

namespace TodoListStart.PerfomanceTests.Tests.TodoList
{
    public class CreateManyList : TestBase
    {
        public TodoListValue todoList = TodoListValueBuilder.CreateDefaultBuilder().Build();
        [Benchmark]
        public void Create()
        {
            // Assert

            // Act
            todoList.Title = new Random().Next(10000).ToString();
            Facade.PostTodoList(todoList);

            // Assert
        }
    }
}
