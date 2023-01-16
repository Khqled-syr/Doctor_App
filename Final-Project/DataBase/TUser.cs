using System;
using System.Collections.Generic;
using Final_Project.DataBase;

namespace Final_Project
{
    public partial class TUser
    {
        public TUser()
        {
            TAppointments = new HashSet<TAppointment>();
        }

        public long UserId { get; set; }
        public string Name { get; set; } = null!;
        public long? Number { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<TAppointment> TAppointments { get; set; }
    }
}
