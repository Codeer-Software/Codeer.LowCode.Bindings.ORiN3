using ApexCharts;
using Codeer.LowCode.Bindings.ApexCharts.Designs;
using Codeer.LowCode.Bindings.ORiN3.Fields;
using Codeer.LowCode.Blazor.RequestInterfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ORiN3App.Client;
using ORiN3App.Client.Shared;
using ORiN3App.Client.Shared.Services;

typeof(ApexChartFieldDesign).ToString();
typeof(SeriesType).ToString();
typeof(ORiN3MonitorField).ToString();

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.RootComponents.Add<AfterBodyOutlet>("body::after");

builder.Services.AddSharedServices();
builder.Services.AddScoped<INavigationService, NavigationService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

using (var client = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
{
    await client.PostAsync("api/license/update_license", new StringContent(""));
}

await builder.Build().RunAsync();
