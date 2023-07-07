

namespace Campaigns.Services.Exceptions
{
    public class CampaignIsActiveException : BaseResponseException
    {
        public CampaignIsActiveException() : base(400,"This campaign is active")
        {

        }
    }
}
