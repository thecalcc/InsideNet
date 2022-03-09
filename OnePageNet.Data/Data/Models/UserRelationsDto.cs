namespace OnePageNet.Data.Data.Models
{
    public class UserRelationsDto : BaseDTO
    {
        public string UserRelationship { get; set; }
        public UserDto CurrentUser { get; set; }
        public UserDto TargetUser { get; set; }
    }
}