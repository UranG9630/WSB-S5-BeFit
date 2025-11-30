using BeFit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BeFit.Models.Excercise> Excercise { get; set; } = default!;
        public DbSet<BeFit.Models.ExcerciseType> ExcerciseType { get; set; } = default!;
        public DbSet<BeFit.Models.Session> Session { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole() { Name = "Administrator", NormalizedName = "ADMIN" });
        }
    }
}
