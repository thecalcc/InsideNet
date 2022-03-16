using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePageNet.Data.Data.Models
{
    public class UpdateUserRelationsDTO:BaseDTO
    {
        public string CurrentUserId { get; set; }
        public string TargetUserId { get; set; }
        public string Command { get; set; }
    }
}
