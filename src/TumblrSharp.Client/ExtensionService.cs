using Microsoft.Extensions.DependencyInjection;
using System;

namespace DontPanic.TumblrSharp.Client
{
    /// <summary>
    /// extensionclass for IServiceCollection
    /// </summary>
    public static class ExtensionService
    {
        /// <summary>
        /// Configure ServiceCollection for <see cref="System.Net.Http.IHttpClientFactory">HttpClientFactory</see>
        /// </summary>
        /// <param name="services">services collection to add httpclient for <see cref="System.Net.Http.IHttpClientFactory">HttpClientFactory</see> to create internal HttpClient
        /// </param>
        public static void UseTumblrClient(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            _ = services.AddHttpClient();
        }
    }
}
