using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Final_Project
{
    public partial class App : Application
    {
        public static TUser? user;
        public static LoginWindow login;
        public static HomeWindow home;

        ShutdownMode shutdown = ShutdownMode.OnLastWindowClose;

    }
}
