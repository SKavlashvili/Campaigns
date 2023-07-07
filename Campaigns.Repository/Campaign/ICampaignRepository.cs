
using Campaigns.Infrastructure;
using Campaigns.Repository.Campaign.Models;
using Npgsql;

namespace Campaigns.Repository
{
    public interface ICampaignRepository
    {
        Task<string> AddCampaign(CampaignRepoModel newCampaign);
        Task<bool> CampaignExists(string id);

        Task<T> AtomicProcess<T>(Func<NpgsqlTransaction, Task<T>> process);

        Task Delete(string id);

        Task<bool> Checker(string id, Func<CampaignRepoModel, bool> checkDirectives);

        Task<int> CountCampaignNames(string name);

        Task<CampaignRepoModel> GetCampaign(string id);

        public CampaignsDBContext GetContext();
    }
}
