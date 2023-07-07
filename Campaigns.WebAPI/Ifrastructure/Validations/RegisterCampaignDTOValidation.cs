using Campaigns.WebAPI.Models;
using FluentValidation;

namespace Campaigns.WebAPI.Validations
{
    public class RegisterCampaignDTOValidation : AbstractValidator<RegisterCampaignsDTO>
    {
        public RegisterCampaignDTOValidation()
        {
            RuleFor<string>(campaign => campaign.CompanyName)
                .NotEmpty()
                .Must((string name) => name.Length > 5 && name.Length < 30)
                .WithMessage("CompanyName's length should be between 5 and 30");

            RuleFor<DateTime>(campaign => campaign.StartDate).NotEmpty();
            RuleFor<DateTime>(campaign => campaign.EndDate).NotEmpty();

            RuleFor(campaign => campaign)
                .NotEmpty()
                .Must(campaign => campaign.StartDate.Date < campaign.EndDate.Date)
                .WithMessage("StartDate should be less then EndDate");

            int RewardsMaxValue = (int)Enum.GetValues(typeof(Reward)).Cast<Reward>().Max();
            int RewardsMinValue = (int)Enum.GetValues(typeof(Reward)).Cast<Reward>().Min();

            RuleFor(campaign => campaign.RewardType)
                .Must(reward => reward <= RewardsMaxValue && reward >= RewardsMinValue)
                .WithMessage($"roeward value must be between {RewardsMinValue} and {RewardsMaxValue}");


        }
    }
}
