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

                foreach (var p in patients)
                {
                    PatientDataGrid.Items.Add(p);
                    PatientDataGrid.Items.Refresh();
                }

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

            using (var db = new doctor_systemContext())
            {

                var idInDB = db.Patients.FirstOrDefault(p => p.PatientId == ((Patient)(PatientDataGrid.SelectedItem)).PatientId);

                if (idInDB == null) return;

                try
                {
                    db.Patients.Remove(idInDB);
           
                    db.SaveChanges();
                    PatientDataGrid.Items.Refresh();

                    MessageBox.Show("Succesfully deleted this user.");

                }
                catch
                {
                    MessageBox.Show("The deletion was not succesfully, this patient still has an appointment..");
                }



                

                

            }

        }



        private void PatientEditBtn_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}