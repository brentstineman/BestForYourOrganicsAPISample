using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BestForYourOrganics.API
{
    public static class GetRating
    {
        [FunctionName("GetRating")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetRating/{id}")]HttpRequest req, 
            string id,
             [CosmosDB( "ratings", "ratings", ConnectionStringSetting = "cosmosDBConnectionString", 
                Id = "{id}")] RatingDocument ratingDoc,
            ILogger log)
        {
            if (ratingDoc == null)
            {
                log.LogInformation($"Document Rating item not found");
            }
            else
            {
                log.LogInformation($"Found Document, ID={ratingDoc.id}");
            }

            return (ActionResult)new OkObjectResult(ratingDoc);
        }
    }
}
