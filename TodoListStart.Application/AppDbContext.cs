using Microsoft.EntityFrameworkCore;
using TodoListStart.Application.Models;
using TodoListStart.Application.Configuration.Models;
using System;

namespace TodoListStart.Application
{
    public class AppDbContext : DbContext
    {
        public DbSet<ListNote> ListNotes { get; set; }
        public DbSet<Note> Notes { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ListNoteModelConfiguration());
            builder.ApplyConfiguration(new NoteModelConfiguration());
        }
    }
}
