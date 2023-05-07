using FinanceWasmApp.Client;
using FinanceWasmApp.Data;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//import AntDesign
builder.Services.AddAntDesign();
builder.Services.AddSingleton<BillDBService>();
builder.Services.AddSingleton<BillTypeDBService>();

await builder.Build().RunAsync();
