using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaforShop.Application.Models
{
    public class SaloonResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        public int WorkDays { get; set; }
        public string Phone { get; set; }
        public string MyProperty { get; set; }
        public int SaloonType { get; set; }
    }
}
