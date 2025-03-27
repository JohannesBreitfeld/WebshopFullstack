using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using Webshop.Presentation.Auth;
using Webshop.Presentation.Components;
using Webshop.Presentation.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddMudServices();
builder.Services.AddScoped<CartService>();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]);
});

// Register CustomAuthStateProvider as AuthenticationStateProvider
builder.Services.AddScoped<CustomAuthStateProvider>();

// Add authorization support
builder.Services.AddAuthorizationCore();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
