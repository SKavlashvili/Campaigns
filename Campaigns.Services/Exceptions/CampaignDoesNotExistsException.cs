
namespace Campaigns.Services.Exceptions
{
    public class CampaignDoesNotExistsException : BaseResponseException
    {
        public CampaignDoesNotExistsException() : base(400,"This campaign does't exists")
        {

        }
    }
}
