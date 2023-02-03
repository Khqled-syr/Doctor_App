using Final_Project.DataBase;
using System;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Final_Project.Pages
{

    public partial class CreateAppointmentPage : System.Windows.Controls.Page
    {

        public CreateAppointmentPage()
        {
            InitializeComponent();


            string connectionString = "Data Source=database.db";
            string selectSql = "SELECT user_ID, name FROM t_users;";

            using (System.Data.SQLite.SQLiteConnection connection = new System.Data.SQLite.SQLiteConnection(connectionString))
            {
                connection.Open();

                using (System.Data.SQLite.SQLiteCommand command = new System.Data.SQLite.SQLiteCommand(selectSql, connection))
                {

                    using (System.Data.SQLite.SQLiteDataAdapter adapter = new System.Data.SQLite.SQLiteDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        usersBox.ItemsSource = table.DefaultView;
                        usersBox.DisplayMemberPath = "name";
                        usersBox.SelectionChanged += userBox_SelectionChanged;
                    }
                }
            }
        }
        private void userBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (usersBox.SelectedItem != null)
            {
                DataRowView row = (DataRowView)usersBox.SelectedItem;
                DoctorTextBox.Text = row["name"].ToString();
            }
        }



        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

            using (var db = new databaseContext())
            {
                if (string.IsNullOrEmpty(DateTextBox.Text) && DateTextBox.Text.Length == 0 || string.IsNullOrEmpty(DescriptionTextBox.Text) && DescriptionTextBox.Text.Length == 0 || string.IsNullOrEmpty(usersBox.Text) && usersBox.Text.Length == 0)
                {
                    NotifyLabel.Content = $"Make sure to fill in all the requirements!";
                    return;
                }

                
                AppointmentsWindow appointments = new AppointmentsWindow();
                CreateAppointmentPage appointmentPage = new CreateAppointmentPage();
                
                //get userID
                DataRowView row = (DataRowView)usersBox.SelectedItem;
                var selectedUser = row["user_ID"];
                var userName = row["name"];

                //get patientID
                Window parentWindow = Window.GetWindow(this);
                if (parentWindow.GetType() == typeof(PatientsWindow))
                {         
                        PatientsWindow patientsWindow = (PatientsWindow)parentWindow;

                        TPatient selectedPatient = (TPatient)patientsWindow.PatientDataGrid.SelectedItem;

                        db.TAppointments.Add(new TAppointment { Date = DateTextBox.Text, Description = DescriptionTextBox.Text, PatientId = selectedPatient.PatientId, UserId = Convert.ToInt64(selectedUser) });
                        db.SaveChanges();

                        appointments.AppointmentsDataGrid.ItemsSource = db.TAppointments
                            .Include(a => a.User)
                            .ToList();

                        appointments.AppointmentsDataGrid.ItemsSource = db.TAppointments
                            .Include(a => a.Patient)
                            .ToList();

                        appointments.AppointmentsCount.Text = $"Appointments: {db.TAppointments.Count().ToString()}";
                        NotifyLabel.Content = $"Successfully made an appointment for '{selectedPatient.Name}'\nwith doctor '{userName}'";
 
                }
            }
        }
        private async void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            PatientsWindow patients = new PatientsWindow();
            patients.Visibility = Visibility.Visible;
            await Task.Delay(700);
            System.Windows.Window win = (System.Windows.Window)Parent;
            win.Close();
        }

        private void DateBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(DateTextBox.Text) && DateTextBox.Text.Length > 0)
                Date.Visibility = Visibility.Collapsed;
            else
                Date.Visibility = Visibility.Visible;



        }

        private void DescriptionBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(DescriptionTextBox.Text) && DescriptionTextBox.Text.Length > 0)
                Description.Visibility = Visibility.Collapsed;
            else
                Description.Visibility = Visibility.Visible;
        }

        private void DoctorBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(DoctorTextBox.Text) && DoctorTextBox.Text.Length > 0)
                Doctor.Visibility = Visibility.Collapsed;
            else
                Doctor.Visibility = Visibility.Visible;
        }
    }
}
