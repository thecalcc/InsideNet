using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePageNet.Data.Data.Models
{
    public class CreateUserRelationsDTO:BaseDTO
    {
        public string CurrentUserId { get; set; }
        public string TargetUserId { get; set; }
    }
}
