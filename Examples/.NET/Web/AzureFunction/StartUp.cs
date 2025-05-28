using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata.Ecma335;

[assembly: FunctionsStartup(typeof(AzureTest.StartUp))]

namespace AzureTest
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.UseTumblrClient();

            builder.Services.AddScoped<IMyTumblrService, MyTumblrService>();
        }
    }
}
