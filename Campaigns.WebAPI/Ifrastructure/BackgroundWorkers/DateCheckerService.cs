using Campaigns.Infrastructure;
using Campaigns.Repository;
using Campaigns.Repository.Campaign.Models;
using Dapper;

namespace Campaigns.WebAPI.Ifrastructure.BackgroundWorkers
{
    public class DateCheckerService : BackgroundService
    {
        private CampaignRepository _repo;
        public DateCheckerService(IServiceProvider serviceProvider)
        {
            _repo = new CampaignRepository(serviceProvider.CreateScope().ServiceProvider);
        }

        public static string BuildSetOfIDS(List<string> ids)
        {
            string res = "(";
            for(int i = 0; i < ids.Count - 1; i++)
            {
                res += $"'{ids[i]}'" + ",";
            }
            if (ids.Count > 0) res += $"'{ids[ids.Count - 1]}'";
            res += ")";
            return res;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(true)
            {
                try
                {
                    List<string> ids = (await _repo.GetContext().Connection.QueryAsync<string>("select \"CampaignID\" from \"Campaigns\" where \"EndDate\" < @dateNow and \"Status\" != 5 and \"Status\" != 2", new { dateNow = DateTime.Now })).ToList();
                    string set = BuildSetOfIDS(ids);
                    if(ids.Count != 0) await _repo.GetContext().Connection.ExecuteAsync($"update \"Campaigns\" set \"Status\" = 5 where \"CampaignID\" in {set}");
                    await Task.Delay(5000);
                }catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    await Task.Delay(5000);
                }
            }
        }
    }

}
