using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;
using TryingNancy.Trying.Managers;
using TryingNancy.Trying.Managers.Interfaces;
using TryingNancy.Trying.Serializer;

namespace TryingNancy.Trying
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            this.SetupPipelines(pipelines);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<JsonSerializer, CamelCaseJsonSerializer>();
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            container.Register<IDataProcessorManager, DataProcessorManager>().AsSingleton();
            container.Register<IRouteDataManager, RouteDataManager>().AsMultiInstance();
        }

        private void SetupPipelines(IPipelines pipelines)
        {
            pipelines.BeforeRequest.AddItemToEndOfPipeline((context) =>
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                context.Items.Add("Stopwatch", stopwatch);

                bool doesWeHaveToStop = context.Request.Headers["Joao-Cleber"].FirstOrDefault() == "PARA-PARA" ? true : false;

                if (doesWeHaveToStop)
                    return "You've said us to stop...";

                if (context.Request.Headers.Authorization == "")
                    return HttpStatusCode.Unauthorized;
                                
                return null;
            });

            pipelines.OnError.AddItemToEndOfPipeline((context, ex) =>
            {
                return null;
            });

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