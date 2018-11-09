using Nancy;
using Nancy.ModelBinding;
using System;
using TryingNancy.Trying.Managers;
using TryingNancy.Trying.Managers.Interfaces;
using TryingNancy.Trying.Models;

namespace TryingNancy.Trying.Controller
{

    public class MyController : NancyModule
    {
        public readonly IRouteDataManager routeManager;
        public readonly IDataProcessorManager dataProcessor;

        public MyController(IRouteDataManager routeManager, IDataProcessorManager dataProcessor)
        {
            this.routeManager = routeManager;
            this.dataProcessor = dataProcessor;

            this.Before.AddItemToStartOfPipeline(x => { return null; });

            this.After.AddItemToStartOfPipeline(x => { });

            this.Get("/", args => ShowMe());

            this.Get("/{id:long}", args => this.GetSomeoneByLong(args.id));

            this.Get("/{id:int}", args => this.GetSomeoneByInt(args.id));

            this.Get("/{id:guid}", args => this.GetSomeoneByGuid(args.id));

            this.Get("/stringOnly/{id}", args => this.GetSomeoneByString(args.id));

            this.Get("/country/(?<country>[A-Z]{3})/name/{name}", args => this.GetSomeoneByRegionAndName(args.country, args.name));

            this.Get("/{file}.{ext:length(3,3)}", args => this.GetSomeoneByFilleNameAndExtension(args.file, args.ext));

            this.Post("/process", args => this.ProcessData());
        }

        public string ShowMe()
        {
            return this.routeManager.ShowMe();
        }

        public string GetSomeoneByLong(long id)
        {
            return this.routeManager.GetSomeoneByLong(id);
        }

        public string GetSomeoneByInt(int id)
        {
            return this.routeManager.GetSomeoneByInt(id);
        }

        public string GetSomeoneByGuid(Guid id)
        {
            return this.routeManager.GetSomeoneByGuid(id);
        }

        public string GetSomeoneByString(string id)
        {
            return this.routeManager.GetSomeoneByString(id);
        }

        public string GetSomeoneByRegionAndName(string country, string name)
        {
            return this.routeManager.GetSomeoneByRegionAndName(country, name);
        }

        public string GetSomeoneByFilleNameAndExtension(string file, string ext)
        {
            return this.routeManager.GetSomeoneByFilleNameAndExtension(file, ext);
        }

        public object ProcessData()
        {
            var complexModel = this.Bind<VeryComplexModel>();

            return this.dataProcessor.ProcessData(complexModel);
        }
    }
}