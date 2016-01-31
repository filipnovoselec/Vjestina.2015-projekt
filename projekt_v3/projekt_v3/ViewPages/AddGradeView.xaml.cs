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
    /// Interaction logic for AddGradeView.xaml
    /// </summary>
    public partial class AddGradeView : Page
    {
        Subject Sub;
        public AddGradeView(Subject Sub)
        {
            this.Sub = Sub;
            InitializeComponent();
            Date.SelectedDate = DateTime.Today;
            LoadItemSource();
        }

        private void LoadItemSource()
        {
            using (var db = new GradedbEntities1())
            {
                Col.ItemsSource = db.Columns.Where(p => p.SubjectId == Sub.Id).ToList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (((ComboBoxItem) Grade.SelectedItem) != null && (Column)Col.SelectedItem != null)
            {
                using(var db = new GradedbEntities1())
                {
                    var grade = new Grade();
                    grade.GradeValue = Convert.ToInt32(((ComboBoxItem)Grade.SelectedItem).Tag.ToString());
                    grade.Date = (DateTime) Date.SelectedDate;
                    grade.ColumnId = ((Column)Col.SelectedItem).Id;

                    db.Grades.Add(grade);

                    db.SaveChanges();
                }

                using (var db = new GradedbEntities1())
                {
                    db.Subjects.FirstOrDefault(p => p.Id == Sub.Id).UpdateAverage();

                    db.SaveChanges();
                }

                    NavigationService.GetNavigationService(this).Navigate(new ViewPages.GradeView(Sub));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).GoBack();
        }
    }
}
