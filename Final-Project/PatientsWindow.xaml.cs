using System.Data.Common;
using System.Linq;
using System.Windows;

namespace Final_Project
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class PatientsWindow : Window
    {


        public PatientsWindow()
        {
            InitializeComponent();



            using (var db = new doctor_systemContext())
            {
                
                var patients = db.Patients;

                PatientDataGrid.ItemsSource = db.Patients.ToList();

                var pcount = patients.Count();
                PatientsCount.Text = $"Patients: {pcount.ToString()}";

            }
        }

        public static PatientsWindow? patients;

        private void Logout_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HomePageBtn_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow home = HomeWindow.home;

            
            home.Show();
            this.Close();

        }


        private void PatientRemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (PatientDataGrid.SelectedItem == null) return;

            Patient selectedPatient = (Patient)PatientDataGrid.SelectedItem;

            using (var db = new doctor_systemContext())
            {

                db.Patients.Remove(selectedPatient);
                db.SaveChanges();
                PatientDataGrid.ItemsSource = db.Patients.ToList();
                PatientDataGrid.Items.Refresh();

                MessageBox.Show("Succesfully deleted this user.");
                        
            }

        }



        private void PatientEditBtn_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}