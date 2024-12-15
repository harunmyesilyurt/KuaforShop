using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaforShop.Core.Entities
{
    public class Users : BaseEntity
    {
        public string Username { get; set; }
        public bool Sex { get; set; }// 0- female 1-male
        public string Password { get; set; }
    }
}
