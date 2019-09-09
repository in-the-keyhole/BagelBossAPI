# BagelBossAPI
Integrating Azure Functions with Cosmos DB SQL API in .NET Core 2.2

### This repository accompanies a tutorial from Zach Gardner on the Keyhole Software blog.

## Blog Overview
I am working on a project that leverages both Azure Functions as well as [Cosmos DB](https://docs.microsoft.com/en-us/azure/cosmos-db/introduction). In trying to get both of these components wired together, I found that there are very few examples that work with the most recent versions of these components. I also saw examples that could work at a small scale, but don’t show industry-standard best practices, and would lead to performance issues if deployed in an environment with any meaningful traffic.

To that end, I put together this blog post showing how to set up an Azure Functions project in .NET Core 2.2 to integrate with Cosmos DB’s SQL API using its native tooling.

## Pre-Reqs
In order to get this example solution running, make sure that you have the most recent version of Visual Studio, which as of writing this post, is Visual Studio 2019 16.2.4. Also, make sure that you have the Azure SDK installed along with .NET Core 2.2 SDK.

Also, be sure to download and install the [Azure Cosmos DB Emulator](https://aka.ms/cosmosdb-emulator). This is what allows a developer to run a simulated Cosmos DB instance on their machine.
