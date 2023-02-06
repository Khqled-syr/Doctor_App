using Final_Project.DataBase;
using Final_Project.Pages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace Final_Project
{
    public partial class PatientsWindow : Window
    {

        public TPatient selectedPatient { get; set; }

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
            window.Source = new Uri("/Pages/AddPatientPage.xaml", UriKind.Relative);
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

            TPatient selectedPatient = (TPatient)PatientDataGrid.SelectedItem;
            var result = MessageBox.Show($"Are you sure you want to permanently delete {selectedPatient.Name} ?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);

            using (var db = new databaseContext())
            {
                try
                {
                    if (result == MessageBoxResult.Yes)
                    {
                        db.TPatients.Remove(selectedPatient);
                        db.SaveChanges();
                        PatientDataGrid.ItemsSource = db.TPatients.ToList();
                        PatientDataGrid.Items.Refresh();
                        PatientsCount.Text = $"Patients: {db.TPatients.Count().ToString()}";


                        MessageBox.Show($"Succesfully deleted {selectedPatient.Name}.");
                    }
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
            window.Source = new Uri("/Pages/AddPatientPage.xaml", UriKind.Relative);
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
            this.Content = new CreateAppointmentPage();
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

                            filterPatients.Add(new TPatient { PatientId = id, Name = name, Number = number, Email = email });
                        }
                    }
                }
            }
        }

        private void FilterBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string searchText = TextBoxFilter.Text.ToLower();
            var filteredItems = filterPatients.Where(item => item.Name.ToLower().Contains(searchText)).ToList();
            PatientDataGrid.ItemsSource = filteredItems;




            if (!string.IsNullOrEmpty(TextBoxFilter.Text) && TextBoxFilter.Text.Length > 0)
                Filter.Visibility = Visibility.Collapsed;
            else
                Filter.Visibility = Visibility.Visible;

        }

        private void OnStart()
        {
            this.WindowState = WindowState.Maximized;

            using (var db = new databaseContext())
            {
                PatientDataGrid.ItemsSource = filterPatients;
                PatientsCount.Text = $"Patients: {db.TPatients.Count().ToString()}";
            }
        }
    }
}