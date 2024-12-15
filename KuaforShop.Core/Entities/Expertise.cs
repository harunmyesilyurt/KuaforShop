using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaforShop.Core.Entities
{
    public class Expertise:BaseEntity
    {
        public string Name { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
