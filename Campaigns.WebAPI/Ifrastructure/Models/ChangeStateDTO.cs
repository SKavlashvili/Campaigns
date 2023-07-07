namespace Campaigns.WebAPI.Models
{
    public class ChangeStateDTO
    {
        public string ID { get; set; }
        public string NewState { get; set; }

        public int GetState()
        {
            return (int)Enum.Parse(typeof(State), this.NewState);
        }
    }
}
