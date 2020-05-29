#if (NETSTANDARD2_0 || NETCOREAPP2_2)
using Microsoft.Extensions.DependencyInjection;
#endif

using System;

namespace DontPanic.TumblrSharp.Client
{
    /// <summary>
    /// extensionclass for IServiceCollection
    /// </summary>
    public static class ExtensionService
    {
#if (NETSTANDARD2_0 || NETCOREAPP2_2)
        /// <summary>
        /// Configure ServiceCollection for <see cref="Microsoft.Extensions.DependencyInjection.IHttpClientFactory">HttpClientFactory</see>
        /// </summary>
        /// <param name="services">
        /// <see cref="Microsoft.Extensions.DependencyInjection.IHttpClientFactory">HttpClientFactory</see> to create internal HttpClient
        /// </param>
        public static void UseTumblrClient(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            _ = services.AddHttpClient();
        }
#endif
    }
}
