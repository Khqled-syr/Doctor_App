using System;
using System.Linq;
using System.Windows;


namespace Final_Project
{
    public partial class AppointmentsWindow : Window
    {
        public AppointmentsWindow()
        {
            InitializeComponent();
            OnStart();

            this.WindowState = WindowState.Maximized;


            using (var db = new databaseContext())
            {
                PatientDataGrid.ItemsSource = db.TAppointments.ToList();
                AppointmentsCount.Text = $"Appointments: {db.TAppointments.Count().ToString()}";

            }
        }


        private void OnStart()
        {
            LoginWindow login = new LoginWindow();

            if (login.NameBox.Text != null)
            {
                Title.Text = "Logged in as " + App.user.Name.ToUpper();
                return;
            }
            else
            {

                MessageBox.Show("Please Login first to enter..");
                return;
            }
        }


        private async void Logout_Button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            
            this.Close();
            login.Show();
            
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow home = new HomeWindow();

            home.Show();
            this.Close();
        }
    }
}
