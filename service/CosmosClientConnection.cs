using CPApp.contract;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents.Client;

namespace CPApp.service
{
    public class CosmosClientConnection : ICosmosClientConnection
    {
        protected string DatabaseId { get; }
        protected string CollectionId { get; set; }
        private readonly string _endpointUrl;
        private readonly string _authKey;
        private const string _partitionKey = "/id";
        private readonly ILogger<ICosmosClientConnection> _logger;

        private CosmosClient cosmosClient;

        // The database we will create
        private Database database;

        // The container we will create.
        private Container container;

        // The name of the database and container we will create
        private string databaseId = "cpapp_db";

        public CosmosClientConnection(IConfiguration config, ILogger<ICosmosClientConnection> logger)
        {
            DatabaseId = config.GetValue<string>("Cosmos:DatabaseId");
            _endpointUrl = config.GetValue<string>("Cosmos:EndPointUri");
            _authKey = config.GetValue<string>("Cosmos:PrimaryKey");
            _logger = logger;
            this.cosmosClient = new CosmosClient(_endpointUrl, _authKey, new CosmosClientOptions() { ApplicationName = "CPApp" });
        }

        private async Task<Database> CreateDatabaseAsync()
        {
            // Create a new database
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
            Console.WriteLine("Created Database: {0}\n", this.database.Id);
            return this.database;
        }

        public async Task<Container> CreateContainerAsync(string containerId)
        {
            await CreateDatabaseAsync();
            // Create a new container
            this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, _partitionKey, 400);
            Console.WriteLine("Created Container: {0}\n", this.container.Id);
            return this.container;
        }
    }
}
