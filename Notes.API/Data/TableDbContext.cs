using Microsoft.EntityFrameworkCore;

using Notes.API.Models.Entities;

namespace Notes.API.Data
{
    public class TableDbContext : DbContext
    {
        public TableDbContext(DbContextOptions<TableDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.Role)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.User)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.UserId);
        }
        public DbSet<Event111> Event111s { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> Users_Roles { get; set; }
    }
}

