using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Final_Project.DataBase;

namespace Final_Project
{
    public partial class App : Application
    {
        public static TUser? user;

        ShutdownMode shutdown = ShutdownMode.OnLastWindowClose;

    }
}
