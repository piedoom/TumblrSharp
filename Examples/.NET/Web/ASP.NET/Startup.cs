using DontPanic.TumblrSharp.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ASPNet
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.UseTumblrClient();
            services.AddTransient<IMyTumblrService, MyTumblrService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Map("", context =>
            {
                context.Run(async (subcontext) =>
                {
                    if (Settings.CONSUMER_KEY == "xxx")
                    {
                        await subcontext.Response.WriteAsync("<h1>Examples for Asp.Net</h1><p>You must set the consumer token and the access token in the source code. Restart the project.</p>");
                    }
                    else
                    {
                        var myService = app.ApplicationServices.GetRequiredService<IMyTumblrService>();

                        var pageContent = await myService.GetFollowerCount();

                        await subcontext.Response.WriteAsync(pageContent);
                    }                    
                });
            });
        }        
    }
}
