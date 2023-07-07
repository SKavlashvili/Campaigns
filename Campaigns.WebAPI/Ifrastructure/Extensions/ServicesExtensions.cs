using Campaigns.Infrastructure;
using Campaigns.Services;
using Campaigns.WebAPI.Ifrastructure.Validations;
using Campaigns.WebAPI.Models;
using Campaigns.WebAPI.Validations;
using FluentValidation;
using System.Security.AccessControl;

namespace Campaigns.WebAPI.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            //Validations
            services.AddSingleton<IValidator<RegisterCampaignsDTO>, RegisterCampaignDTOValidation>();
            services.AddSingleton<IValidator<ChangeStateDTO>, ChangeStateValidation>();

            //Services
            services.AddScoped<ICampaignService, CampaignService>();
            services.AddScoped<CampaignsDBContext>((IServiceProvider provider) =>
            {
                return new CampaignsDBContext(provider.GetService<IConfiguration>());
            });

        }
    }
}
