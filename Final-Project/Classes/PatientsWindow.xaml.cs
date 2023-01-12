using Final_Project.Databases;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace Final_Project
{
    public partial class PatientsWindow : Window
    {
        public PatientsWindow()
        {
            InitializeComponent();
            OnStart();

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

        }

        private void PatientDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (PatientDataGrid.SelectedItem == null) return;

            TPatient selectedPatient = (TPatient)PatientDataGrid.SelectedItem;

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

            using (var db = new databaseContext())
            {
                var nameBox = Microsoft.VisualBasic.Interaction.InputBox("Enter the full name", "Add patient", "Full Name");
                var numberBox = Microsoft.VisualBasic.Interaction.InputBox("Enter the phone number", "Add patient", "Phone Number");
                var emailBox = Microsoft.VisualBasic.Interaction.InputBox("Enter the email", "Add patient", "Email");
                var addressBox = Microsoft.VisualBasic.Interaction.InputBox("Enter the address", "Add patient", "address");
                var ageBox = Microsoft.VisualBasic.Interaction.InputBox("Enter the age", "Add patient", "Age");

                try
                {
                    db.TPatients.Add(new TPatient(nameBox, Convert.ToInt32(numberBox), emailBox, addressBox, Convert.ToInt32(ageBox)));
                    db.SaveChanges();
                    PatientDataGrid.ItemsSource = db.TPatients.ToList();
                    PatientsCount.Text = $"Patients: {db.TPatients.Count().ToString()}";
                }
                catch (Exception ex)
                {
                    if (nameBox == null)
                    {
                        MessageBox.Show("ERROR" + ex.Message);
                        return;
                    }
                }
            }
        }
        private void MakeAppointmentBtn_Click(object sender, RoutedEventArgs e)
        {
            AppointmentsWindow appointments = new AppointmentsWindow();

            appointments.Show();
            this.Close();

            TPatient selectedPatient = (TPatient)PatientDataGrid.SelectedItem;
            long selectedUser = App.user.UserId;


            using (var db = new databaseContext())
            {


                var day = Microsoft.VisualBasic.Interaction.InputBox($"Enter the day", $"make an appointment for {selectedPatient.Name}", "Day");
                var date = Microsoft.VisualBasic.Interaction.InputBox("Enter the date", $"make an appointment for {selectedPatient.Name}", "Date");

                try
                {
                    Debug.WriteLine(selectedUser);
                    db.TAppointments.Add(new TAppointment(day, date, selectedPatient.PatientId, selectedUser));
                    db.SaveChanges();
                    appointments.AppointmentsDataGrid.ItemsSource = db.TAppointments.ToList();
                    appointments.AppointmentsCount.Text = $"Appointments: {db.TAppointments.Count().ToString()}";


                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR" + ex);
                }

            }

        }
        private void PatientEditBtn_Click(object sender, RoutedEventArgs e)
        {

        }


        private void OnStart()
        {
            LoginWindow login = new LoginWindow();
            this.WindowState = WindowState.Maximized;

            using (var db = new databaseContext())
            {

                PatientDataGrid.ItemsSource = db.TPatients.ToList();
                PatientsCount.Text = $"Patients: {db.TPatients.Count().ToString()}";

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

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            //string searchQuery = TextBoxSearch.Text;
            //string textToSearch = "";


            //List<string> searchResults = new List<string>();


            //int searchIndex = textToSearch.IndexOf(searchQuery);
            //while (searchIndex >= 0 )
            //{
            //    searchResults.Add(textToSearch.Substring(searchIndex, searchQuery.Length));

            //    searchIndex = textToSearch.IndexOf(searchQuery, searchIndex + searchQuery.Length);

            //}


        }


    }
}