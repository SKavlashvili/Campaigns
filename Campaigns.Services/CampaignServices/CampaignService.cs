using Campaigns.Repository;
using Campaigns.Repository.Campaign.Models;
using Campaigns.Services.Exceptions;
using Mapster;
using Npgsql;
using Dapper;

namespace Campaigns.Services
{
    public class CampaignService : ICampaignService
    {
        ICampaignRepository _campaignRepo;
        public CampaignService(IServiceProvider provider)
        {
            _campaignRepo = new CampaignRepository(provider);
        }

        public async Task ChangeState(string id, int state)
        {
            if(await _campaignRepo.CampaignExists(id))
            {
                int rowsEffected = await _campaignRepo.AtomicProcess<int>(async (transaction) =>
                {
                    return await transaction.Connection.ExecuteAsync("update \"Campaigns\" set \"State\" = @state where \"CampaignID\" = @id"
                        , new { state = state, id = id });
                });
            }
            else throw new CampaignDoesNotExistsException();
        }

        public async Task<string> CloneCampaign(string id)
        {
            if (await _campaignRepo.CampaignExists(id))
            {
                CampaignRepoModel camp = await _campaignRepo.GetCampaign(id);
                string name = null;
                if (camp.CompanyName[camp.CompanyName.Length - 1] == ')')
                {
                    name = camp.CompanyName.Substring(0, camp.CompanyName.Length - 3);
                }
                else name = camp.CompanyName;
                int counter = await _campaignRepo.CountCampaignNames(name);
                string newID = Guid.NewGuid().ToString();
                await RegisterCampaign(new RegisterCampaignModel()
                {
                    CampaignID = newID,
                    CompanyName = name + $"({counter + 1})",
                    StartDate = camp.StartDate,
                    EndDate = camp.EndDate,
                    RewardType = camp.RewardType,
                    Status = 4,
                }, CancellationToken.None);
                return newID;
            }
            else throw new CampaignDoesNotExistsException();
        }

        public async Task DeleteCampaign(string id)
        {

            if (await _campaignRepo.CampaignExists(id))
            {
                if(await _campaignRepo.Checker(id, (camp) => camp.Status == 1)) throw new CampaignIsActiveException();
                await _campaignRepo.Delete(id);
            }
            else throw new CampaignDoesNotExistsException();
        }
        
        private static string BuildQueryFilter(FilterModel model)
        {
            string query = "where ";
            if(model.CompanyName != null) query += $"\"{nameof(model.CompanyName)}\" = @CompanyName and ";
            if(model.StartDate != null) query += $"\"{nameof(model.StartDate)}\" > @StartDate and ";
            if (model.EndDate != null) query += $"\"{nameof(model.EndDate)}\" < @EndDate and ";
            if(model.RewardType != null) query += $"\"{nameof(model.RewardType)}\" = @RewardType and ";
            if (model.Status != null) query += $"\"{nameof(model.Status)}\" = @Status and ";
            query = query.Substring(0, query.Length - 4);
            return query;
        }

        public async Task<List<CampaignRepoModel>> FilterData(FilterModel filter, int page, int elementsPerPage)
        {
            return (await _campaignRepo.GetContext().Connection.QueryAsync<CampaignRepoModel>($"select * from (select * from \"Campaigns\" {BuildQueryFilter(filter)} limit {page * elementsPerPage}) as x offset {(page - 1) * elementsPerPage}", filter)).ToList();
        }

        public async Task<string> RegisterCampaign(RegisterCampaignModel newCampaign, CancellationToken token)
        {
            try
            {
                return await _campaignRepo.AddCampaign(newCampaign.Adapt<CampaignRepoModel>(RegisterCampaignModel.ToCampaignRepoModel));
            }
            catch (PostgresException ex)
            {
                if (ex.Message.Split(':')[0].Equals("23505")) throw new CampaignAlreadyExistsException();

                throw ex;
            }
        }
    }
}
