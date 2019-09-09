using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;
using BagelBoss.API.API.Entities;

namespace BagelBoss.API.API
{
    public class Function1
    {
        private readonly Container bagelContainer;

        public Function1(Container bagelContainer)
        {
            this.bagelContainer = bagelContainer;
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            if (String.IsNullOrEmpty(name))
            {
                return new BadRequestObjectResult("Please pass a name on the query string or in the request body");
            }

            var bagel = new Bagel()
            {
                Id = Guid.NewGuid(),
                Store = name,
                Schmear = "Plain whipped cream cheese"
            };

            await bagelContainer.CreateItemAsync(bagel, bagel.PartitionKey);

            return (ActionResult)new OkObjectResult($"Your bagel order, id {bagel.Id}, has been placed at the store named {bagel.Store} for a bagel with the following schmear: {bagel.Schmear}");
        }
    }
}