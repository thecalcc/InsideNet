namespace InsideNet.Data.Data.Models
{
    public class UserRelationDto : BaseDto
    {
        public string UserRelationship { get; set; }
        public string CurrentUserId { get; set; }
        public string TargetUserId { get; set; }
    }
}