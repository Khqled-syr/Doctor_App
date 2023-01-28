using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Final_Project.DataBase;
using BC = BCrypt.Net.BCrypt;

namespace Final_Project
{
    public partial class LoginWindow : Window
    {

        public LoginWindow()
        {
            InitializeComponent();
            //Scaffold-DbContext "DataSource=database.db" Microsoft.EntityFrameworkCore.Sqlite
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new databaseContext())
            {
                TUser? user = db.TUsers.FirstOrDefault(user => user.Name.ToLower() == NameBox.Text.ToLower());
                if (user == null)
                {   
                    Results.Content = "Password or Username is not correct, please try again.";
                    passwordBox.Clear();
                    return;
                }

                if (BC.Verify(passwordBox.Password, user.Password))
                {
                    App.user = user;
                    HomeWindow home = new HomeWindow();

                    this.Close();
                    home.Show();

                }
                else
                {
                    Results.Content = "Password or Username is not correct, please try again.";
                    passwordBox.Clear();
                    return;
                }
            }
        }

        private void Button_EnterKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                using (var db = new databaseContext())
                {
                    TUser? user = db.TUsers.FirstOrDefault(user => user.Name.ToLower() == NameBox.Text.ToLower());
                    if (user == null)
                    {
                        Results.Content = "Password or Username is not correct, please try again.";
                        passwordBox.Clear();
                        return;
                    }

                    if (BC.Verify(passwordBox.Password, user.Password))
                    {
                        App.user = user;
                        HomeWindow home = new HomeWindow();

                        this.Close();
                        home.Show();
                    }
                    else
                    {
                        Results.Content = "Password or Username is not correct, please try again.";
                        passwordBox.Clear();
                        return;
                    }
                }
            }
        }

        //Events
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NameBox.Text) && NameBox.Text.Length > 0)
                Name.Visibility = Visibility.Collapsed;
            else
                Name.Visibility = Visibility.Visible;
        }

        private void Name_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NameBox.Focus();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordBox.Password) && passwordBox.Password.Length > 0)
                Password.Visibility = Visibility.Collapsed;
            else
                Password.Visibility = Visibility.Visible;
        }
        
        
        private void Password_MouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Focus();
        }
    }
}
