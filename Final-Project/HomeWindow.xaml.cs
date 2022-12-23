using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using Microsoft.EntityFrameworkCore.Storage;

namespace Final_Project
{
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            txtTitle.Text = "Welcome, " + App.user.Name;
           
        }

         public static HomeWindow home;
        


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximized = false;

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (e.ClickCount == 2)
            {
                this.WindowState = WindowState.Normal;
                this.Width = 1080;
                this.Height = 720;
                IsMaximized = false;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                IsMaximized = true;
            }

        }
        private void Logout_Button_Click(object sender, RoutedEventArgs e)
        {
            
            HomeWindow home = new HomeWindow();
            LoginWindow login = new LoginWindow();

            login.Show();
            this.Close();
        }


        private void PatientsButton_Click(object sender, RoutedEventArgs e)
        {
            PatientsWindow patients = new PatientsWindow();


            patients.Show();
            this.Close();



        }

        private void AppointmentsBtn_Click(object sender, RoutedEventArgs e)
        {

            AppointmentsWindow appointments = new AppointmentsWindow();

            appointments.Show();
            this.Close();

        }

    }
}
