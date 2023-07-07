using Campaigns.Services;
using Campaigns.WebAPI.Models;
using FluentValidation;
using FluentValidation.Results;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Campaigns.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private IValidator<RegisterCampaignsDTO> _campaignsRegistrationValidator;
        private IValidator<ChangeStateDTO> _changeStateValidator;
        
        private ICampaignService _campaignService;
        public CampaignsController(IValidator<RegisterCampaignsDTO> CampaignsValidator, ICampaignService campaignService, 
            IValidator<ChangeStateDTO> stateValidator)
        {
            _campaignsRegistrationValidator = CampaignsValidator;
            _campaignService = campaignService;
            _changeStateValidator = stateValidator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddCampaign([FromBody] RegisterCampaignsDTO campaignsDTO, CancellationToken token)
        {
            ValidationResult valRes = await _campaignsRegistrationValidator.ValidateAsync(campaignsDTO);
            if (!valRes.IsValid) return BadRequest(valRes.Errors);
            
            return Ok(await _campaignService.RegisterCampaign(campaignsDTO.Adapt<RegisterCampaignModel>(RegisterCampaignsDTO.ToRegisterCampaignModel),token));
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> ChangeState([FromBody] ChangeStateDTO newState)
        {
            ValidationResult valRes = await _changeStateValidator.ValidateAsync(newState);
            if (!valRes.IsValid) return BadRequest(valRes.Errors);
            await _campaignService.ChangeState(newState.ID,newState.GetState());

            return Ok();
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteCampaign(string id)
        {
            await _campaignService.DeleteCampaign(id);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CloneCampaign(string id)
        {
            return Ok(await _campaignService.CloneCampaign(id));
        }

        [HttpPost("[action]/{page}/{elementsPerPage}")]
        public async Task<IActionResult> FilterData([FromBody] Filter filter, int page, int elementsPerPage)
        {
            return Ok(await _campaignService.FilterData(filter.Adapt<FilterModel>(Filter.FilterToFilterModel), page, elementsPerPage));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> TestAction()
        {
            Console.WriteLine("In TestAction");
            return Ok();
        }
        
    }
}
