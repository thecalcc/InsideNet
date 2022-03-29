namespace OnePageNet.Data.Data.Models
{
    public class UserRelationsDto : BaseDTO
    {
        public string UserRelationship { get; set; }
        public string CurrentUserId { get; set; }
        public string TargetUserId { get; set; }
    }
}