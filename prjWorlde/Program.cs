
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.WebUtilities;

namespace prjWorlde
{
    public class Program
    {
        public static object WebAssemblyHostBuilder { get; private set; }

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddSingleton<Game>();
            builder.Services.AddSingleton<GameInput>();
            builder.Services.AddSingleton(
                sp =>
                {
                    var httpClient = sp.GetRequiredService<HttpClient>();
                    const string filePath = "word-list.txt";
                    return new AnswerProvide(httpClient, filePath);
                });
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}