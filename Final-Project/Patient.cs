using System;
using System.Collections.Generic;

namespace Final_Project
{
    public partial class Patient
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int PatientId { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public int Number { get; set; }
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
