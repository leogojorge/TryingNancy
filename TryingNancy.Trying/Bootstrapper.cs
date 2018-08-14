using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.Diagnostics;
using Nancy.TinyIoc;
using System.Diagnostics;
using System.Threading;
using System.Linq;

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
                Stopwatch stopwatch = Stopwatch.StartNew();
                context.Items.Add("Stopwatch", stopwatch);

                bool doesWeHaveToStop = context.Request.Headers["PARA-PARA"].FirstOrDefault() == null ? false : true;

                if(doesWeHaveToStop)
                    return "You said us to stop...";

                if (context.Request.Headers.Authorization == "")
                    return HttpStatusCode.Unauthorized;
                                
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
