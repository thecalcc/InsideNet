using OnePageNet.App.Data.Entities;

namespace OnePageNet.App.Data.Models;

public class GroupDto : BaseDto
{
    // This is related to the UserGroup property but we use Ids in order not to
    // send a whole entity back and forth to the front-end
    public string UserGroupId { get; set; }
    public string MediaUri { get; set; }
    public string Name { get; set; }
    public ICollection<MessageDto> Message { get; set; } 
}