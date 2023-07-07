using Campaigns.Repository.Campaign.Models;

namespace Campaigns.Services
{
    public interface ICampaignService
    {
        public Task<string> RegisterCampaign(RegisterCampaignModel newCampaign, CancellationToken token);
        Task DeleteCampaign(string id);
        Task ChangeState(string id, int state);
        Task<string> CloneCampaign(string id);

        Task<List<CampaignRepoModel>> FilterData(FilterModel filter, int page, int elementsPerPage);
    }
}
