using OnePageNet.Data.Data.Entities;
using OnePageNet.Data.Data.Models;
using OnePageNet.Services.Services.Interfaces;

namespace OnePageNet.App.Controllers
{
    public class UserGroupsController : BaseController<UserGroupEntity, UserGroupDTO>
    {
        public UserGroupsController(IDatabaseService<UserGroupEntity, UserGroupDTO> databaseService) : base(databaseService)
        {

        }
    }
}
