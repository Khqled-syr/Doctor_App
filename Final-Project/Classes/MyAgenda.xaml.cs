using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Final_Project
{
    public partial class MyAgenda : Window
    {
        public class AgendaItem
        {
            public string? Title { get; set; }
            public string? Description { get; set; }
            public DateTime StartTime { get; set; }
            public TimeSpan Duration { get; set; }
        }

        public ObservableCollection<AgendaItem> AgendaItems { get; set; }

        public MyAgenda()
        {


            AgendaItems = new ObservableCollection<AgendaItem>();
            AgendaItems.Add(new AgendaItem { Title = "Item 1", Description = "Description for item 1", StartTime = DateTime.Now, Duration = TimeSpan.FromHours(1) });
            AgendaItems.Add(new AgendaItem { Title = "Item 2", Description = "Description for item 2", StartTime = DateTime.Now.AddHours(1), Duration = TimeSpan.FromHours(2) });



            InitializeComponent();
            DataContext = this;
        }
    }
}
