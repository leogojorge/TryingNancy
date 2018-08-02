using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.Diagnostics;
using Nancy.TinyIoc;
using System.Diagnostics;
using System.Threading;

namespace TryingNancy.Trying
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            this.AddStopwatch(pipelines);
        }

        public override void Configure(INancyEnvironment environment)
        {
            environment.Diagnostics(password: "123");
            base.Configure(environment);
        }
        private void AddStopwatch(IPipelines pipelines)
        {
            pipelines.BeforeRequest += context =>
            {
                if (context.Request.Headers.Authorization != "")
                    return null;
                
                Stopwatch stopwatch = Stopwatch.StartNew();
                context.Items.Add("Stopwatch", stopwatch);
                return null;
            };

            pipelines.OnError += (context, ex) =>
            {
                return null;
            };

            pipelines.AfterRequest.AddItemToEndOfPipeline(context =>
            {
                object Objstopwatch;
                context.Items.TryGetValue("Stopwatch", out Objstopwatch);
                if (Objstopwatch != null)
                {
                    Stopwatch stopwatch = (Stopwatch)Objstopwatch;
                    stopwatch.Stop();
                    context.Response.Headers.Add("X-Processed-Time", stopwatch.Elapsed.TotalMilliseconds.ToString() + " ms");
                }
            });
        }
    }
}
