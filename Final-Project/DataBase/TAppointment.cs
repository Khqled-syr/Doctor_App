namespace Final_Project.DataBase
{
    public partial class TAppointment
    {
        private string p;
        private string u;
        public long AppointmentId { get; set; }
        public string? Day { get; set; }
        public string? Date { get; set; }
        public long? UserId { get; set; }
        public long? PatientId { get; set; }
        public string? Description { get; set; }

        public virtual TPatient? Patient { get; set; }
        public virtual TUser? User { get; set; }



        public TAppointment(string date, string description, long? patientId, long? userId)
        {
            Date = date;
            Description = description;
            UserId = userId;
            PatientId = patientId;
        }

        public TAppointment(string date, string description, TPatient? patient, TUser? selectedUser)
        {
            Date = date;
            Description = description;
            Patient = patient;
            User = selectedUser;
        }     
        
        public TAppointment(string date, string description, string patientName, string userName)
        {
            Date = date;
            Description = description;
             p = patientName;
            u = userName;
        }

        public TAppointment() { }

    }
}
