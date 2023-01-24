using Final_Project.DataBase;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Final_Project.Classes
{
    public partial class AddPatientPage : Page
    {
        public AddPatientPage()
        {
            InitializeComponent();
        }

        private async void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            PatientsWindow patients = new PatientsWindow();
            patients.Visibility = Visibility.Visible;
            await Task.Delay(1000);
            Window win = (Window)this.Parent;
            win.Close();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

            PatientsWindow patients = new PatientsWindow();

            using (var db = new databaseContext())
            {
                if (string.IsNullOrEmpty(NameTextBox.Text) && NameTextBox.Text.Length == 0 || string.IsNullOrEmpty(AgeTextBox.Text) && AgeTextBox.Text.Length == 0 || string.IsNullOrEmpty(NumberTextBox.Text) && NumberTextBox.Text.Length == 0 || string.IsNullOrEmpty(EmailTextBox.Text) && EmailTextBox.Text.Length == 0 || string.IsNullOrEmpty(AddressTextBox.Text) && AddressTextBox.Text.Length == 0)
                {
                    NotifyLabel.Content = $"Make sure to fill in all the requirements!";
                    return;
                }
                else
                {
                    int parsedValue;
                    if(!int.TryParse(NumberTextBox.Text, out parsedValue) || !int.TryParse(AgeTextBox.Text, out parsedValue))
                    {

                        NotifyLabel.Content = "Age or number is not correct!";
                        return;
                    }
                    else
                    {
                        db.TPatients.Add(new TPatient { Name = NameTextBox.Text, Age = Convert.ToInt64(AgeTextBox.Text), Number = Convert.ToInt64(NumberTextBox.Text), Email = EmailTextBox.Text, Address = AddressTextBox.Text });
                        db.SaveChanges();

                        patients.PatientDataGrid.ItemsSource = db.TPatients.ToList();
                        patients.PatientsCount.Text = $"Patients: {db.TPatients.Count().ToString()}";

                        NotifyLabel.Content = $"Successfully added '{NameTextBox.Text}'";

                        NameTextBox.Clear();
                        AgeTextBox.Clear();
                        NumberTextBox.Clear();
                        EmailTextBox.Clear();
                        AddressTextBox.Clear();
                    }
                }
            }
        }



        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NameTextBox.Text) && NameTextBox.Text.Length > 0)
                Name.Visibility = Visibility.Collapsed;
            else
                Name.Visibility = Visibility.Visible;
        }
        private void AgeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(AgeTextBox.Text) && AgeTextBox.Text.Length > 0)
                Age.Visibility = Visibility.Collapsed;
            else
                Age.Visibility = Visibility.Visible;
        }
        private void NumberBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NumberTextBox.Text) && NumberTextBox.Text.Length > 0)
                Number.Visibility = Visibility.Collapsed;
            else
                Number.Visibility = Visibility.Visible;
        }
        private void EmailBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EmailTextBox.Text) && EmailTextBox.Text.Length > 0)
                Email.Visibility = Visibility.Collapsed;
            else
                Email.Visibility = Visibility.Visible;
        }
        private void AddressBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(AddressTextBox.Text) && AddressTextBox.Text.Length > 0)
                Address.Visibility = Visibility.Collapsed;
            else
                Address.Visibility = Visibility.Visible;
        }
    }
}
