using System;
using System.Collections.Generic;

namespace Final_Project
{
    public partial class TAppointment
    {

        public long AppointmentId { get; set; }
        public string? Day { get; set; }
        public string? Date { get; set; }
        public long? UserId { get; set; }
        public long? PatientId { get; set; }

        public virtual TPatient? Patient { get; set; }
        public virtual TUser? User { get; set; }

        public TAppointment(string day, DateTime dateTime, long patientId, int id)
        {
            Day = day;
            dateTime = dateTime;
            PatientId = patientId;
            id = id;
        }
    }
}
