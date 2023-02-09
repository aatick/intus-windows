global using IntusWindows.Shared;
global using static IntusWindows.Shared.Helper;
using IntusWindows.Client;
using IntusWindows.BLL.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IClientOrderService, ClientOrderService>();
builder.Services.AddScoped<IClientWindowService, ClientWindowService>();
builder.Services.AddScoped<IClientSubElementService, ClientSubElementService>();
await builder.Build().RunAsync();
