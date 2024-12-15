using KuaforShop.Core.Enumertaions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaforShop.Core.Entities
{
    public class Saloons : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public TimeOnly OpenTime { get; set; }
        public TimeOnly CloseTime { get; set; }
        public enmWorkDays WorkDays { get; set; }
        public string Phone { get; set; }
        public int MyProperty { get; set; }
    }
}
