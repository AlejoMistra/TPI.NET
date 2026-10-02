using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazor.WebAssembly;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configurar HttpClient para llamadas a la API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7146/") });

var app = builder.Build();

await app.RunAsync();
