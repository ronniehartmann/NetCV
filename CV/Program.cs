using Application.Authentication;
using Application.Authentication.Stores;
using Application.Services;
using Application.Services.Impl;
using CV.Components;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddLog4Net("log4net.config");

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<LockoutService>();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CvContext>(options =>
    options.UseMySql(connectionString, new MariaDbServerVersion(new Version(10, 5, 23))));

builder.Services.Configure<AdminUserConfig>(builder.Configuration.GetSection("Administrator"));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        options.Lockout.MaxFailedAccessAttempts = 3;
        options.Lockout.AllowedForNewUsers = true;
    })
    .AddUserStore<ConfigurationStore>()
    .AddRoleStore<DummyRoleStore>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
});

builder.Services.AddRadzenComponents();

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