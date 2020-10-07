using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoListStart.Application.Models;

namespace TodoListStart.Application.Configuration.Models
{
    public class ListNoteModelConfiguration : IEntityTypeConfiguration<ListNote>
    {
        public void Configure(EntityTypeBuilder<ListNote> builder)
        {
        }
    }
}
