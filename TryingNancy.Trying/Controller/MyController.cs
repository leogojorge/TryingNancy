using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TryingNancy.Trying.Models;

namespace TryingNancy.Trying.Controller
{
    public class MyController : NancyModule
    {

        public MyController()
        {
            this.Get("/", args => { 
               this.Context.Trace.TraceLog.WriteLog(x => x.Append(ShowMe()));
                return ShowMe();
            });
        
            this.Post("/process", args => ProcessData());
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
            var result = new Dictionary<string, string>();
            result.Add("Result", "OK");

            return JsonConvert.SerializeObject(result);

        //    return @"
        // _   _          _   _  _______     __
        //| \ | |   /\   | \ | |/ ____\ \   / /
        //|  \| |  /  \  |  \| | |     \ \_/ / 
        //| . ` | / /\ \ | . ` | |      \   /  
        //| |\  |/ ____ \| |\  | |____   | |   
        //|_| \_/_/    \_\_| \_|\_____|  |_|   ";
        }
    }
}