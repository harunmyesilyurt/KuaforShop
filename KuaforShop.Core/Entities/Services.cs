using KuaforShop.Core.Enumertaions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaforShop.Core.Entities
{
    public class Services:BaseEntity
    {
        public Guid SaloonId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Duration { get; set; }
        public enmDurationType DurationType { get; set; }
    }
}
