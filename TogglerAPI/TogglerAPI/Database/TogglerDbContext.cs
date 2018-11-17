using Microsoft.EntityFrameworkCore;
using TogglerAPI.Interfaces;
using TogglerAPI.Models;

namespace TogglerAPI.Database
{
    public class TogglerDbContext : DbContext, ITogglerDbContext
    {
        public TogglerDbContext(DbContextOptions<TogglerDbContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Toggle> Toggles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ToggleRule> ToggleRules { get; set; }

        public void Test()
        {
            Roles.Add(new Role() { Name = "Name", Description = "Description" });
            SaveChanges();
        }
    }
}
