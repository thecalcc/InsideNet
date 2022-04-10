namespace InsideNet.Data.Data.Models
{
    public class UpdateUserRelationsDto:BaseDto
    {
        public string CurrentUserId { get; set; }
        public string TargetUserId { get; set; }
        public string Command { get; set; }
    }
}
