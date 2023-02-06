using Final_Project.DataBase;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BC = BCrypt.Net.BCrypt;

namespace Final_Project.Pages
{
    /// <summary>
    /// Interaction logic for UserRegisterPage.xaml
    /// </summary>
    public partial class UserRegisterPage : Page
    {
        public UserRegisterPage()
        {
            InitializeComponent();
        }

        private async void CreateBtn_Click(object sender, RoutedEventArgs e)
        {

            using (var db = new databaseContext())
            {
                if (string.IsNullOrEmpty(NameTextBox.Text) && NameTextBox.Text.Length == 0 || string.IsNullOrEmpty(PasswordBox.Password) && PasswordBox.Password.Length == 0 || string.IsNullOrEmpty(NumberTextBox.Text) && NumberTextBox.Text.Length == 0 || string.IsNullOrEmpty(EmailTextBox.Text) && EmailTextBox.Text.Length == 0)
                {
                    NotifyLabel.Content = $"Make sure to fill in\nall the requirements!";
                    return;
                }
                else
                {
                    int parsedValue;
                    if (!int.TryParse(NumberTextBox.Text, out parsedValue))
                    {

                        NotifyLabel.Content = "number is not correct!";
                        return;
                    }
                    else
                    {
                        var hashed = BC.HashPassword(PasswordBox.Password);
                        await Task.Delay(10);
                        db.TUsers.Add(new TUser { Name = NameTextBox.Text, Password = hashed, Number = long.Parse(NumberTextBox.Text), Email = EmailTextBox.Text });
                        db.SaveChanges();

                        await Task.Delay(10);
                        NotifyLabel.Content = $"Successfully added '{NameTextBox.Text}\nto the user list!'";

                        NameTextBox.Clear();
                        NumberTextBox.Clear();
                        EmailTextBox.Clear();
                        PasswordBox.Clear();
                    }
                }
            }
        }




        private async void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Visibility = Visibility.Visible;
            await Task.Delay(10);
            System.Windows.Window win = (System.Windows.Window)Parent;
            win.Close();
        }


        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NameTextBox.Text) && NameTextBox.Text.Length > 0)
                Name.Visibility = Visibility.Collapsed;
            else
                Name.Visibility = Visibility.Visible;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PasswordBox.Password) && PasswordBox.Password.Length > 0)
                Password.Visibility = Visibility.Collapsed;
            else
                Password.Visibility = Visibility.Visible;
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
    }
}
