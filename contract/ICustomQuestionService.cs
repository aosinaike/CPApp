using CPApp.Domains;
using CPApp.dtos;
using Microsoft.Azure.Cosmos;

namespace CPApp.contract
{
    public interface ICustomQuestionService
    {
        Task<CustomQuestion> GetQuestion(Guid questionId);
        Task<ItemResponse<CustomQuestion>> CreateCustomQuestion(CustomQuestion customQuestion);
        Task<ItemResponse<CustomQuestion>> UpdateCustomQuestion(CustomQuestion customQuestion);
        Task<CreatePersonalInformationDTO> GetCandidateQuestions(Guid EmployeeId);
        Task<ItemResponse<PersonalInformation>> CreatePersonalProfile(PersonalInformation personalInformation);

    }
}
