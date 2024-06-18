using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents.Client;

namespace CPApp.contract
{
    public interface ICosmosClientConnection
    {
        public Task<Container> CreateContainerAsync(string containerId);
    }
}
