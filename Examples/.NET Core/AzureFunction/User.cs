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
using System.Net;
using System.Net.Http.Headers;

namespace AzureTest
{
    public class User
    {
        private readonly IMyTumblrService _service;

        public User(IMyTumblrService myTumblrService)
        {
            _service = myTumblrService;
        }

        [FunctionName("User")]
        public async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function: User processed a request.");

            string responseMessage = await _service.GetUser();

            if (responseMessage == null)
            {
                responseMessage = "Es ist ein Fehler im TumblrService aufgetreten, versuchen sie es später nochmal";
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(responseMessage);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");


            return response;
        }
    }
}
