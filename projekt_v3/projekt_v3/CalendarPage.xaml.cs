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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace projekt_v3
{
    /// <summary>
    /// Interaction logic for CalendarPage.xaml
    /// </summary>
    public partial class CalendarPage : UserControl
    {
        public CalendarPage()
        {
            InitializeComponent();
            LoadItemTemplate();
        }

        private void LoadItemTemplate()
        {
            using (var Events = new GradedbEntities1())
            {
                EventListView.ItemsSource = Events.Calendars.ToList();
            }
        }

        private void EventListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var Events = new GradedbEntities1()) 
            {
                Calendar item = (Calendar)EventListView.SelectedItem;
                DescLabel.Text = Events.Calendars.FirstOrDefault(p => p.Event == item.Event).Description;
            }
        }

        private void AddEvent_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new ViewPages.AddEventView());
        }

        private void DeleteEvent_Click(object sender, RoutedEventArgs e)
        {
            if (EventListView.SelectedItem != null)
            {
                using (var db = new GradedbEntities1())
                {
                    db.Calendars.Remove(db.Calendars.FirstOrDefault(p => p.Id == ((Calendar)EventListView.SelectedItem).Id));
                    db.SaveChanges();
                }
                NavigationService.GetNavigationService(this).Navigate(new CalendarPage());
            }
        }
    }
}
