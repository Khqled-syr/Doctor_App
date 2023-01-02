using System.Linq;
using System.Windows;

namespace Final_Project
{
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            OnStart();

        }
        
        private void Logout_Button_Click(object sender, RoutedEventArgs e)
        {
           
            LoginWindow login = new LoginWindow();

            login.Show();
            this.Close();
        }
        
        private void PatientsButton_Click(object sender, RoutedEventArgs e)
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
        private void OnStart()
        {
            LoginWindow login = new LoginWindow();
            this.WindowState = WindowState.Maximized;




            using (var db = new databaseContext())
            {
                var appointments = db.TAppointments;

                HomeAppointmentsGrid.ItemsSource = db.TAppointments.ToList();
                HomePatientGrid.ItemsSource = db.TPatients.ToList();

                //var acount = appointments.Count();
                //var pcount = patients.Count();
                AppointmentsCount.Text = $"Appointments: {db.TAppointments.Count().ToString()}";
                PatientsCount.Text = $"Patients: {db.TPatients.Count().ToString()}";

            }


            if (login.NameBox.Text != null)
            {
                Title.Text = "Logged in as " + App.user.Name.ToUpper();
                return;
            }
            else
            {

                Title.Text = "Logged in as guest";
                return;
            }
        }

    }
}
