namespace Campaigns.Repository.Campaign.Models
{
    public class CampaignRepoModel
    {
        public string CampaignID { get; set; }
        public string CompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RewardType { get; set; }
        public int Status { get; set; }
        public int State { get; set; }
        public DateTime CreateDate { get; set; }
        public CampaignRepoModel()
        {
            CreateDate = DateTime.Now;
        }
    }
}
