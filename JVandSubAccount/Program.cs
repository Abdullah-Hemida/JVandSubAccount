using JVandSubAccount.Data;
using JVandSubAccount.ServicesLayer;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using JVandSubAccount.ServicesLayer.IServices;
using JVandSubAccount.ServicesLayer.Services;
using JVandSubAccount.Data.Repositories;
using JVandSubAccount.Data.IRepositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IJVRepository, JVRepository>();
builder.Services.AddScoped<IJVService, JVService>();

builder.Services.AddScoped<ISubAccountRepository, SubAccountRepository>();
builder.Services.AddScoped<ISubAccountService, SubAccountService>();
// Add HttpClient with proper base address
builder.Services.AddHttpClient<LocalizationService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7260");
});

// Add localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddScoped<LocalizationService>();

// Configure supported cultures
var supportedCultures = new[] { "en", "ar" };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.SetDefaultCulture(supportedCultures[0])
           .AddSupportedCultures(supportedCultures)
           .AddSupportedUICultures(supportedCultures);
});
var culture = CultureInfo.CurrentCulture; // fallback

// Try to get culture from cookie
var requestLocalizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("en")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

var app = builder.Build();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/jv");
        return;
    }

    await next();
});

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseRequestLocalization(requestLocalizationOptions);

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();