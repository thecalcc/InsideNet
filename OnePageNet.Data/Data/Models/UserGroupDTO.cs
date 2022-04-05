using OnePageNet.Data.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePageNet.Data.Data.Models
{
    public class UserGroupDTO : BaseDTO
    {
        public string GroupId { get; set; }
        public string UsersId { get; set; }
    }
}
