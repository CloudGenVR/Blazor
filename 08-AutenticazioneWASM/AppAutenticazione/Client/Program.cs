using AppAutenticazione.Client;
using AppAutenticazione.Client.Authentications;
using AppAutenticazione.Client.Authentications.Abstractions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AppAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AppAuthenticationStateProvider>());
builder.Services.AddScoped<IAuthenticationService>(s => s.GetRequiredService<AppAuthenticationStateProvider>());
builder.Services.AddScoped<IHostEnvironmentAuthenticationStateProvider>(s => s.GetRequiredService<AppAuthenticationStateProvider>());

await builder.Build().RunAsync();
