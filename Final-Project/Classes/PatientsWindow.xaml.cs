using Final_Project.Classes;
using Final_Project.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Final_Project
{
    public partial class PatientsWindow : Window
    {

        public PatientsWindow()
        {
            InitializeComponent();
            OnStart();
            GetFilteredPatients();
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow home = new HomeWindow();

            home.Show();
            this.Close();
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {

            LoginWindow login = new LoginWindow();

            login.Show();
            this.Close();
        }

        private void AppointmentsBtn_Click(object sender, RoutedEventArgs e)
        {
            AppointmentsWindow appointments = new AppointmentsWindow();

            appointments.Show();
            this.Close();

        }

        private void AgendaBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Coming Soon!");

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

        private void PatientDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (PatientDataGrid.SelectedItem == null) return;

            TPatient selectedPatient = (TPatient) PatientDataGrid.SelectedItem;

            using (var db = new databaseContext())
            {
                try
                {
                    db.TPatients.Remove(selectedPatient);
                    db.SaveChanges();
                    PatientDataGrid.ItemsSource = db.TPatients.ToList();
                    PatientDataGrid.Items.Refresh();
                    PatientsCount.Text = $"Patients: {db.TPatients.Count().ToString()}";


                    MessageBox.Show($"Succesfully deleted {selectedPatient.Name}.");
                }
                catch
                {
                    MessageBox.Show($"Unable to delete {selectedPatient.Name} due to an existed appointment!");
                }
            }
        }

        private void AddPatientBtn_Click(object sender, RoutedEventArgs e)
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

        private void MakeAppointmentBtn_Click(object sender, RoutedEventArgs e)
        {
            AppointmentsWindow appointments = new AppointmentsWindow();

            appointments.Show();
            this.Close();



            TPatient selectedPatient = (TPatient) PatientDataGrid.SelectedItem;
            long selectedUser = App.user.UserId;

            using (var db = new databaseContext())
            {
                var date = Microsoft.VisualBasic.Interaction.InputBox($"Enter the date",
                    $"make an appointment for {selectedPatient.Name}", "Date");
                var description = Microsoft.VisualBasic.Interaction.InputBox("Enter the description",
                    $"make an appointment for {selectedPatient.Name}", "Description");

                try
                {
                    db.TAppointments.Add(new TAppointment(date, description, selectedPatient.PatientId, selectedUser));
                    db.SaveChanges();

                    appointments.AppointmentsDataGrid.ItemsSource = db.TAppointments
                        .Include(a => a.User)
                        .ToList();

                    appointments.AppointmentsDataGrid.ItemsSource = db.TAppointments
                        .Include(a => a.Patient)
                        .ToList();


                    appointments.AppointmentsCount.Text = $"Appointments: {db.TAppointments.Count().ToString()}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR" + ex);
                }

            }

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

        private readonly ObservableCollection<TPatient> filterPatients = new ObservableCollection<TPatient>();
        private const string connectionString = "Data Source= database.db";

        private void GetFilteredPatients()
        {
            using (System.Data.SQLite.SQLiteConnection connection = new System.Data.SQLite.SQLiteConnection(connectionString))
            {
                connection.Open();
                
                using (System.Data.SQLite.SQLiteCommand command = new System.Data.SQLite.SQLiteCommand("SELECT * FROM t_patients", connection))
                {
                    using (System.Data.SQLite.SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long id = (long)reader["patient_ID"];

                            string name = reader["Name"].ToString();

                            long number = (long)reader["Number"];

                            string email = reader["Email"].ToString();

                            filterPatients.Add(new TPatient{PatientId = id, Name =  name, Number = number, Email = email});
                        }
                    }
                }
            }
        }

        private void filterBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string searchText = TextBoxFilter.Text;
            var filteredItems = filterPatients.Where(item => item.Name.Contains(searchText)).ToList();
            PatientDataGrid.ItemsSource = filteredItems;
            
            


            if (!string.IsNullOrEmpty(TextBoxFilter.Text) && TextBoxFilter.Text.Length > 0)
                Filter.Visibility = Visibility.Collapsed;
            else
                Filter.Visibility = Visibility.Visible;

        }

        private void OnStart()
        {
            LoginWindow login = new LoginWindow();
            this.WindowState = WindowState.Maximized;

            using (var db = new databaseContext())
            {
                PatientDataGrid.ItemsSource = filterPatients;
                PatientsCount.Text = $"Patients: {db.TPatients.Count().ToString()}";
            }
        }
    }
}