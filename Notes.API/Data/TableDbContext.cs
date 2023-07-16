using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Notes.API.Models.Entities;

namespace Notes.API.Data
{
    public class TableDbContext : IdentityDbContext<User, Role, Guid>
    {
        public TableDbContext(DbContextOptions<TableDbContext> options) : base(options)
        {
        }
        public DbSet<Event111> Event111s { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }


    }
}

