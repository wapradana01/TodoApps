using Microsoft.EntityFrameworkCore;
using Todo.Api.DataAccess.Bases;
using Todo.Api.DataAccess.Builders;
using Todo.Api.DataAccess.Models;
using Todo.Api.Shared.Objects;

namespace Todo.Api.DataAccess
{
    public class ApplicationDbContext(DbContextOptions options, CurrentUserAccessor currentUserAccessor) : DbContextBase(options, currentUserAccessor)
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<TodoActivity> TodoActivities { get; set; }
        public virtual DbSet<DocNumberConfig> DocNumberConfigs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserEntityBuilder().Configure(modelBuilder.Entity<User>());
            new TodoActivityEntityBuilder().Configure(modelBuilder.Entity<TodoActivity>());
            new DocNumberConfigEntityBuilder().Configure(modelBuilder.Entity<DocNumberConfig>());
        }
    }
}
