using Final_Project.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Final_Project
{
    public partial class MyAgenda : Window
    {


        public MyAgenda()
        {
            InitializeComponent();
        }


        public void OnStart()
        {
            using (var db = new databaseContext())
            {
                AgendaView.ItemsSource = db.TAppointments
                    .Include(a => a.User)
                    .ToList();

                

                
            }
        }
    }
}