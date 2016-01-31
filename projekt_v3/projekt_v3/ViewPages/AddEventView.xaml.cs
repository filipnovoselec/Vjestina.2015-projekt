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

namespace projekt_v3.ViewPages
{
    /// <summary>
    /// Interaction logic for AddEventView.xaml
    /// </summary>
    public partial class AddEventView : Page
    {
        public AddEventView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (EvDate.SelectedDate != null && EvName != null)
            {
                using (var db = new GradedbEntities1())
                {
                    var NewEvent = new Calendar();
                    NewEvent.Event = EvName.Text;
                    NewEvent.Date = (DateTime)EvDate.SelectedDate;
                    NewEvent.Description = EvDesc.Text;

                    db.Calendars.Add(NewEvent);

                    db.SaveChanges();
                }

                NavigationService.GetNavigationService(this).Navigate(new CalendarPage());
            }
        }
    }
}
