using KuaforShop.Core.Enumertaions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaforShop.Core.Entities
{
    public class Employees : BaseEntity 
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Sex { get; set; } // 0- female 1-male
        public Guid SaloonId { get; set; }
        public enmRoles Role { get; set; }
        public  TimeOnly BeginTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public enmWorkDays WorkDays { get; set; }
    }
}
