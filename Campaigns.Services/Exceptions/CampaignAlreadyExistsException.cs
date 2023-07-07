
namespace Campaigns.Services
{
    public class CampaignAlreadyExistsException : BaseResponseException
    {
        public CampaignAlreadyExistsException() : base(400,"Campaign already exists")
        {

        }
    }
}
