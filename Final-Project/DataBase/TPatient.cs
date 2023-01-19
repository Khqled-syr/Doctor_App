using System;
using System.Collections.Generic;

namespace Final_Project.DataBase
{
    public partial class TPatient
    {

        public TPatient()
        {
            TAppointments = new HashSet<TAppointment>();
        }

        public long PatientId { get; set; }
        public string? Name { get; set; }
        public long? Number { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public long? Age { get; set; }

        public virtual ICollection<TAppointment> TAppointments { get; set; }

        public TPatient(string? name, long? number, string? email, string? address, long? age)
        {
            Name = name;
            Number = number;
            Email = email;
            Address = address;
            Age = age;
        }

    }
}
