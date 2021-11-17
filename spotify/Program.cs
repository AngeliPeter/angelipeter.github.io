using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using spotify.Data;
using spotify.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Syncfusion.Blazor;
using System.Threading.Tasks;

namespace spotify
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTMzOTc1QDMxMzkyZTMzMmUzMGRxK2RwSDVxY0Y3ZGlFeTZuU1kxVmk3K0pUUVpSaG9ka1ZLZjJrZVBkU1E9");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped<ISpotify, Spotify>();
            builder.Services
                .AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            builder.Services.AddBlazoredLocalStorage().AddScoped<Spotify>();
            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddScoped<SpotifyDb>();

            await builder.Build().RunAsync();
        }
    }
}
