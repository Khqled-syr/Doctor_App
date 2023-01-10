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
        }


        private void OnStart()
        {
            LoginWindow login = new LoginWindow();
            this.WindowState = WindowState.Maximized;

            using (var db = new databaseContext())
            {
                PatientDataGrid.ItemsSource = db.TAppointments.ToList();
                AppointmentsCount.Text = $"Appointments: {db.TAppointments.Count().ToString()}";
            }

            if (login.NameBox.Text != null)
            {
                Title.Text = App.user.Name.ToUpper();
                return;
            }
            else
            {

                Title.Text = "Guest";
                return;
            }
        }


        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
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

        private void PatientsBtn_Click(object sender, RoutedEventArgs e)
        {
            PatientsWindow patients = new PatientsWindow();

            patients.Show();
            this.Close();
        }
    }
}
