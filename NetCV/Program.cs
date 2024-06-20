using Application.Dtos;
using Application.Services.Contents;
using Application.Services.Contents.Impl;
using Application.Services.Pdf;
using Application.Services.Pdf.Impl;
using Application.Services.Resources;
using Application.Services.Resources.Impl;
using Application.Services.Settings;
using Application.Services.Settings.Impl;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Identity;
using Infrastructure.Identity.Stores;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCV.Components;
using NetCV.Services;
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
builder.Services.AddTransient<ISettingsRepository, SettingsRepository>();
builder.Services.AddTransient<ISkillRepository, SkillRepository>();
builder.Services.AddTransient<FileUploadService>();

builder.Services.AddTransient<IPdfService, QuestPdfService>();
builder.Services.AddTransient<IContentService, ContentService>();
builder.Services.AddTransient<ISettingsService, SettingsService>();
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

    var context = app.Services.GetRequiredService<CvContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();