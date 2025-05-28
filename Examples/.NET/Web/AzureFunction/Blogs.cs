using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;
using System.Net;
using System.Net.Http.Headers;

namespace AzureTest
{
    public class Blogs
    {
        private readonly IMyTumblrService _service;

        public Blogs(IMyTumblrService myTumblrService)
        {
            _service = myTumblrService;
        }

        [FunctionName("Blogs")]
        public async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            dynamic data = JsonConvert.DeserializeObject(requestBody);
            
            name = name ?? data?.name;

            string responseMessage = await _service.GetBlog(name);

            if (responseMessage == null)
                responseMessage = "Es ist ein Fehler im TumblrService aufgetreten, versuchen sie es später nochmal";

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseMessage)
            };

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            return response;
        }
    }
}
