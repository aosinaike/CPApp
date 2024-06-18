using CPApp.contract;
using CPApp.Domains;
using CPApp.dtos;
using Microsoft.AspNetCore.Mvc;

namespace CPApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {

        private ICustomQuestionService _customQuestionService;

        public CandidateController(ICustomQuestionService customQuestionService)
        {
            this._customQuestionService = customQuestionService;
        }

        [HttpGet]
        public async Task<ActionResult> GetCandidateQuestions([FromQuery] Guid employerId)
        {
            var response =  await _customQuestionService.GetCandidateQuestions(employerId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> SaveCandidateProfile(CreatePersonalInformationDTO personalInformationDTO) {
            DateTime temp;
            if (DateTime.TryParse(personalInformationDTO.DateOfBirth, out temp))
            {
                return BadRequest("Improper date format");
            }
            PersonalInformation personalInformation = new PersonalInformation
            {
                FirstName = personalInformationDTO.FirstName,
                LastName = personalInformationDTO.LastName,
                Email = personalInformationDTO.Email,
                Nationality = personalInformationDTO.Residency,
                Residency = personalInformationDTO.Residency,
                DateOfBirth = DateTime.ParseExact(personalInformationDTO.DateOfBirth, "dd/MM/yyyy", null),
                Gender = personalInformationDTO.Gender,
                IDNumber = personalInformationDTO.IDNumber,
                Phone = personalInformationDTO.Phone,
                Questions = personalInformationDTO.Questions,
                Answers = personalInformationDTO.Answers,
            };
            var response = await _customQuestionService.CreatePersonalProfile(personalInformation);
            return StatusCode((int)response.StatusCode, response.Resource);
        }
    }
}
