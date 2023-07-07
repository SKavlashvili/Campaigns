using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Campaigns.Infrastructure
{
    public class CampaignsDBContext
    {
        public NpgsqlConnection Connection { get; set; }
        public CampaignsDBContext(IConfiguration configs)
        {
            Connection = new NpgsqlConnection(configs.GetConnectionString("CampaignsDBConnectionString"));
            Connection.Open();
        }
    }
}
