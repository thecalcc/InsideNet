using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePageNet.Data.Data.Models
{
    public class AccountSettingsDTO: BaseDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public int PasswordLength { get; set; }
    }
}
