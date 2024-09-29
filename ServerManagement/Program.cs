using Microsoft.EntityFrameworkCore;
using ServerManagement.Components;
using ServerManagement.Data;
using ServerManagement.Models;
using ServerManagement.StateStore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextFactory<ServerManagementContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ServerManagement"));
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//builder.Services.AddTransient<SessionStorage>();
builder.Services.AddScoped<ContainerStorage>();
builder.Services.AddScoped<TorontoOnlineServersStore>();
builder.Services.AddTransient<IServersEFCoreRepository, ServersEFCoreRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
