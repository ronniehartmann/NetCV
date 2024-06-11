using Application.Authentication;
using Application.Authentication.Stores;
using Application.Dtos;
using Application.Services.Contents;
using Application.Services.Contents.Impl;
using Application.Services.Pdf;
using Application.Services.Pdf.Impl;
using Application.Services.Resources;
using Application.Services.Resources.Impl;
using CV.Components;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddLog4Net(builder.Configuration.GetRequiredSection("Log4NetCore:Log4NetConfigFileName").Value);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<LockoutService>();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<CvContext>(options =>
    options.UseMySql(connectionString, new MariaDbServerVersion(new Version(10, 5, 23))));

builder.Services.Configure<AdminUserConfig>(builder.Configuration.GetSection("Administrator"));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        options.Lockout.MaxFailedAccessAttempts = 3;
        options.Lockout.AllowedForNewUsers = true;
    })
    .AddUserStore<MonoConfigurationUserStore>()
    .AddRoleStore<DisabledRoleStore>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
});

builder.Services.AddRadzenComponents();

builder.Services.AddTransient<IContentRepository, ContentRepository>();
builder.Services.AddTransient<IEducationRepository, EducationRepository>();
builder.Services.AddTransient<IExperienceRepository, ExperienceRepository>();
builder.Services.AddTransient<IHobbyRepository, HobbyRepository>();
builder.Services.AddTransient<IReferenceRepository, ReferenceRepository>();
builder.Services.AddTransient<ISkillRepository, SkillRepository>();

builder.Services.AddTransient<IPdfService, QuestPdfService>();
builder.Services.AddTransient<IContentService, ContentService>();
builder.Services.AddTransient<ResourceService<EducationDto>, EducationService>();
builder.Services.AddTransient<ResourceService<ExperienceDto>, ExperienceService>();
builder.Services.AddTransient<ResourceService<HobbyDto>, HobbyService>();
builder.Services.AddTransient<ResourceService<ReferenceDto>, ReferenceService>();
builder.Services.AddTransient<ResourceService<SkillDto>, SkillService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();