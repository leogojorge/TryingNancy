using Nancy;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace TryingNancy.Trying
{
    class Program
    {
        static void Main(string[] args)
        {

            var server = new Nancy.Hosting.Self.NancyHost(new Uri("http://localhost:8282"));


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
