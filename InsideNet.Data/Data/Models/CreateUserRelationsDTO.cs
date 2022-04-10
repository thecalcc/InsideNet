namespace InsideNet.Data.Data.Models
{
    public class CreateUserRelationsDto:BaseDto
    {
        public string CurrentUserId { get; set; }
        public string TargetUserId { get; set; }
    }
}
