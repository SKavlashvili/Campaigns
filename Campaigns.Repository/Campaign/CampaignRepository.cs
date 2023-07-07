using Campaigns.Infrastructure;
using Campaigns.Repository.Campaign.Models;
using Microsoft.Extensions.Configuration;
using Dapper;
using Npgsql;
using System.Threading.Tasks.Dataflow;

namespace Campaigns.Repository
{
    public class CampaignRepository : ICampaignRepository
    {
        private CampaignsDBContext _campaignDbContext;
        public CampaignRepository(IServiceProvider provider)
        {
            _campaignDbContext = (CampaignsDBContext)provider.GetService(typeof(CampaignsDBContext));
        }

        public async Task<string> AddCampaign(CampaignRepoModel newCampaign)
        {
            string query = "INSERT INTO \"Campaigns\"(\"CampaignID\",\"CreateDate\",\"CompanyName\",\"StartDate\",\"EndDate\",\"RewardType\",\"Status\") " +
                "VALUES(@CampaignID,@CreateDate,@CompanyName,@StartDate,@EndDate,@RewardType,@Status)";
            await _campaignDbContext.Connection.ExecuteAsync(query, newCampaign);
            return newCampaign.CampaignID;
        }

        public async Task<T> AtomicProcess<T>(Func<NpgsqlTransaction, Task<T>> process)
        {
            T res = default;
            using(NpgsqlTransaction transaction = await _campaignDbContext.Connection.BeginTransactionAsync())
            {
                res = await process(transaction);
                await transaction.CommitAsync();
            }
            return res;
        }
        public CampaignsDBContext GetContext()
        {
            return _campaignDbContext;
        }
        public async Task<bool> CampaignExists(string id)
        {
            string query = "select * from \"Campaigns\" where \"CampaignID\" = @id";
            CampaignRepoModel campaign = await _campaignDbContext.Connection.QuerySingleOrDefaultAsync<CampaignRepoModel>(query, new { id = id });
            if (campaign == null) return false;
            return true;
        }
        public async Task Delete(string id)
        {
            string query = "update \"Campaigns\" set \"Status\" = 2 where \"CampaignID\" = @id";
            await _campaignDbContext.Connection.ExecuteAsync(query, new {id = id});
        }

        public async Task<bool> Checker(string id, Func<CampaignRepoModel,bool> checkDirectives)
        {
            string query = "select * from \"Campaigns\" where \"CampaignID\" = @id";
            var Params = new {id = id };
            CampaignRepoModel campaign = await _campaignDbContext.Connection.QuerySingleAsync<CampaignRepoModel>(query,Params);
            return checkDirectives(campaign);
        }

        public async Task<CampaignRepoModel> GetCampaign(string id)
        {
            string query = "select * from \"Campaigns\" where \"CampaignID\" = @id";
            var Params = new {id = id};
            CampaignRepoModel campaign = await _campaignDbContext.Connection.QuerySingleAsync<CampaignRepoModel>(query, Params);
            return campaign;
        }

        public async Task<int> CountCampaignNames(string name)
        {
            string query = "select count(*) from \"Campaigns\" where \"CompanyName\" ilike @someRegex";
            int count = await _campaignDbContext.Connection.QuerySingleAsync<int>(query, new { someRegex = $"{name}(%)"});
            return count + 1;
        }
    }
}
