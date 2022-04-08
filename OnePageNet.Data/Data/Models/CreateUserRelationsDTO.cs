namespace OnePageNet.Data.Data.Models
{
    public class CreateUserRelationsDTO:BaseDTO
    {
        public string CurrentUserId { get; set; }
        public string TargetUserId { get; set; }
    }
}
