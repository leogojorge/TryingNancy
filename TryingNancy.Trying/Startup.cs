using Microsoft.AspNetCore.Builder;
using Nancy.Owin;

namespace TryingNancy.Trying
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(owin => owin.UseNancy(new NancyOptions
            {
                Bootstrapper = new Bootstrapper()
            }));
        }
    }
}
