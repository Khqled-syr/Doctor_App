using System;
using System.Collections.Generic;
using System.Net.Cache;
using System.Reflection.Metadata.Ecma335;

namespace Final_Project
{
    public partial class Patient
    {


        public int PatientId { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public int Number { get; set; }
        public string Email { get; set; } = null!;
        public string? Address { get; set; } = null!;

        public virtual ICollection<Appointment>? Appointments { get; set; }

        public Patient(int patientId, string name, int age, int number, string email, string? address, ICollection<Appointment>? appointments)
        {
            PatientId = patientId;
            Name = name;
            Age = age;
            Number = number;
            Email = email;
            Address = address;
           //Appointments = appointments;
        }

        public Patient(string name)
        {
            Name = name;
        }
    }
}
