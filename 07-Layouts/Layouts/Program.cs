using Layouts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.Configure<LayoutConfig>(options => builder.Configuration.GetSection("LayoutConfig").Bind(options));
builder.Services.AddScoped<ILayoutSelector, LayoutSelector>();
builder.Services.AddSingleton<IStorage, Storage>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
