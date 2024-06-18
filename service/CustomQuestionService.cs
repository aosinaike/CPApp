using CPApp.constants;
using CPApp.contract;
using CPApp.Domains;
using CPApp.dtos;
using Microsoft.Azure.Cosmos;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Data.Common;
using System.Net;

namespace CPApp.service
{
    public class CustomQuestionService : ICustomQuestionService
    {
        private ICosmosClientConnection _cosmosClient;
        Container container;
        public CustomQuestionService(ICosmosClientConnection cosmosClient) {
            this.container = cosmosClient.CreateContainerAsync("custom_question").Result;
        }
        public async Task<ItemResponse<CustomQuestion>> CreateCustomQuestion(CustomQuestion customQuestion)
        {
            customQuestion.Id = Guid.NewGuid();
            
            try
            {
                // Read the item to see if it exists.  
                ItemResponse<CustomQuestion> customQuestionResponse = await this.container.ReadItemAsync<CustomQuestion>(customQuestion.Id.ToString(), new PartitionKey(customQuestion.Id.ToString()));
                Console.WriteLine("Item in database with id: {0} already exists\n", customQuestionResponse.Resource.Id);
                return customQuestionResponse;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Create an item in the container representing the Question."
                ItemResponse<CustomQuestion> customQuestionResponse = await this.container.CreateItemAsync<CustomQuestion>(customQuestion, new PartitionKey(customQuestion.Id.ToString()));

                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", customQuestionResponse.Resource.Id, customQuestionResponse.RequestCharge);
                return customQuestionResponse;
            }
        }

        public async Task<CustomQuestion> GetQuestion(Guid questionId)
        {
            ItemResponse<CustomQuestion> customQuestionResponse = await this.container.ReadItemAsync<CustomQuestion>(questionId.ToString(), new PartitionKey(questionId.ToString()));
            Console.WriteLine("Item in database with id: {0} already exists\n", customQuestionResponse.Resource.Id);
            return customQuestionResponse;
        }

        public async Task<ItemResponse<CustomQuestion>> UpdateCustomQuestion(CustomQuestion customQuestion)
        {
            var updateCustomQuestionResponse = await this.container.ReplaceItemAsync<CustomQuestion>(customQuestion, customQuestion.Id.ToString(), new PartitionKey(customQuestion.Id.ToString()));
            return updateCustomQuestionResponse;
        }

        public async Task<CreatePersonalInformationDTO> GetCandidateQuestions(Guid EmployerId)
        {
            var sqlQueryText = "SELECT * FROM c WHERE c.EmployerId = '"+ EmployerId + "'";
            string answer = "";
            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<CustomQuestion> queryResultSetIterator = this.container.GetItemQueryIterator<CustomQuestion>(queryDefinition);

            Dictionary<Guid, CustomQuestion> questionsList = new Dictionary<Guid, CustomQuestion>();
            Dictionary<Guid, string> answerList = new Dictionary<Guid, string>();
            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<CustomQuestion> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (CustomQuestion customQuestion in currentResultSet)
                {
                    questionsList.Add(customQuestion.Id, customQuestion);
                    answerList.Add(customQuestion.Id, answer);
                    Console.WriteLine("\tRead {0}\n", customQuestion);
                }
            }

            CreatePersonalInformationDTO pii = new CreatePersonalInformationDTO
            {
                Questions = questionsList,
                Answers = answerList
            };
            return pii;
        }

        public async Task<ItemResponse<PersonalInformation>> CreatePersonalProfile(PersonalInformation personalInformation)
        {
            personalInformation.Id = Guid.NewGuid();
            try
            {
                // Read the item to see if it exists.  
                ItemResponse<PersonalInformation> personalInformationResponse = await this.container.ReadItemAsync<PersonalInformation>(personalInformation.Id.ToString(), new PartitionKey(personalInformation.Id.ToString()));
                Console.WriteLine("Item in database with id: {0} already exists\n", personalInformationResponse.Resource.Id);
                return personalInformationResponse;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Create an item in the container representing Personal Information."
                ItemResponse<PersonalInformation> personalInformationResponse = await this.container.CreateItemAsync<PersonalInformation>(personalInformation, new PartitionKey(personalInformation.Id.ToString()));
                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", personalInformationResponse.Resource.Id, personalInformationResponse.RequestCharge);
                return personalInformationResponse;
            }
        }
    }
}
