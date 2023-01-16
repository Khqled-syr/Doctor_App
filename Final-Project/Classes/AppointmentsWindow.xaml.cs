using System;
using System.Linq;
using System.Windows;
using Final_Project.DataBase;

namespace Final_Project
{
    public partial class AppointmentsWindow : Window
    {
        public AppointmentsWindow()
        {
            InitializeComponent();
            OnStart();
        }


        private void MakeAppointmentBtn_Click(object sender, RoutedEventArgs e)
        {
            
            PatientsWindow patients = new PatientsWindow();
            TPatient selectedPatient = (TPatient)patients.PatientDataGrid.SelectedItem;

/*            using (var db = new databaseContext())
            {

                var day = Microsoft.VisualBasic.Interaction.InputBox($"Enter the day", $"make an appointment", "Day");
                var date = Microsoft.VisualBasic.Interaction.InputBox("Enter the date", $"make an appointment", "Date");

                try
                {
                    db.TAppointments.Add(new TAppointment(day, date));
                    db.SaveChanges();
                    AppointmentsDataGrid.ItemsSource = db.TAppointments.ToList();
                    AppointmentsCount.Text = $"Appointments: {db.TAppointments.Count().ToString()}";


                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR" + ex);
                }

            }*/

        }


        private void OnStart()
        {
            LoginWindow login = new LoginWindow();
            this.WindowState = WindowState.Maximized;

            using (var db = new databaseContext())
            {
                AppointmentsDataGrid.ItemsSource = db.TAppointments.ToList();
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

        private void AppointmentDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AppointmentsDataGrid.SelectedItem == null) return;

            TAppointment selectedAppointment = (TAppointment)AppointmentsDataGrid.SelectedItem;

            using (var db = new databaseContext())
            {
                try
                {   
                    db.TAppointments.Remove(selectedAppointment);
                    db.SaveChanges();
                    AppointmentsDataGrid.ItemsSource = db.TAppointments.ToList();
                    AppointmentsDataGrid.Items.Refresh();
                    PatientsCount.Text = $"Appointments: {db.TAppointments.Count().ToString()}";


                    MessageBox.Show($"Succesfully deleted {selectedAppointment.AppointmentId} for patient: {selectedAppointment.PatientId}.");
                }
                catch
                {
                    MessageBox.Show($"Unable to delete {selectedAppointment.AppointmentId}.");
                }
            }

        }
    }
}
