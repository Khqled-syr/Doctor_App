using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;

namespace Final_Project.DataBase
{

    public partial class TUser
    {
        public TUser()
        {
            TAppointments = new HashSet<TAppointment>();
        }


        public TUser user = App.user;

        public long UserId { get; set; }
        public string? Name { get; set; }
        public long? Number { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<TAppointment> TAppointments { get; set; }

        public static explicit operator TUser(string v)
        {
            throw new NotImplementedException();
        }
    }
}
