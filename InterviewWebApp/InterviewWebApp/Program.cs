using InterviewWebApp.Client.Pages;
using InterviewWebApp.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<HttpClient>(s =>
{
    var uriHelper = s.GetRequiredService<NavigationManager>();
    return new HttpClient
    {
        BaseAddress = new Uri(uriHelper.BaseUri)
    };
});

builder.Services.AddIdentity<CustomUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
    })
        .AddDefaultUI()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddTokenProvider<DataProtectorTokenProvider<CustomUser>>(TokenOptions.DefaultProvider);

builder.Services.AddRazorPages();
builder.Services.AddScoped<ICalendarService, CalendarService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorPages();
app.MapFallbackToPage("/calendar/book/{code}", "/_Host");
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

app.MapControllers();

app.Run();
