namespace OnePageNet.Data.Data.Models
{
    public class UserRelationsDto:BaseDTO
    {
        public string UserRelationship { get; set; }
        public string CurrentUser { get; set; }
        public string TargetUser { get; set; }
    }
}
