using System.Linq;
using System.Windows;
using Final_Project.DataBase;
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
        private void OnStart()
        {
            LoginWindow login = new LoginWindow();
            this.WindowState = WindowState.Maximized;


            using (var db = new databaseContext())
            {

                HomePatientGrid.ItemsSource = db.TPatients.ToList();

                UserTitle.Text = App.user.Name.ToUpper();
                AppointmentsCount.Text = $"Patients: {db.TAppointments.Count().ToString()}";

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

        private void AgendaBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
