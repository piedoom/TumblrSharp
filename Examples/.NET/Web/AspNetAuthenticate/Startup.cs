using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;

namespace AspNetAuthenticate
{    

    /// <summary>
    /// startup
    /// </summary>
    public class Startup
    {
        private OAuthClient oAuthClient;

        private string CONSUMER_KEY = "xxx";
        private string CONSUMER_SECRET = "xxx";

        private Token accessToken;
        private Token requestToken;

        private TumblrClient tumblrClient;

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940 
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            oAuthClient = new OAuthClientFactory().Create(CONSUMER_KEY, CONSUMER_SECRET);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Map("/Request", context =>
            {
                context.Run(async (subcontext) =>
                {
                    // create the callbackurl
                    string callbackUrl = subcontext.Response.HttpContext.Request.Scheme + "://" +subcontext.Response.HttpContext.Request.Host.ToString() + "/TumblrLogIn";

                    // ordering requesttoken
                    requestToken = await oAuthClient.GetRequestTokenAsync(callbackUrl);

                    // get the url for authorization
                    var lAuthorizeUrl = oAuthClient.GetAuthorizeUrl(requestToken);

                    // call the authorize url
                    subcontext.Response.Redirect(lAuthorizeUrl.AbsoluteUri);
                    
                });
            });

            app.MapWhen(
                context => context.Request.Path.StartsWithSegments("/TumblrLogIn"),
                context =>
                {
                    context.Run(async (subcontext) =>
                    {

                        IQueryCollection query = subcontext.Request.Query;

                        query.TryGetValue("oauth_token", out StringValues oauth_token);
                        query.TryGetValue("oauth_verifier", out StringValues oauth_verifier);

                        // ordering accesstoken
                        accessToken = await oAuthClient.GetAccessTokenAsync(requestToken, oauth_token.ToString(), oauth_verifier.ToString());

                        // create tumblrclient
                        tumblrClient = new TumblrClientFactory().Create<TumblrClient>(CONSUMER_KEY, CONSUMER_SECRET, accessToken);

                        var userInfo = await tumblrClient.GetUserInfoAsync();

                        await subcontext.Response.WriteAsync($"<h1>Authenticate - Examples for Asp.Net - Login Success</h1><p>User: {userInfo.Name}</p><p>Following: {userInfo.FollowingCount}</p>");
                    });

                });

            app.Map("", context =>
            {
                context.Run(async (subcontext) =>
                {
                    if (CONSUMER_KEY == "xxx")
                    {
                        await subcontext.Response.WriteAsync("<h1>Authenticate - Examples for Asp.Net</h1><p>You must set the consumerkey and the consumersecret in the source code. Restart the project.</p>");
                    }
                    else
                    {
                        await subcontext.Response.WriteAsync("<h1>Authenticate - Examples for Asp.Net</h1><p>Login Tumblr</p><a href=\"Request\">LogIn</a>");
                    }
                });
            });

        }

    }
}
