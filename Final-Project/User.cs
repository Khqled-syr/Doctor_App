﻿using System;
using System.Collections.Generic;

namespace Final_Project
{
    public partial class User
    {
        public User()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Age { get; set; }
        public int Number { get; set; }
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}