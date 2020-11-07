using Microsoft.EntityFrameworkCore;
using TodoListStart.Application.Models;
using TodoListStart.Application.Configuration.Models;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace TodoListStart.Application
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
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
            base.OnModelCreating(builder);
        }
    }
}
