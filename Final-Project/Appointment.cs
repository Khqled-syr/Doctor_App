using System;
using System.Collections.Generic;

namespace Final_Project
{
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public string Day { get; set; } = null!;
        public DateTime Date { get; set; }
        public int PatientId { get; set; }
        public int UserId { get; set; }

        public virtual Patient Patient { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
