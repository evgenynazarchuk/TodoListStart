using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoListStart.Application.Models;

namespace TodoListStart.Application.Configuration.Models
{
    public class TodoItemModelConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder
                .HasOne(i => i.TodoList)
                .WithMany(l => l.TodoItems)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
