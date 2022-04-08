namespace OnePageNet.Data.Data.Models
{
    public class UpdateUserRelationsDTO:BaseDTO
    {
        public string CurrentUserId { get; set; }
        public string TargetUserId { get; set; }
        public string Command { get; set; }
    }
}
