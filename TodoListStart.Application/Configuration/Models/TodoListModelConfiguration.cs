using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoListStart.Application.Models;

namespace TodoListStart.Application.Configuration.Models
{
    public class TodoListModelConfiguration : IEntityTypeConfiguration<TodoList>
    {
        public void Configure(EntityTypeBuilder<TodoList> builder)
        {
        }
    }
}
