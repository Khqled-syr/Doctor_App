using Final_Project.DataBase;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Final_Project.Pages
{



    public partial class AddPatientPage : System.Windows.Controls.Page
    {
        public AddPatientPage()
        {
            InitializeComponent();
        }   

            private async void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            PatientsWindow patients = new PatientsWindow();
            patients.Visibility = Visibility.Visible;
            await Task.Delay(10);
            System.Windows.Window win = (System.Windows.Window)Parent;
            win.Close();
        }
        private async void AddBtn_Click(object sender, RoutedEventArgs e)
        {

            PatientsWindow patients = new PatientsWindow();

            using (var db = new databaseContext())
            {

                TPatient? patient = db.TPatients.FirstOrDefault(patient => patient.Name.ToLower() == NameTextBox.Text.ToLower());

                if (string.IsNullOrEmpty(NameTextBox.Text) && NameTextBox.Text.Length == 0 || string.IsNullOrEmpty(AgeTextBox.Text) && AgeTextBox.Text.Length == 0 || string.IsNullOrEmpty(NumberTextBox.Text) && NumberTextBox.Text.Length == 0 || string.IsNullOrEmpty(EmailTextBox.Text) && EmailTextBox.Text.Length == 0 || string.IsNullOrEmpty(AddressTextBox.Text) && AddressTextBox.Text.Length == 0)
                {
                    NotifyLabel.Content = $"Make sure to fill in all the requirements!";
                    await Task.Delay(2000);
                    NotifyLabel.Content = "";
                    return;
                }

                else
                {
                    int parsedValue;
                    if (!int.TryParse(NumberTextBox.Text, out parsedValue) || !int.TryParse(AgeTextBox.Text, out parsedValue))
                    {

                        NotifyLabel.Content = "Age or number is not correct!";
                        await Task.Delay(2000);
                        NotifyLabel.Content = "";
                        return;
                    }
                    else if (patient != null)
                    {
                        NotifyLabel.Content = $"this patient does already exist!";
                        await Task.Delay(2000);
                        NotifyLabel.Content = "";
                        return;
                    }
                    else
                    {
                        db.TPatients.Add(new TPatient { Name = NameTextBox.Text, Age = int.Parse(AgeTextBox.Text), Number = long.Parse(NumberTextBox.Text), Email = EmailTextBox.Text, Address = AddressTextBox.Text });
                        db.SaveChanges();


                        patients.PatientDataGrid.ItemsSource = db.TPatients.ToList();
                        patients.PatientsCount.Text = $"Patients: {db.TPatients.Count().ToString()}";

                        NotifyLabel.Content = $"Successfully added '{NameTextBox.Text}'";

                        NameTextBox.Clear();
                        AgeTextBox.Clear();
                        NumberTextBox.Clear();
                        EmailTextBox.Clear();
                        AddressTextBox.Clear();

                        await Task.Delay(2000);
                        NotifyLabel.Content = "";
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

        private void NumberTextBox_OnKeyUp(object sender, KeyEventArgs e)
        {
            string number = NumberTextBox.Text;

            if (IsValidPhoneNumber(number))
            {
                NumberTextBox.Foreground = System.Windows.Media.Brushes.Black;
            }
            else
            {
                NumberTextBox.Foreground = System.Windows.Media.Brushes.Red;
            }

        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // This is a basic example, you can use regular expressions or any other method to check if the phone number is in the correct format
            return Regex.IsMatch(phoneNumber, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
        }
    }
}
