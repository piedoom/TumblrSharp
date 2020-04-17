using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Testing
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            TumblrClientFactory.ConfigureService(services, Settings.CONSUMER_KEY, Settings.CONSUMER_SECRET, new Token(Settings.OAUTH_TOKEN, Settings.OAUTH_TOKEN_SECRET));

            services.AddTransient<IMyTumblrService, MyTumblrService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Map("", context => {

                context.Run(async (subcontext) =>
                {
                    var myService = app.ApplicationServices.GetRequiredService<IMyTumblrService>();
                    var pageContent = await myService.GetFollowerCount();

                    await subcontext.Response.WriteAsync(pageContent);
                });
                
                });
        }
    }
}
