using Emata.UI;
using Emata.UI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Retrieve the base URL from appsettings.json
string baseUrl = builder.Configuration.GetSection("ApiConfig")["BaseUrl"] ?? "https://localhost:7170/api/";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseUrl), Timeout = TimeSpan.FromMinutes(10) });

builder.Services.AddScoped<ISessionService, SessionService>();

await builder.Build().RunAsync();
