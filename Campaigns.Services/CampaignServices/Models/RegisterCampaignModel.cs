
using Campaigns.Repository.Campaign.Models;
using Mapster;
using System.Net.NetworkInformation;

namespace Campaigns.Services
{
    public class RegisterCampaignModel
    {
        public static TypeAdapterConfig ToCampaignRepoModel;
        static RegisterCampaignModel()
        {
            ToCampaignRepoModel = new TypeAdapterConfig();
            ToCampaignRepoModel.ForType<RegisterCampaignModel, CampaignRepoModel>()
                .Map(dest => dest.CampaignID, src => src.CampaignID)
                .Map(dest => dest.CompanyName, src => src.CompanyName)
                .Map(dest => dest.StartDate, src => src.StartDate)
                .Map(dest => dest.EndDate, src => src.EndDate)
                .Map(dest => dest.RewardType, src => src.RewardType)
                .Map(dest => dest.Status, src => src.Status);
        }
        public string CampaignID { get; set; }
        public string CompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RewardType { get; set; }
        public int Status { get; set; }
    }
}
