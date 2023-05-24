using Microsoft.EntityFrameworkCore;

using Notes.API.Models.Entities;

namespace Notes.API.Data
{
    public class TableDbContext : DbContext
    {
        public TableDbContext(DbContextOptions<TableDbContext> options) : base(options)
        {
        }

        public DbSet<Event111> Event111s { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}

