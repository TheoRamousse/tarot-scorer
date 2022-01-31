using Blazored.LocalStorage;
using Blazored.Modal;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIGateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddBlazorise(options =>
           {
               options.ChangeTextOnKeyPress = true;
           })
            .AddBootstrapProviders()
            .AddFontAwesomeIcons();
            
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddBlazoredModal();

            await builder.Build().RunAsync();
        }
    }
}
