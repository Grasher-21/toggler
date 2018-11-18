using Microsoft.EntityFrameworkCore;

namespace TogglerAPI.Models
{
    public class TogglerContext : DbContext
    {
        public TogglerContext(DbContextOptions<TogglerContext> options) : base(options)
        {
        }

        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ToggleModel> Toggles { get; set; }
        public DbSet<ServiceModel> Services { get; set; }
        public DbSet<ToggleServicePermissionModel> ToggleServicePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<ToggleServicePermissionModel>().HasKey(t => new { t.ToggleId, t.ServiceId });
        }
    }
}
