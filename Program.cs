using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Paslauga.Components;
using Paslauga.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CloudDbContext>(options =>
{
    options.UseSqlite("Data Source=cloud.db");
});
builder.Services.AddSwaggerDocument();
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<CloudDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddFastEndpoints();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

app.UseFastEndpoints();
app.UseSwaggerGen();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
