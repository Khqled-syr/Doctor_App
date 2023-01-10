using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
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
                //var iDBox = Microsoft.VisualBasic.Interaction.InputBox("Enter the ID", "Add patient", "Full Name");
                var nameBox = Microsoft.VisualBasic.Interaction.InputBox("Enter the Full Name", "Add patient", "Full Name");
                var numberBox = Microsoft.VisualBasic.Interaction.InputBox("Enter the number", "Add patient", "Number");
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
                        MessageBox.Show("ERROR" + ex.Message); return;
                    }
                }
            }
        }
        private void MakeAppointmentBtn_Click(object sender, RoutedEventArgs e)
        {

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