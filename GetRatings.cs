using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace BestForYourOrganics.API
{
    public static class GetRatings
    {
        [FunctionName("GetRatings")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetRatings/{userid}")]HttpRequest req, 
            string userid,
            [CosmosDB("ratings", "ratings", ConnectionStringSetting = "cosmosDBConnectionString", SqlQuery = "SELECT * FROM c WHERE c.userid={userid}")] IEnumerable<object> documents,
            ILogger log)
        {
            if (documents == null)
            {
                log.LogInformation($"Found items for user {userid}");
            }
            else
            {
                log.LogInformation($"Found rating items for user {userid}");
            }

            return (ActionResult)new OkObjectResult(documents);
        }
    }
}
