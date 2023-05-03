using FinanceAssemblyApp;
using FinanceAssemblyApp.Data;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// register AntDesign
builder.Services.AddAntDesign();
builder.Services.AddTransient<HttpClient>();
builder.Services.AddDbContext<ProductDbContext>(options =>
{
    options.UseSqlite("Data Source = Products.db");
});
builder.Services.AddScoped<ProductServices>();
await builder.Build().RunAsync();
