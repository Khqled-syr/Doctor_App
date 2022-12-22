using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BC = BCrypt.Net.BCrypt;

namespace Final_Project
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        public static LoginWindow login;


        public void NameBox_TextChanged(object sender, TextChangedEventArgs e)
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

        //Scaffold-DbContext "server=localhost;database=doctor_system;user=root;" Pomelo.EntityFrameworkCore.MySql

        public void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new doctor_systemContext())
            {
                var user = db.Users.FirstOrDefault(user => user.Name == NameBox.Text);
                if (user == null)
                {
                    MessageBox.Show("Password or username is not correct!");
                    passwordBox.Clear();
                    return;
                }

                if (BC.Verify(passwordBox.Password, user.Password))
                {
                    App.user = user;

                    //MessageBox.Show("Logged in successfully!");
                    //LoginWindow login = new LoginWindow();
                    HomeWindow home = new HomeWindow();



                    home.Show();
                    this.Close();



                }
                else
                {
                    MessageBox.Show("Password or username is not correct!");
                    passwordBox.Clear();
                    return;
                }
            }
        }

        private void Button_EnterKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                using (var db = new doctor_systemContext())
                {
                    var user = db.Users.FirstOrDefault(user => user.Name == NameBox.Text);
                    if (user == null)
                    {
                        MessageBox.Show("Password or username is not correct!");
                        passwordBox.Clear();
                        return;
                    }

                    if (BC.Verify(passwordBox.Password, user.Password))
                    {
                        App.user = user;

                        //MessageBox.Show("Logged in successfully!");
                        //LoginWindow login = new LoginWindow();

                        HomeWindow home = new HomeWindow();

                        home.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Password or username is not correct!");
                        passwordBox.Clear();
                        return;
                    }
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }

        }
        /*        private bool IsMaximized = false;
                private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
                {
        *//*            if (e.ClickCount == 2)
                    {
                        this.WindowState = WindowState.Normal;
                        this.Width = 720;
                        this.Height = 1080;
                        IsMaximized = false;

                    }*//*
                    else
                    {
                        this.WindowState = WindowState.Maximized;
                        IsMaximized = true;
                    }*/

    }
}
