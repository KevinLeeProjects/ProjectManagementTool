using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_Management_Tool.Data;
using Project_Management_Tool.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Project_Management_ToolDBContextConnection") ?? throw new InvalidOperationException("Connection string 'Project_Management_ToolDBContextConnection' not found.");

builder.Services.AddDbContext<Project_Management_ToolDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<Project_Management_ToolDBContext>();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;



app.UseAuthorization();

app.MapRazorPages();

app.Run();
