using GigTracker.Frontend;
using GigTracker.Frontend.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7230") });

builder.Services.AddScoped<GigService>();
builder.Services.AddScoped<BandService>();
builder.Services.AddScoped<BandMemberService>();
builder.Services.AddScoped<DialogService>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
