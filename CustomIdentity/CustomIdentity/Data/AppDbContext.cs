using CustomIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomIdentity.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<GroupModel> Groups { get; set; }
    public DbSet<AssignmentModel> Assignments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<GroupModel>()
            .HasOne(p => p.Student)
            .WithMany()
            .HasForeignKey(p => p.StudentName)
            .HasPrincipalKey(u => u.Name)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder.Entity<GroupModel>()
            .HasOne(p => p.Teacher)
            .WithMany()
            .HasForeignKey(p => p.TeacherName)
            .HasPrincipalKey(u => u.Name)
            .OnDelete(DeleteBehavior.NoAction);
        
    }
}
