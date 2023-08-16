using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_Management_Tool.Areas.Identity.Data;

namespace Project_Management_Tool.Data;

public class Project_Management_ToolDBContext : IdentityDbContext<ApplicationUser>
{
    public Project_Management_ToolDBContext(DbContextOptions<Project_Management_ToolDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
