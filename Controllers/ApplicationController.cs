using CPApp.constants;
using CPApp.contract;
using CPApp.Domains;
using CPApp.dtos;
using CPApp.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CPApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private ICustomQuestionService _customQuestionService;

        public ApplicationController(ICustomQuestionService customQuestionService)
        {
            this._customQuestionService = customQuestionService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewApplicationQuestion(CreateCustomQuestionDTO customQuestionDTO)
        {
            CustomQuestion customQuestion = new CustomQuestion
            {
                EmployerId = Guid.NewGuid(),
                enableOthersOption = customQuestionDTO.enableOthersOption,
                Options = customQuestionDTO.Options,
                Question = customQuestionDTO.Question,
                Type = QuestionTypeUtil.ToQuestionType(customQuestionDTO.Type)
            };
            var response = await _customQuestionService.CreateCustomQuestion(customQuestion);

            return StatusCode((int)response.StatusCode, response.Resource);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateApplicationCustomQuestion(CustomQuestion customQuestion)
        {
            var exisitingCustomQuestion = await _customQuestionService.GetQuestion(customQuestion.Id);
            exisitingCustomQuestion.enableOthersOption = customQuestion.enableOthersOption;
            exisitingCustomQuestion.Options = customQuestion.Options;
            exisitingCustomQuestion.Question = customQuestion.Question;
            var response = await _customQuestionService.UpdateCustomQuestion(customQuestion);
            return StatusCode((int)response.StatusCode, response.Resource);
        }
    }
}
