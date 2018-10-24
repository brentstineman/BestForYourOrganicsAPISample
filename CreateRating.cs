
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using Microsoft.Extensions.Logging;

namespace BestForYourOrganics.API
{
    public static class CreateRating
    {
        [FunctionName("CreateRating")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req,
            [CosmosDB("ratings", "ratings", Id = "id", ConnectionStringSetting = "cosmosDBConnectionString")] out RatingDocument ratingDocument,
         ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = new StreamReader(req.Body).ReadToEnd();

            ratingDocument = null;

            try
            {
                ratingDocument = JsonConvert.DeserializeObject<RatingDocument>(requestBody);
                ratingDocument.id = System.Guid.NewGuid();
            }
            catch (Exception e)
            {
                log.LogError($"{e.ToString()}");
            }

            return ratingDocument != null
                ? (ActionResult)new JsonResult(ratingDocument)
                : new BadRequestObjectResult("Please pass id, userid, productid, timestamp, locationname, rating, and usernotes");
        }
    }
}
