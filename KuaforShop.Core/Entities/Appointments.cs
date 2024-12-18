using KuaforShop.Core.Enumertaions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaforShop.Core.Entities
{
    public class Appointments : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public enmAppointmentStatus Status { get; set; }

        // Navigation properties
        public virtual Users User { get; set; }
        public virtual Services Service { get; set; }
        public virtual Employees Employee { get; set; }
    }
}
