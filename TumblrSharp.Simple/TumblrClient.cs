using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DontPanic.TumblrSharp.Simple
{
    /// <summary>
    /// Call unauthenticated endpoints from the Tumblr V1 API.
    /// Because this does not require authentication, methods will be static for ease of use.
    /// </summary>
    public static class TumblrClient
    {

        async public static void GetPageAsync(string blog)
        {
            string endpoint = BuildURL(blog);
            string responseDirty = await endpoint.GetJsonAsync();
            string response = StripInvalidJS(responseDirty);

            // convert JSON to 
        }

        /// <summary>
        /// Returns the API route for a blog.
        /// </summary>
        /// <param name="blog">The name of the target blog.</param>
        /// <returns>The V1 API endpoint for the target blog.</returns>
        private static string BuildURL(string blog)
        {
            return $"http://{blog}.tumblr.com/api/read/json";
        }

        /// <summary>
        /// The V1 Tumblr API doesn't return pure JSON, but a JavaScript object.  It consistently returns
        /// 'var tumblr_api_read = ' before each JSON object, and a ';' at the end.  Because the V1 API is not subject to change,
        /// we can simply perform a substring operation on our string to get rid of these garbage characters.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static string StripInvalidJS(string json)
        {
            // index of where JSON actually starts
            const int beginning = 21;
            // index of how many characters to cut at the end
            const int end = 1;

            // total length of the substring
            int length = json.Length - end;

            return json.Substring(beginning, length);
        }
    }




    
}
