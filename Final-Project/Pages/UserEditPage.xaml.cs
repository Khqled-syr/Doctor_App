using Final_Project.DataBase;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Numerics;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Final_Project.Pages
{
    public partial class UserEditPage : Page
    {

        private readonly string connectionString = "Data Source=database.db";
        string selectSql = "SELECT user_ID, name FROM t_users;";

        public UserEditPage()
        {
            InitializeComponent();
            LoadUsers();

        }
        private void userBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (usersBox.SelectedItem != null)
            {
                DataRowView row = (DataRowView)usersBox.SelectedItem;
                NameTextBox.Text = row["name"].ToString();
            }
        }

        private void LoadUsers()
        {
            using (System.Data.SQLite.SQLiteConnection connection = new System.Data.SQLite.SQLiteConnection(connectionString))
            {
                connection.Open();

                using (System.Data.SQLite.SQLiteCommand command = new System.Data.SQLite.SQLiteCommand(selectSql, connection))
                {
                    using (System.Data.SQLite.SQLiteDataAdapter adapter = new System.Data.SQLite.SQLiteDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        usersBox.ItemsSource = table.DefaultView;
                        usersBox.DisplayMemberPath = "name";
                        usersBox.SelectionChanged += userBox_SelectionChanged;
                    }
                }
                connection.Close();
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                var user = NameTextBox.Text;
                var result = MessageBox.Show("Are you sure you want to delete the user?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new databaseContext())
                    {
                        DeleteUser(user);
                        db.SaveChanges();


                    }

                    NotifyLabel.Content = $"Succesfully removed {user}!";
                    NameTextBox.Text = "";

                    using (System.Data.SQLite.SQLiteConnection connection = new System.Data.SQLite.SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        using (System.Data.SQLite.SQLiteCommand command = new System.Data.SQLite.SQLiteCommand(selectSql, connection))
                        {
                            using (System.Data.SQLite.SQLiteDataAdapter adapter = new System.Data.SQLite.SQLiteDataAdapter(command))
                            {
                                DataTable table = new DataTable();
                                adapter.Fill(table);
                                usersBox.ItemsSource = table.DefaultView;
                                usersBox.DisplayMemberPath = "name";
                                usersBox.SelectionChanged += userBox_SelectionChanged;
                            }
                        }
                        connection.Close();
                    }
                }

            }
        }

        private void DeleteUser(string name)
        {
            using (var connection = new System.Data.SQLite.SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new System.Data.SQLite.SQLiteCommand("DELETE FROM t_users WHERE name = @name", connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.ExecuteNonQuery();
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

        private async void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow Login = new LoginWindow();
            Login.Visibility = Visibility.Visible;
            await Task.Delay(700);
            System.Windows.Window win = (System.Windows.Window)Parent;
            win.Close();
        }

    }
}
