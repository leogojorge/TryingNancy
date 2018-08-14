using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using TryingNancy.Trying.Models;

namespace TryingNancy.Trying.Controller
{

    public class MyController : NancyModule
    {

        public MyController()
        {
            this.Get("/", args => ShowMe());
           
            this.Get("/{id:string}", args => GetSomeone(args.id as string));

            this.Get("/intOnly/{id:int}", args => GetSomeoneByInt(args.id));

            this.Get("/guidOnly/{id:guid}", args => GetSomeoneByGuid(args.id));

            this.Get("/stringOnly/{id}", args => GetSomeoneByString(args.id as string));
            
            this.Post("/process", args => ProcessData());
        }

        public string GetSomeone(object id)
         {
             return "Id: " + id + " - Param type: " + id.GetType();
         }        

         public string GetSomeoneByInt(int id)
         {
             return "Id: " + id + " - Param type: " + id.GetType();
         }        
         public string GetSomeoneByGuid(Guid id)
         {
             return "Id: " + id + " - Param type: " + id.GetType();
         }        
         public string GetSomeoneByString(string id)
         {
            return "Id: " + id + " - Param type: " + id.GetType();
         }

        public object ProcessData()
        {
            var complexModel = this.Bind<VeryComplexModel>();

            var response = new ResponseModel<VeryComplexModel>()
            {
                ResponseBody = complexModel,
                StatusCode = HttpStatusCode.OK
            };
            
            return response;
        }
        public string ShowMe()
        {          
            return @"
             _   _          _   _  _______     __
            | \ | |   /\   | \ | |/ ____\ \   / /
            |  \| |  /  \  |  \| | |     \ \_/ / 
            | . ` | / /\ \ | . ` | |      \   /  
            | |\  |/ ____ \| |\  | |____   | |   
            |_| \_/_/    \_\_| \_|\_____|  |_|   ";
        }
    }
    
}