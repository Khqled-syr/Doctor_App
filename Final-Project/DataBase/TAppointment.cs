﻿using System;
using System.Collections.Generic;

namespace Final_Project.DataBase
{
    public partial class TAppointment
    {
        //private TUser selectedUser = App.user;
        //private TPatient selectedPatient;

        public long AppointmentId { get; set; }
        public string? Day { get; set; }
        public string? Date { get; set; }
        public long? UserId { get; set; }
        public long? PatientId { get; set; }

        public virtual TPatient? Patient { get; set; }
        public virtual TUser? User { get; set; }

        public TAppointment(string day, string date, long? patientId, long? userId)
        {
            Day = day;
            Date = date;
            UserId = userId;
            PatientId = patientId;
        }
        public TAppointment(string day, string date, TPatient? patient, TUser? selectedUser)
        {
            Day = day;
            Date = date;
            Patient = patient;
            User = selectedUser;
        }
    }
}
