namespace Campaigns.Services
{
    public class FilterModel
    {
        public string? CompanyName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? RewardType { get; set; }

        public int? Status { get; set; }
    }
}
