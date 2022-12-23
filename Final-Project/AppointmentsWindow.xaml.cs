using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Final_Project
{
    /// <summary>
    /// Interaction logic for AppointmentsWindow.xaml
    /// </summary>
    public partial class AppointmentsWindow : Window
    {
        public AppointmentsWindow()
        {
            InitializeComponent();


            using (var db = new doctor_systemContext())
            {

                var appointments = db.Appointments;

                PatientDataGrid.ItemsSource = db.Appointments.ToList();

                var pcount = appointments.Count();
                PatientsCount.Text = $"Appointments: {pcount.ToString()}";

            }
        }

        private void HomePageBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Logout_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
