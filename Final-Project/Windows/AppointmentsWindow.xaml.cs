using Final_Project.DataBase;
using Final_Project.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Final_Project
{
    public partial class AppointmentsWindow : Window
    {
        public AppointmentsWindow()
        {
            InitializeComponent();
            OnStart();
        }


        private void AgendaBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Coming Soon!");
        }


        private void AddNewPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            AddPatientPage patientPage = new AddPatientPage();
            NavigationWindow window = new NavigationWindow();
            window.Source = new Uri("/Pages/AddPatientPage.xaml", UriKind.Relative);
            window.ShowsNavigationUI = false;
            window.WindowState = WindowState.Maximized;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.WindowStyle = System.Windows.WindowStyle.None;
            window.Background = new SolidColorBrush(Colors.White);
            window.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void GithubBtn_Click(object sender, RoutedEventArgs e)
        {
            var uri = "https://github.com/Levaii";
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            System.Diagnostics.Process.Start(psi);
        }

        private void EmailBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("countrykhaled@gmail.com", "Feel free to email me!");
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

        private void AppointmentDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentsDataGrid.SelectedItem == null) return;

            PatientsWindow patientsWindow = new PatientsWindow();
            TAppointment selectedAppointment = (TAppointment)AppointmentsDataGrid.SelectedItem;
            TPatient selectedPatient = (TPatient)patientsWindow.PatientDataGrid.SelectedItem;
            var result = MessageBox.Show($"Are you sure you want to delete the appointment ?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);

            using (var db = new databaseContext())
            {
                try
                {
                    if (result == MessageBoxResult.Yes)
                    {

                        db.TAppointments.Remove(selectedAppointment);
                        db.SaveChanges();
                        AppointmentsDataGrid.ItemsSource = db.TAppointments.ToList();
                        AppointmentsDataGrid.Items.Refresh();
                        AppointmentsCount.Text = $"Appointments: {db.TAppointments.Count().ToString()}";


                        MessageBox.Show($"Succesfully deleted the appointment!");
                    }
                }
                catch
                {
                    MessageBox.Show($"Unable to delete the appointment");
                }
            }
        }

        private void OnStart()
        {
            this.WindowState = WindowState.Maximized;

            using (var db = new databaseContext())
            {
                AppointmentsDataGrid.ItemsSource = db.TAppointments
                    .Include(a => a.User)
                    .ToList();

                AppointmentsDataGrid.ItemsSource = db.TAppointments
                        .Include(a => a.Patient)
                        .ToList();

                AppointmentsCount.Text = $"Appointments: {db.TAppointments.Count().ToString()}";
            }
        }
    }

}
