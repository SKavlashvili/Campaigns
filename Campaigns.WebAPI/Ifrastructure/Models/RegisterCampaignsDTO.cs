using Mapster;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Campaigns.Services;
namespace Campaigns.WebAPI.Models
{
    public enum Reward
    {
        Free_spin = 1,
        Cash,
    }

    public enum Status
    {
        Active = 1,
        Cancelled,
        Re_Activated,
        Empty,//ეს გამოჩნდება  მხოლოდ Unpublished კამპანიის შემთხვევაში და რედაქტირება იქნება
              //შეუძლებელი (სანამ არ დაფაბლიშდება კამპანია). 
        Finished,
    }

    public enum State
    {
        Unpublished = 1,
        Published
    }
    public class RegisterCampaignsDTO
    {
        public static TypeAdapterConfig ToRegisterCampaignModel;
        static RegisterCampaignsDTO()
        {
            ToRegisterCampaignModel = new TypeAdapterConfig();
            ToRegisterCampaignModel.ForType<RegisterCampaignsDTO, RegisterCampaignModel>()
                .Map(dest => dest.CampaignID, src => src.GetID())
                .Map(dest => dest.RewardType, src => ((Reward)src.RewardType).ToString())
                .Map(dest => dest.StartDate, src => src.StartDate)
                .Map(dest => dest.CompanyName, src => src.CompanyName)
                .Map(dest => dest.EndDate, src => src.EndDate)
                .Map(dest => dest.Status, src => src.GetStatus());
            


        }
        private string _campaignID;
        public string CompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set;}
        public int RewardType { get; set; }
        private int _status;
        public RegisterCampaignsDTO()
        {
            _campaignID = Guid.NewGuid().ToString();
            _status = (int)Status.Empty;
        }
        public int GetStatus()
        {
            return _status;
        }
        public string GetID()
        {
            return _campaignID;
        }


    }
}
