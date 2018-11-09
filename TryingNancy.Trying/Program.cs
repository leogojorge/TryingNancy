using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace TryingNancy.Trying
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                        .UseKestrel()
                        .UseUrls("http://*:5000")
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseStartup<Startup>()
                        .Build();

            host.Run();
        }
    }
}
