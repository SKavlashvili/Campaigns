using Campaigns.Services;
using Mapster;

namespace Campaigns.WebAPI.Models
{
    public class Filter
    {
        public static TypeAdapterConfig FilterToFilterModel;
        static Filter()
        {
            FilterToFilterModel = new TypeAdapterConfig();
            FilterToFilterModel.ForType<Filter, FilterModel>()
                .Map(dest => dest.RewardType, src => src.RewardType)
                .Map(dest => dest.CompanyName, src => src.CompanyName)
                .Map(dest => dest.EndDate, src => src.EndDate)
                .Map(dest => dest.StartDate, src => src.StartDate)
                .Map(dest => dest.Status, src => src.Status);
        }

        public string? CompanyName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? RewardType { get; set; }

        public int? Status { get; set; }
    }
}
