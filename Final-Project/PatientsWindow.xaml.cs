using System.Data.Common;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using Microsoft.VisualBasic;

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


        private void PatientDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (PatientDataGrid.SelectedItem == null) return;

            Patient selectedPatient = (Patient)PatientDataGrid.SelectedItem;

            using (var db = new doctor_systemContext())
            {
                try
                {
                    db.Patients.Remove(selectedPatient);
                    db.SaveChanges();
                    PatientDataGrid.ItemsSource = db.Patients.ToList();
                    PatientDataGrid.Items.Refresh();

                    MessageBox.Show($"Succesfully deleted {selectedPatient.Name}.");
                }
                catch
                {
                    MessageBox.Show($"Unable to delete {selectedPatient.Name} due to an existed appointment!");
                }


            }

        }



        private void PatientEditBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MakeAppointmentBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddPatientBtn_Click(object sender, RoutedEventArgs e)
        {


/*            using (var db = new doctor_systemContext())
            {


                db.Patients.Add(new Patient("TEST"));
                db.SaveChanges();
                PatientDataGrid.ItemsSource = db.Patients.ToList();
                PatientDataGrid.Items.Refresh();
            }*/

        }
    }
}