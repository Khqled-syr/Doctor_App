using System;
using System.Collections.Generic;

namespace Final_Project.Databases
{
    public partial class TUser
    {
        public TUser()
        {
            TAppointments = new HashSet<TAppointment>();
        }

        public long UserId { get; set; }
        public string? Name { get; set; }
        public long? Number { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<TAppointment> TAppointments { get; set; }
    }
}
