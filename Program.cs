using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Paslauga.Components;
using Paslauga.Data;
using Paslauga.Helpers;
using System.Text.Json;

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
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<ApiService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5200");
});

var app = builder.Build();

app.UseFastEndpoints();
app.UseSwaggerGen();
app.UsePathBase("/dashboard");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
