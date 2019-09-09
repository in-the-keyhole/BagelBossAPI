using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(BagelBoss.API.API.Startup))]

namespace BagelBoss.API.API
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<Container>(GetContainer);
        }

        private Container GetContainer(IServiceProvider options)
        {
            var cosmosDbServiceEndpoint = Environment.GetEnvironmentVariable("CosmosDbServiceEndpoint");
            var cosmosDbAuthKey = Environment.GetEnvironmentVariable("CosmosDbAuthKey");
            var cosmosDbDatabaseName = Environment.GetEnvironmentVariable("CosmosDbDatabaseName");
            var cosmosDbContainerName = Environment.GetEnvironmentVariable("CosmosDbContainerName");
            var cosmosDbPartitionKey = Environment.GetEnvironmentVariable("CosmosDbPartitionKey");

            var client = new CosmosClient(cosmosDbServiceEndpoint, cosmosDbAuthKey, new CosmosClientOptions()
            {
                ConnectionMode = ConnectionMode.Direct
            });
            client.CreateDatabaseIfNotExistsAsync(cosmosDbDatabaseName).Wait();
            var database = client.GetDatabase(cosmosDbDatabaseName);
            database.CreateContainerIfNotExistsAsync(cosmosDbContainerName, cosmosDbPartitionKey);

            return database.GetContainer(cosmosDbContainerName);
        }
    }
}