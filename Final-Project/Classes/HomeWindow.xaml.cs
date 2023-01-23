using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Final_Project.Classes;
using System.Windows.Media;
using System.Windows.Navigation;
using Final_Project.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Final_Project
{
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            OnStart();

        }
        
        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
           
            LoginWindow login = new LoginWindow();

            login.Show();
            this.Close();
        }
        
        private void PatientsBtn_Click(object sender, RoutedEventArgs e)
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
        public void OnStart()
        {
            LoginWindow login = new LoginWindow();
            AppointmentsWindow appointments = new AppointmentsWindow();
            this.WindowState = WindowState.Maximized;


            using (var db = new databaseContext())
            {

                UserTitle.Text = App.user.Name.ToUpper();
                AppointmentsCount.Text = $"Today's appointments: {db.TAppointments.Count().ToString()}";


                HomePatientGrid.ItemsSource = db.TAppointments
                    .Include(a => a.User)
                        .ToList();
                
                HomePatientGrid.ItemsSource = db.TAppointments
                    .Include(a => a.Patient)
                        .ToList();


                

            }
        }

        private void AgendaBtn_Click(object sender, RoutedEventArgs e)
        {

            MyAgenda agenda = new MyAgenda();

            agenda.Show();
            this.Close();

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

        private void AddNewPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            AddPatientPage patientPage = new AddPatientPage();
            NavigationWindow window = new NavigationWindow();
            window.Source = new Uri("/Classes/AddPatientPage.xaml", UriKind.Relative);
            window.ShowsNavigationUI = false;
            window.WindowState = WindowState.Maximized;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.WindowStyle = System.Windows.WindowStyle.None;
            window.Background = new SolidColorBrush(Colors.White);
            window.Show();
            this.Visibility = Visibility.Hidden;
        }
    }
}
