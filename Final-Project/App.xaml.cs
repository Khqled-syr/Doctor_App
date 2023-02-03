using Final_Project.DataBase;
using Final_Project.Pages;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Final_Project
{
    public partial class App : Application
    {
        public static TUser? user;


        public App()
        {
            ShutdownMode = ShutdownMode.OnLastWindowClose;
        }
    }
}