using Application.Services;
using Application.Services.Impl;
using CV.Components;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<CvContext>(options => options
        .UseMySql(
            connectionString,
            new MariaDbServerVersion(new Version(10, 5, 23))));

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();

builder.Services.AddTransient<CvContext>();

builder.Services.AddTransient<IContentService, ContentService>();
builder.Services.AddTransient<IExperienceService, ExperienceService>();
builder.Services.AddTransient<IHobbyService, HobbyService>();
builder.Services.AddTransient<IReferenceService, ReferenceService>();
builder.Services.AddTransient<ISkillService, SkillService>();

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